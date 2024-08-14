using Entidades;
using Microsoft.AspNetCore.Identity;
using Servicio.Enums;

namespace Datos.Seeds
{
    public class SuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser adminuser = new();
            adminuser.UserName = "adminuser";
            adminuser.Email = "adminuser@email.com";
            adminuser.LastName = "user";
            adminuser.PhoneNumber = "829-123-9811";
            adminuser.IsActive = true;
            adminuser.EmailConfirmed = true;
            adminuser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != adminuser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminuser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(adminuser, RolesEnum.SuperAdmin.ToString());
                }
            }
        }
    }
}
