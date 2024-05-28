using MEAL_2024_API.Models;
using MEAL_2024_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MEAL_2024_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<ActionResult<ContactModel>> PostContact(ContactModel contact)
        {
            await _contactService.AddContact(contact);
            await _contactService.SendContactEmail(contact);

            return contact;
        }
    }
}
