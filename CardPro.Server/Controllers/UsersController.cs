using CardPro.Server.Models;
using CardPro.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CardPro.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost("login")]
        public ActionResult Login(User user)
        {
            if (usersService.Login(user))
            {
                return Ok(usersService.GenerateJwtToken(user));
            }
            return BadRequest("User was not found");
        }
    }
}
