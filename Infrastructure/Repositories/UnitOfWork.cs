using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork(SGPContext context) : IUnitOfWork
    {
        private readonly SGPContext _context = context;

        // Backing fields for repositories
        private IUserRepository? _users;
        private IUserSessionRepository? _userSessions;

        // Expose repositories
        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IUserSessionRepository UserSessions => _userSessions ??= new UserSessionRepository(_context);

        // Save changes
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Dispose DbContext
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
