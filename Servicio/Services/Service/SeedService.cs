using Datos.Seeds;
using Entidades;
using Microsoft.AspNetCore.Identity;
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

        public async Task SeedAsync()
        {
            await DefaultRoles.SeedAsync(_userManager, _roleManager);
            await SuperAdminUser.SeedAsync(_userManager, _roleManager);
            await TiendaUser.SeedAsync(_userManager, _roleManager);
            await ClienteUser.SeedAsync(_userManager, _roleManager);
        }
    }
}
