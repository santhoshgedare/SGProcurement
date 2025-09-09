using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserSessionRepository(SGPContext context) : RootRepository<UserSession>(context),IUserSessionRepository
    {
        public async Task<UserSession?> GetActiveSessionAsync(Guid userId)
        {
            return await _context.UserSessions
                .Where(s => s.UserId == userId && s.IsActive)
                .OrderByDescending(s => s.LoginAt)
                .FirstOrDefaultAsync();
        }
    }
}
