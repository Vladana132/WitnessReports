using Microsoft.Extensions.DependencyInjection;
using WitnessReports.Interface.Services;
using WitnessReports.Service.Services;

namespace WitnessReports.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services
          )
        {
            return services
                    .AddScoped<IWitnessReportService, WitnessReportService>();
        }
    }
}
