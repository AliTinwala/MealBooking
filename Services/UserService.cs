using MEAL_2024_API.Context;
using MEAL_2024_API.DTO;
using MEAL_2024_API.Helpers;
using MEAL_2024_API.Models;
using MEAL_2024_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MEAL_2024_API.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _authContext;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UserService(AppDbContext authContext, IConfiguration configuration,
            IEmailService emailService)
        {
            _authContext = authContext;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _authContext.Users.AnyAsync(x => x.EmailId == email);
        }

        public string CheckPasswordStrength(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (password.Length < 8)
            {
                stringBuilder.Append("Minimum password length should be 8" + Environment.NewLine);
            }

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                stringBuilder.Append("Password should be Alphanumeric" + Environment.NewLine);
            }

            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
            {
                stringBuilder.Append("Password should contain special chars" + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        public async Task RegisterUserAsync(User userObj)
        {
            userObj.UserId = new Guid();
            userObj.RegistrationDate = DateTime.Now;
            userObj.ModifiedDate = DateTime.Now;
            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Token = "";

            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
        }

        public async Task SendResetPasswordEmailAsync(string email)
        {
            var user = await _authContext.Users.FirstOrDefaultAsync(a => a.EmailId == email);
            if (user == null)
            {
                throw new Exception("Email doesn't exist");
            }

            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            emailToken = HttpUtility.UrlEncode(emailToken);//added URL encoding

            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);
            string from = _configuration["EmailSettings:From"];
            var emailModel = new EmailModel(email, "Reset Password!",
                EmailBody.EmailStringBody(email, emailToken));
            _emailService.SendEmail(emailModel);
            _authContext.Entry(user).State = EntityState.Modified;
            await _authContext.SaveChangesAsync();
        }

        public async Task ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var decodedToken = HttpUtility.UrlDecode(resetPasswordDTO.EmailToken);// Added URL decoding
            var user = await _authContext.Users.FirstOrDefaultAsync(a => a.EmailId == resetPasswordDTO.Email);
            if (user == null)
            {
                throw new Exception("User doesn't exist!");
            }

            var tokenCode = HttpUtility.UrlDecode(user.ResetPasswordToken);// URL decode the token stored in the database
            DateTime emailTokenExpiry = user.ResetPasswordExpiry;
            if (tokenCode != resetPasswordDTO.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                throw new Exception("Invalid reset link!");
            }

            user.Password = PasswordHasher.HashPassword(resetPasswordDTO.NewPassword);
            _authContext.Entry(user).State = EntityState.Modified;
            await _authContext.SaveChangesAsync();
        }
    }
}
