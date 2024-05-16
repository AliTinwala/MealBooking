using MEAL_2024_API.Context;
using MEAL_2024_API.DTO;
using MEAL_2024_API.Helpers;
using MEAL_2024_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MEAL_2024_API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace MEAL_2024_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        private readonly IRepository<User> _userRepository;

        public UserController(AppDbContext appDbContext, IRepository<User> repository)
        {
            _authContext = appDbContext;
            _userRepository = repository;

        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UseLoginDTO userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            }
            var user = await _authContext.Users.
                FirstOrDefaultAsync(x => x.EmailId == userObj.Email);
            if(user == null)
            {
                return NotFound(new { Message = "User Not Found!" });
            }

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect!" });
            }

            //user.Token = CreateJwtToken(user);

            //return Ok(new
            //{
            //    Token = $"{user.Token}",
            //    Message = "Login Success!"
            //});

            // Generate the JWT token
            var token = CreateJwtToken(user);
            // Assign the token to the user entity
            user.Token = token;
            var newAccessToken = user.Token;


            user.RefreshedToken = CreateRefreshToken();
            var newRefreshToken = user.RefreshedToken;

            // Update the user in the database
            _authContext.Users.Update(user);
            user.RefreshTokenExpiryTime = DateTime.Now.AddSeconds(20);
            await _authContext.SaveChangesAsync();

            //return Ok(new
            //{
            //    Token = token,
            //    Message = "Login Success!"
            //});

            return Ok(new TokenApiDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            }) ;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            }


            //check email
            if(await CheckEmailExistAsync(userObj.EmailId))
            {
                return BadRequest(new { Message = "Email Already Exists!" });
            }

            //check password strength
            var pass = CheckPasswordStregth(userObj.Password);
            if(!string.IsNullOrEmpty(pass))
            {
                return BadRequest(new { Message = pass});
            }

            userObj.UserId = new Guid();
            userObj.RegistrationDate = DateTime.Now;
            userObj.ModifiedDate = DateTime.Now;

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Token = "";

            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered!"
            });
        }


        private Task<bool> CheckEmailExistAsync(string email) => 
            _authContext.Users.AnyAsync(x => x.EmailId == email);

        private string CheckPasswordStregth(string password)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if(password.Length < 8)
            {
                stringBuilder.Append("Minimum password length should be 8" + Environment.NewLine);
            }

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") &&
                Regex.IsMatch(password, "[0-9]")))
                stringBuilder.Append("Password should he Alphanumeric" + Environment.NewLine);

            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
            {
                stringBuilder.Append("Password should contain special chars" + Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        private string CreateJwtToken(User user)
        {
            var jwtTokenHAndler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryveryveryveryverysecret.............");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHAndler.CreateToken(tokenDescriptor);
            return jwtTokenHAndler.WriteToken(token);
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _authContext.Users
                .Any(a => a.RefreshedToken == refreshToken);

            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryveryveryveryverysecret.............");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token,tokenValidationParameters,out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if(jwtSecurityToken != null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("This is invalid Token");
            }

            return principal;
        }

        [Authorize]
        [HttpGet]

        public async Task<ActionResult<User>> GetAllUsers()
        {
            var entity = await _userRepository.GetAll();
            return Ok(entity);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenApiDTO tokenApiDTO)
        {
            if(tokenApiDTO is null)
            {
                return BadRequest("Invalid CLient Request");
            }

            string accessToken = tokenApiDTO.AccessToken;
            string refreshToken = tokenApiDTO.RefreshToken;

            var principal = GetPrincipleFromExpiredToken(accessToken);
            var userName = principal.Identity.Name;

            var user = await _authContext.Users.FirstOrDefaultAsync(u => (u.FirstName + u.LastName) == userName);
            if(user is null || user.RefreshedToken != refreshToken || 
                user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid Request");
            }

            var newAccessToken = CreateJwtToken(user);
            var newRefreshToken = CreateRefreshToken();

            user.RefreshedToken = newRefreshToken;
            await _authContext.SaveChangesAsync();
            return Ok(new TokenApiDTO
            {
                AccessToken= newAccessToken,
                RefreshToken=newRefreshToken,
            });
        }

    }
}
