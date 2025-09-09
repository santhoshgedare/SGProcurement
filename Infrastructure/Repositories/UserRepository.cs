using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository(SGPContext context) : IUserRepository
    {
    }
}
