using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        /*User Registeration Part Added till this commit */
        //POST : /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                PasswordHash = registerRequestDto.Password
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if(identityResult.Succeeded)
            {
                //Add Role for user
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Gerenated");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTo _loginuser)
        {
            var validUser = await _userManager.FindByEmailAsync(_loginuser.Username);
            if (validUser != null)
            {
                var verifypassword = await _userManager.CheckPasswordAsync(validUser, _loginuser.Password);
                if (verifypassword)
                {
                    // Create Token Here only /////////



                    return Ok();
                }
            }
            return BadRequest("Username or Password incorrect");
        }
    }
}
