using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace PaymentGateway.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            AddIoC(services);

            services.AddSingleton<IRestClient, RestClient>();

            return services;
        }

        private static void AddIoC(IServiceCollection services)
        {

        }
    }
}
