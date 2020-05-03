
using ExampleNetCoreAPI.Model;
using ExampleNetCoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleNetCoreAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var auth = _authService.Authenticate(userParam.Username, userParam.Password);
            if (auth == null)
                return BadRequest(new { message = "Kullanici veya şifre hatalı!" });
            return Ok(auth.Token);
        }
    }
}