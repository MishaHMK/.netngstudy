using api.Models;

namespace api.Interfaces
{
    public interface ITokenService
    {
        string GreateToken(AppUser user);
    }
}
