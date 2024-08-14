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
    public class Cliente
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser clientuser = new();
            clientuser.UserName = "clientuser";
            clientuser.Email = "clientuser@email.com";
            clientuser.LastName = "User";
            clientuser.PhoneNumber = "829-123-9811";
            clientuser.IsActive = true;
            clientuser.EmailConfirmed = true;
            clientuser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != clientuser.Id))
            {
                var user = await userManager.FindByEmailAsync(clientuser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(clientuser, RolesEnum.Cliente.ToString());
                }
            }
        }
    }
}
