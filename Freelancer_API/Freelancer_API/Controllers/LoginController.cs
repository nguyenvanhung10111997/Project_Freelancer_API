using log4net.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Freelancer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        
        public LoginController()
        {
            
        }

        [HttpGet]
        [Route("api/v1/Login")]
        public IActionResult Login()
        {
            try
            {

                return Ok();
            }
            catch(Exception ex)
            {

                return BadRequest();
            }
        }
    }
}
