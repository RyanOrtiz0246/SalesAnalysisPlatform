using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesAnalysisPlatform.Infrastructure.Interfaces;
using SalesAnalysisPlatform.Infrastructure.Context;
using SalesAnalysisPlatform.Infrastructure.Repositories;


namespace SalesAnalysisPlatform.Infrastructure.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SalesDbContext>(options =>
                options.UseOracle(connectionString));

            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
