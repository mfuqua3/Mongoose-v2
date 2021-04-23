using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mongoose.Api.Models.Account.Request;
using Mongoose.Api.Models.Account.Response;
using Mongoose.Api.Services;
using Mongoose.Api.Services.Contracts;
using Mongoose.Core.Entities;

namespace Mongoose.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtAuthService _jwtAuthService;
        private readonly IEmailService _emailService;

        public AccountController(
            ILogger<AccountController> logger, 
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IJwtAuthService jwtAuthService,
            IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtAuthService = jwtAuthService;
            _emailService = emailService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var authByEmail = new EmailAddressAttribute().IsValid(loginRequest.Username);
            var user = authByEmail
                ? await _userManager.FindByEmailAsync(loginRequest.Username)
                : await _userManager.FindByNameAsync(loginRequest.Username);
            if (user == null)
                return BadRequest("Invalid username of password.");
            var loginResult = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, true, false);
            if (!loginResult.Succeeded)
                return BadRequest("Invalid username of password.");
            var response = await CreateLoginResponse(user);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {

            var userExists = await _userManager.FindByEmailAsync(registrationRequest.Email) != null;
            if (userExists)
                return BadRequest("A user with that email is already registered");
            var userNameTaken = await _userManager.FindByNameAsync(registrationRequest.Username) != null;
            if (userNameTaken)
                return BadRequest("That username has already been taken");
            var newUser = new AppUser
            {
                Email = registrationRequest.Email,
                UserName = registrationRequest.Username
            };
            var result = await _userManager.CreateAsync(newUser, registrationRequest.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Description ?? "An unexpected error occurred");
            }

            var response = await CreateLoginResponse(newUser);
            return Ok(response);
        }
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
        {
            var authByEmail = new EmailAddressAttribute().IsValid(forgotPasswordRequest.Username);
            var user = authByEmail
                ? await _userManager.FindByEmailAsync(forgotPasswordRequest.Username)
                : await _userManager.FindByNameAsync(forgotPasswordRequest.Username);
            if (user == null)
                return Ok();
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var emailBody = _emailService.CreateResetEmailBody(resetToken, user.Id);
            var response = await _emailService.SendEmail(user.Email, "Password Reset Request", emailBody);
            return Ok();
        }
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordRequest.UserId);
            if (user == null)
                return BadRequest();
            var resetResult = await _userManager.ResetPasswordAsync(user, resetPasswordRequest.ResetToken,
                resetPasswordRequest.Password);
            if (resetResult.Succeeded)
                return Ok();
            return BadRequest();
        }

        private async Task<LoginResponse> CreateLoginResponse(AppUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var delimitedRoles = string.Join(',', userRoles);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, delimitedRoles)
            };
            var jwtResult = _jwtAuthService.GenerateTokens(user.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{user.UserName}] logged in the system.");
            var response = new LoginResponse
            {
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            };
            return response;
        }
    }
}
