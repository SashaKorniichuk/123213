using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Seeds
{
    public partial class Seeder
    {
        private async static Task SeedRoles(RoleManager<Role> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var user = new Role() { Name = "participant" };
                await roleManager.CreateAsync(user);
            }
        }
    }
}
