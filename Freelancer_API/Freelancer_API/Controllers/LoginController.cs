using Freelancer_API.Common;
using Freelancer_API.Helpers;
using Freelancer_API.Models;
using Freelancer_API.Services.Interfaces;
using Freelancer_Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Freelancer_API.Controllers
{
    [Authorize]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly AppSettings appSettings;
        private readonly ILogger logger;
        private readonly IRoleService roleService;
        private readonly IUserRoleService userRoleService;

        public LoginController(IUserService userService,
            IOptions<AppSettings> appSettings,
            ILoggerFactory logger,
            IRoleService roleService,
            IUserRoleService userRoleService)
        {
            this.userService = userService;
            this.appSettings = appSettings.Value;
            this.logger = logger.CreateLogger<LoginController>();
            this.roleService = roleService;
            this.userRoleService = userRoleService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/v1/Authenticate")]
        public IActionResult Authenticate([FromBody]UserModel model)
        {
            var user = userService.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var role = roleService.GetRoleByUserId(user.UserId);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, role.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = model.Id,
                Username = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [Route("/api/v1/Register")]
        [HttpPost]
        public IActionResult Register([FromBody]UserModel model)
        {
            try
            {
                var role = roleService.GetRoleByName(Constants.ROLE_ADMIN);
                var user = new User
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                userService.CreateUser(user, model.Password);

                var userCreated = userService.GetUserByUserName(model.UserName);
                var userRole = new UserRole
                {
                    UserId = userCreated.UserId,
                    RoleId = role.RoleId,
                    IsActive = true
                };

                userRoleService.CreateUserRole(userRole);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Constants.ROLE_ADMIN)]
        [HttpGet]
        [Route("/api/v1/GetAllUser")]
        public IActionResult GetAll()
        {
            var users = userService.GetAll();

            return Ok(users);
        }
    }
}
