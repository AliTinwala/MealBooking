using MEAL_2024_API.Context;
using MEAL_2024_API.Helpers;
using MEAL_2024_API.Models;
using MEAL_2024_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;

namespace MEAL_2024_API.Services
{
    public class UserService:IUserService
    {
        private readonly AppDbContext _authContext;

        public UserService(AppDbContext authContext)
        {
            _authContext = authContext;
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
    }
}
