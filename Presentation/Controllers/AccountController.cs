using Entidades.Dtos.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio.Enums;
using Servicio.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Membership system")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
            Summary = "User Login",
            Description = "Authenticates an user and return a JWT")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateWebApiAsync(request));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("admin-user-register")]
        [SwaggerOperation(
            Summary = "Admin User Creation",
            Description = "Receives the needed parameters to create a admin role user")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AdminRegisterAsync([FromBody] RegisterRequest request)
        {
            string role = RolesEnum.SuperAdmin.ToString();
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterUserAsync(request, origin, role));
        }


        [HttpGet("confirm-email")]
        [SwaggerOperation(
            Summary = "Confirmacion de usuario",
            Description = "Permite activar un usuario nuevo")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            return Ok(await _accountService.ConfirmAccountAsync(userId, token));
        }

        [HttpPost("forgot-password")]
        [SwaggerOperation(
                Summary = "Forgot PasswordHash",
                Description = "Allows the user to recover it's account by changing its password, needs to enter the email and a link will be sent to it's mail which will open the restore password view.")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPassswordAsync(request, origin));
        }



        [HttpPost("reset-password")]
        [SwaggerOperation(
            Summary = "Reestablish PasswordHash",
            Description = "Allows the user to change its password")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }
    }  
}
