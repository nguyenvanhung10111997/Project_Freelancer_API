using Freelancer_API.Services.Interfaces;
using Freelancer_Data;
using Freelancer_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_API.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly Freelancer_DBContext context;

        public UserRoleService(Freelancer_DBContext context)
        {
            this.context = context;
        }

        public void CreateUserRole(UserRole model)
        {
            context.UserRoles.Add(model);
            context.SaveChanges();
        }

    }
}
