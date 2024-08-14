using Entidades;
using Entidades.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Servicio.Services
{
    public class AccountService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                             IEmailService emailService,
                             IOptions<JWTSettings> jwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }
    }
}
