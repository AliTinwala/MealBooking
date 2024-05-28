using MEAL_2024_API.Models;

namespace MEAL_2024_API.Services.Interfaces
{
    public interface IContactService
    {
        Task AddContact(ContactModel contact);
        Task SendContactEmail(ContactModel contact);

    }
}
