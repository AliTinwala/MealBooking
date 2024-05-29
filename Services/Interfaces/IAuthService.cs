using MEAL_2024_API.DTO;
using MEAL_2024_API.Models;
using System.Security.Claims;

namespace MEAL_2024_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenApiDTO> AuthenticateAsync(UseLoginDTO userObj);
        string CreateJwtToken(User user);
        string CreateRefreshToken();
        ClaimsPrincipal GetPrincipleFromExpiredToken(string token);
    }
}
