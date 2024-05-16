
using MEAL_2024_API.DTO;
using MEAL_2024_API.Models;
using Microsoft.AspNetCore.Mvc;
using MEAL_2024_API.Services.Interfaces;

namespace MEAL_2024_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
    
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UserController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UseLoginDTO userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }

            var token = await _authService.AuthenticateAsync(userObj);
            if (token == null)
            {
                return NotFound(new { Message = "User Not Found or Password is Incorrect!" });
            }

            return Ok(new TokenApiDTO
            {
                AccessToken = token,
                RefreshToken = _authService.CreateRefreshToken()
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }

            if (await _userService.CheckEmailExistAsync(userObj.EmailId))
            {
                return BadRequest(new { Message = "Email Already Exists!" });
            }

            var passwordValidationMessage = _userService.CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(passwordValidationMessage))
            {
                return BadRequest(new { Message = passwordValidationMessage });
            }

            await _userService.RegisterUserAsync(userObj);

            return Ok(new { Message = "User Registered!" });
        }
    }
}
