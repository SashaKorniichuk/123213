using Data;
using Data.DBContext;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

namespace Domain.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configurator)
        {
            services.AddSingleton<IMongoClient>(options => new MongoClient(configurator.GetConnectionString("MongoDb")));
            services.AddScoped(s => new DbContext(s.GetRequiredService<IMongoClient>(), configurator["DbName"]));
            return services;
        }

        public static IServiceCollection RegisterGenericRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
 
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        public static IServiceCollection RegisterIdentity(this IServiceCollection services, IConfiguration configurator)
        {
            var builder = services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddMongoDbStores<User, Role, string>
            (
                configurator.GetConnectionString("MongoDb"), configurator["DbName"]
            ).AddDefaultTokenProviders();

            var identityBuilder = new IdentityBuilder(builder.UserType, builder.RoleType, builder.Services);
            identityBuilder.AddSignInManager<SignInManager<User>>();

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurator["Secret"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
