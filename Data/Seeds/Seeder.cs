using Data.DBContext;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Seeds
{
    public partial class Seeder
    {
        public async static Task SeedData(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<DbContext>();

                await SeedRoles(roleManager);
                await SeedTasks(context);
            }
        }
    }
}
