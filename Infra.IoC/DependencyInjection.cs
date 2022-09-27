using Application.Mappings;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Interfaces.Repositories;
using Infra.Data.Context;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var mySqlVersion = new MySqlServerVersion(new Version(14, 41, 0));
            services.AddDbContext<GameeDbContext>(opt => opt.UseMySql(configuration.GetConnectionString("DefaultConnection"), mySqlVersion));

            services.AddScoped<ITeamRepository, TeamRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapper));
            services.AddScoped<ITeamService, TeamService>();

            return services;
        }

        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
