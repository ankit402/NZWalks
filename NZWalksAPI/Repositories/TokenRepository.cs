using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalksAPI.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public TokenRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public string CreateToken(IdentityUser user, List<string> roles)
        {
            // claim token
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            // additional information for key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                claim, 
                expires : DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
