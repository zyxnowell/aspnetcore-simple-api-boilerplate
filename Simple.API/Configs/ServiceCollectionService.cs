using Microsoft.Extensions.DependencyInjection;
using Simple.Core.Interfaces;
using Simple.Core.Services;

namespace Simple.API.Configs
{
    /// <summary>
    /// Service Collection
    /// </summary>
    public static class ServiceCollectionService
    {
        public static IServiceCollection RegisterServiceCollectionDI(this IServiceCollection services)
        {
            //Add DIs here

            //services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ISampleService, SampleService>();


            return services;
        }
    }
}
