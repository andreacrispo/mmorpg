
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using MMORPG.Repository;
using MMORPG.Service;
 
namespace MMORPG.Application
{
    public static class DependencyInjectionSetup
    {

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {

            services.AddSingleton<ICharacterFactory, CharacterFactory>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IRealTimeCharacterService, RealTimeCharacterService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
