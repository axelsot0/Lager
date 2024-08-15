using Entidades;
using Microsoft.AspNetCore.Identity;
using Servicio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Tienda.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RolesEnum.Cliente.ToString()));
        }
    }
}
