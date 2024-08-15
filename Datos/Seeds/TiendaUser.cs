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
    public class TiendaUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser tiendaUser = new();
            tiendaUser.UserName = "tiendauser";
            tiendaUser.Email = "tiendauser@email.com";
            tiendaUser.LastName = "User";
            tiendaUser.PhoneNumber = "829-123-9811";
            tiendaUser.IsActive = true;
            tiendaUser.EmailConfirmed = true;
            tiendaUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != tiendaUser.Id))
            {
                var user = await userManager.FindByEmailAsync(tiendaUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(tiendaUser, "123Pa$$word");
                    await userManager.AddToRoleAsync(tiendaUser, RolesEnum.Tienda.ToString());
                }
            }
        }
    }
}
