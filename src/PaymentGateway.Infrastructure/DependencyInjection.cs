using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Interfaces;
using PaymentGateway.Infrastructure.Services.PaymentService;
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
            services.AddSingleton<IAcquiringBankClient, AcquiringBankClient>();
            services.AddSingleton<IPaymentService, ABPaymentService>();
        }
    }
}
