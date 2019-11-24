using Freelancer_API.Services.Interfaces;
using Freelancer_Data;
using Freelancer_Data.Models;
using Freelancer_API.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelancer_API.Services
{
    public class UserService: IUserService
    {
        private readonly Freelancer_DBContext context;
        public UserService(Freelancer_DBContext context)
        {
            this.context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = context.Users.FirstOrDefault(x => x.UserName == username);

            if (user == null)
                return null;

            if (!App_Helper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public IList<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetUserById(int UserId)
        {
            return context.Users.FirstOrDefault(x => x.UserId == UserId);
        }

        public User GetUserByUserName(string username)
        {
            return context.Users.FirstOrDefault(u => u.UserName.Equals(username));
        }

        public User CreateUser(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (context.Users.Any(x => x.UserName == user.UserName))
                throw new Exception("Username \"" + user.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            App_Helper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }
    }
}
