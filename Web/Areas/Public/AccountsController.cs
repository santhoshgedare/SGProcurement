using Core.DTOs;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Areas.Public
{
    public class AccountsController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration config,
        IUnitOfWork unitOfWork,
        SGPContext dbContext) : PublicController
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IConfiguration _config = config;
        private readonly SGPContext _dbContext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                DisplayName = string.IsNullOrEmpty(dto.DisplayName)
                                ? $"{dto.FirstName} {dto.LastName}"
                                : dto.DisplayName,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Add default role "User"
            await _userManager.AddToRoleAsync(user, "Admin");

            return Ok("User registered successfully");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || user.IsDeleted)
                return Unauthorized("Invalid username or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid username or password");

            // Capture session via UnitOfWork
            var userAgent = Request.Headers["User-Agent"].ToString();
            var session = new UserSession
            {
                UserId = user.Id,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                UserAgent = userAgent,
                DeviceType = DetectDeviceType(userAgent),
                Browser = DetectBrowser(userAgent),
                OperatingSystem = DetectOS(userAgent),
                Location = "Unknown",IsActive=true
            };
            _unitOfWork.UserSessions.Add(session);
            await _unitOfWork.CompleteAsync();

            var token = await GenerateJwtToken(user);


           
            return Ok(new { token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity?.Name;
            if (userName != null)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var session = await _unitOfWork.UserSessions.GetActiveSessionAsync(user.Id);
                    if (session != null)
                    {
                        session.IsActive = false;
                        session.ExpiredAutomatically = false; 
                        session.LogoutAt = DateTime.UtcNow;
                        await _unitOfWork.CompleteAsync();
                    }
                }
            }

            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully");
        }

         
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var userName = User.Identity?.Name;
            if (userName == null) return Unauthorized();

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null || user.IsDeleted) return NotFound();

            return Ok(new
            {
                user.UserName,
                user.Email,
                user.DisplayName,
                user.UserType,
                user.FullName
            });
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var secret = _config["JwtSettings:SecretKey"]
                 ?? throw new InvalidOperationException("JWT SecretKey is missing in configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim("DisplayName", user.DisplayName ?? ""),
                new Claim("UserType", user.UserType.ToString())
            };

            var roles = (await _userManager.GetRolesAsync(user))
                        .Where(r => !string.IsNullOrEmpty(r))
                        .ToList();

            claims.Add(new Claim("Roles", System.Text.Json.JsonSerializer.Serialize(roles)));

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string DetectDeviceType(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";
            if (userAgent.Contains("Mobi")) return "Mobile";
            if (userAgent.Contains("Tablet")) return "Tablet";
            return "Desktop";
        }

        private string DetectBrowser(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";
            if (userAgent.Contains("Edg")) return "Edge";
            if (userAgent.Contains("Chrome")) return "Chrome";
            if (userAgent.Contains("Firefox")) return "Firefox";
            if (userAgent.Contains("Safari") && !userAgent.Contains("Chrome")) return "Safari";
            return "Other";
        }

        private string DetectOS(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent)) return "Unknown";
            if (userAgent.Contains("Windows")) return "Windows";
            if (userAgent.Contains("Android")) return "Android";
            if (userAgent.Contains("iPhone") || userAgent.Contains("iPad")) return "iOS";
            if (userAgent.Contains("Mac OS")) return "MacOS";
            if (userAgent.Contains("Linux")) return "Linux";
            return "Other";
        }

    }
}
