using Freelancer_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_API.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

        IList<User> GetAll();

        User CreateUser(User user, string password);

        User GetUserById(int userId);

        User GetUserByUserName(string username);
    }
}
