using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Registry;
using System;
using WitnessReports.Infrastructure.Extensions;
using WitnessReports.Service.Extensions;

namespace WitnessReports.Api.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FBI Witness Reports API", Version = "v1" });
            });
        }

        public static void ConfigurePollyPolicy(this IServiceCollection services)
        {
            var registry = new PolicyRegistry
            {
                { "HttpRequestPolicy", Policy.Handle<Exception>().WaitAndRetryAsync(3, time => TimeSpan.FromSeconds(time * 2)) }
            };
            services.AddSingleton<IReadOnlyPolicyRegistry<string>>(registry);
        }

        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddServiceDependencies();
            services.AddInfrastructureDependencies();
            return services;
        }
    }
}
