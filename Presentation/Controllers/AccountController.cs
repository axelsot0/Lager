using Entidades.Dtos.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("user-register")]
        [SwaggerOperation(
            Summary = "User Creation",
            Description = "Receives the needed parameters to create a user")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterUserAsync(request, origin, request.Role));
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


        [HttpPost("Change-Status")]
        [SwaggerOperation(
            Summary = "Changing the status of user",
            Description = "Allow that the users join or not to their account ")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ChangeStatusAsync([FromBody] ChangeStatusUser request)
        {
            return Ok(await _accountService.ChangeUserStatus(request));
        }


        [HttpPost("Get-All-Tiendas")]
        [SwaggerOperation(
            Summary = "Get All the users with the role SuperMarket ",
            Description = "Allow get all the supermarkets ")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Async()
        {
            return Ok(await _accountService.GetAllTiendas());
        }
    }  
}
