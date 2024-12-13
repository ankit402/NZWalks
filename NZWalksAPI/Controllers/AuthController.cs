using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.DTO;
using NZWalksAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        /*User Registeration Part Added till this commit */
        //POST : /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDto)
        {
            string splterror = string.Empty;
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                PasswordHash = registerRequestDto.Password
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                //Add Role for user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Gerenated");
                    }
                }
            }
            foreach (var item in identityResult.Errors)
            {
                splterror  = item.Code + " :  " +  item.Description;
            }
            
            return BadRequest($"Error {splterror}");
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
                    // get roles 
                    var roles = await _userManager.GetRolesAsync(validUser);
                    if ((roles != null))
                    {
                        // create a token
                        var jwttoken = this.tokenRepository.CreateToken(validUser, roles.ToList());
                        var response = new LoginReponseDto
                        {
                            JwtToken = jwttoken
                        };
                        return Ok(jwttoken);
                    }
                    
                }
            }
            return BadRequest("Username or Password incorrect");
        }
    }
}
