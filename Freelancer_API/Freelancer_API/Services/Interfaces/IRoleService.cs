using Freelancer_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_API.Services.Interfaces
{
    public interface IRoleService
    {
        Role GetRoleByName(string name);

        Role GetRoleById(int id);

        Role GetRoleByUserId(int userId);
    }
}
