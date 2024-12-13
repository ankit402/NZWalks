using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
