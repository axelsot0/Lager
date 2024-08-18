using Datos.Seeds;
using Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Servicio.Interface;

namespace Servicio.Services.Service
{
    public class SeedService : ISeedService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await DefaultRoles.SeedAsync(userManager, roleManager);
            await SuperAdminUser.SeedAsync(userManager, roleManager);
            await TiendaUser.SeedAsync(userManager, roleManager);
            await ClienteUser.SeedAsync(userManager, roleManager);
        }
    }
}
