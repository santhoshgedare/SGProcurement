using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable

    {
        IUserRepository Users { get; }
        IUserSessionRepository UserSessions { get; }
        // Add other repositories here
        Task<int> CompleteAsync();
    }
}
