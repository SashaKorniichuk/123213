using Data.Seeds;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extension
{
    public static class ApplicationBuilderExtension
    {
        public static void AddSeeder(this IApplicationBuilder app)
        {
            Seeder.SeedData(app.ApplicationServices);
        }
    }
}
