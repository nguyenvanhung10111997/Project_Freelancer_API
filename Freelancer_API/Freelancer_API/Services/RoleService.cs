using Freelancer_API.Services.Interfaces;
using Freelancer_Data;
using Freelancer_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly Freelancer_DBContext context;

        public RoleService(Freelancer_DBContext context)
        {
            this.context = context;
        }

        public Role GetRoleByName(string name)
        {
            return context.Roles.FirstOrDefault(r => r.Name.Equals(name));
        }

        public Role GetRoleById(int id)
        {
            return context.Roles.FirstOrDefault(r => r.RoleId == id);
        }

        public Role GetRoleByUserId(int userId)
        {
            var userRole = context.UserRoles.FirstOrDefault(u => u.UserId == userId);

            return context.Roles.FirstOrDefault(r => r.RoleId == userRole.RoleId);
        }
    }
}
