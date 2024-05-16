using MEAL_2024_API.Context;
using MEAL_2024_API.DTO;
using MEAL_2024_API.Helpers;
using MEAL_2024_API.Models;
using MEAL_2024_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MEAL_2024_API.Services
{
    public class AuthService:IAuthService

    {
        private readonly AppDbContext _authContext;

        public AuthService(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<string> AuthenticateAsync(UseLoginDTO userObj)
        {
            var user = await _authContext.Users.FirstOrDefaultAsync(x => x.EmailId == userObj.Email);
            if (user == null || !PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return null;
            }

            var token = CreateJwtToken(user);
            user.Token = token;

            var refreshToken = CreateRefreshToken();
            user.RefreshedToken = refreshToken;

            var refreshTokenExpiryTime = DateTime.Now.AddDays(5);
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            _authContext.Users.Update(user);
            await _authContext.SaveChangesAsync();

            return token;
        }

        public string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryveryveryveryverysecret.............");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _authContext.Users.Any(a => a.RefreshedToken == refreshToken);

            return tokenInUser ? CreateRefreshToken() : refreshToken;
        }

        public ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryveryveryveryverysecret.............");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
}
}
