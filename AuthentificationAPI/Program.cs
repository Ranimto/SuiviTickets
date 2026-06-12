using Domain.Entities;
using Infrastructure;
using Infrastructure.Extensions;
using Infrastructure.Identity;
using Infrastructure.Persistence.Repository;
using Application.Interfaces.IRepos;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Mapping;
using Application.Common;
using Application.Interfaces;
using Application.Features.Auth;
using Application.Interfaces.IRepository;
using Infrastructure.Persistances.Repository;

namespace AuthentificationAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Infrastructure (DbContext + Identity)
            builder.Services.AddInfrastructure(builder.Configuration);


            // Mapster configuration
            TypeAdapterConfig.GlobalSettings.Scan(typeof(UserRepository).Assembly);
            MapsterGlobalConfig.RegisterMappings();

            // Lire la config JWT
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            // Enregistrer le JwtService
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();

            // Seed roles and admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();

                    await RoleSeeder.SeedRolesAsync(roleManager);
                    await RoleSeeder.SeedAdminUserAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Seeder error: {ex.Message}");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
