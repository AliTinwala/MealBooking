using MEAL_2024_API.Models;

namespace MEAL_2024_API.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
