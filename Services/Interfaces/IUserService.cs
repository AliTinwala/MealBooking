using MEAL_2024_API.DTO;
using MEAL_2024_API.Models;

namespace MEAL_2024_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CheckEmailExistAsync(string email);
        string CheckPasswordStrength(string password);
        Task RegisterUserAsync(User userObj);
        Task SendResetPasswordEmailAsync(string email);
        Task ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
    }
}
