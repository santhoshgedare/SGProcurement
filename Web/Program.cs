using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Seed;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ===== DbContext =====
builder.Services.AddDbContext<SGPContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ===== Identity =====
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<SGPContext>()
.AddDefaultTokenProviders();

// ===== JWT =====
var jwt = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwt.GetValue<string>("SecretKey")?? "YourVeryStrongSecretKeyHere123456789");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.GetValue<string>("Issuer"),
        ValidAudience = jwt.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ClockSkew = TimeSpan.Zero
    };
});

// ===== Services =====

builder.Services.AddControllers()
   .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();

// ===== Swagger =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SGP API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() }
    });
});

var app = builder.Build();

// ===== Redirect root to Swagger =====
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/") { context.Response.Redirect("/docs"); return; }
    await next();
});

// ===== Basic Auth for Swagger =====
var swaggerUser = builder.Configuration["SwaggerAuth:Username"];
var swaggerPass = builder.Configuration["SwaggerAuth:Password"];

app.UseWhen(c => c.Request.Path.StartsWithSegments("/docs") || c.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        var auth = context.Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(auth) && auth.StartsWith("Basic "))
        {
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(auth["Basic ".Length..])).Split(':', 2);
            if (decoded.Length == 2 && decoded[0] == swaggerUser && decoded[1] == swaggerPass) { await next(); return; }
        }
        context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Swagger\"";
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    });
});

// ===== Middleware =====
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SGP API v1"); c.RoutePrefix = "docs"; });

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// Run Identity Seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await IdentitySeeder.SeedAsync(userManager, roleManager);
}


app.Run();
