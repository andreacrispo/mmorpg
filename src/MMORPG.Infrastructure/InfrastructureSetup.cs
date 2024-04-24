using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MMORPG.Infrastrutture
{
    public static class InfrastructureSetup
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var path = Directory.GetCurrentDirectory();  
            var DbPath = Path.Join(path, "app.db");

            services.AddDbContext<MMORPGDbContext>(options =>
                options.UseSqlite($"Data Source={DbPath}"));

            return services;
        }
    }
}
