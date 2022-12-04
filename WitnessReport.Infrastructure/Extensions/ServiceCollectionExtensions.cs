using Microsoft.Extensions.DependencyInjection;
using WitnessReports.Interface.Infrastructure;

namespace WitnessReports.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddHttpClient<IFBIClient, FBIClient>();

            services.AddScoped<IFBIWantedService, FBIWantedService>();
            services.AddScoped<IReportCreationService, ReportCreationService>();

            return services;
        }
    }
}
