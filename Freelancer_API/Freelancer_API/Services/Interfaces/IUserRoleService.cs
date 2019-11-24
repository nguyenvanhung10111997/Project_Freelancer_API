using Freelancer_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_API.Services.Interfaces
{
    public interface IUserRoleService
    {
        void CreateUserRole(UserRole model);
    }
}
