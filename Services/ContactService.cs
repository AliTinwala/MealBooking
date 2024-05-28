using MEAL_2024_API.Models;
using MEAL_2024_API.Repositories.Interface;
using MEAL_2024_API.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;

namespace MEAL_2024_API.Services
{
    public class ContactService:IContactService
    {
        private readonly IRepository<ContactModel> _contactRepository;
        private readonly IConfiguration _config;

        public ContactService(IRepository<ContactModel> contactRepository,
            IConfiguration config)
        {
            _contactRepository = contactRepository;
            _config = config;
        }

        public async Task AddContact(ContactModel contact)
        {
            await _contactRepository.Add(contact);
            await _contactRepository.SaveChanges();
        }

        public async Task SendContactEmail(ContactModel contact)
        {
            var emailModel = new EmailModel
            (_config["EmailSettings:From"], contact.Subject,
            $"Name: {contact.Name}\nPhone: {contact.Phone}\nEmail: {contact.Email}\nMessage: {contact.Message}");
            
            await SendEmail(emailModel);
        }

        private async Task SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            var from = _config["EmailSettings:From"];
            emailMessage.From.Add(new MailboxAddress("Contact Form", from));
            emailMessage.To.Add(new MailboxAddress(emailModel.To, emailModel.To));
            emailMessage.Subject = emailModel.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = emailModel.Content
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), true);
                    await client.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
