
using MEAL_2024_API.DTO;
using MEAL_2024_API.Models;
using Microsoft.AspNetCore.Mvc;
using MEAL_2024_API.Services.Interfaces;
using MEAL_2024_API.Context;
using System.Security.Claims;

namespace MEAL_2024_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
    
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
    
        public UserController(IAuthService authService, IUserService userService
            , IConfiguration configuration,
            IEmailService emailService,AppDbContext authContext)
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

            return Ok(token);
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

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            try
            {
                Console.WriteLine($"Received email:{email}"); 
                await _userService.SendResetPasswordEmailAsync(email);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Email Sent!"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                string result = await _userService.ResetPasswordAsync(resetPasswordDTO);

                if (result == "Password reset successfully")
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Password Reset Successfully!"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = result
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }

        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var email = changePasswordDTO.Email;

                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email not found in request body");
                }

                var result = await _userService.ChangePasswordAsync(email, changePasswordDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "Error changing password");

                return StatusCode(500, "An error occurred while changing the password.");
            }
        }

    }
}
