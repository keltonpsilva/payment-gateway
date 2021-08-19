using PaymentGateway.Domain.DomainModel;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Contracts.Request;

namespace PaymentGateway.Infrastructure.Integrations.AcquiringBank.Interfaces
{
    public interface IAcquiringBankClient
    {
        PaymentIntent GetPaymentIntent(string id);
        PaymentIntent CreatePaymentIntent(PaymentIntentCreateRequest paymentIntentCreateOptions);
    }
}
