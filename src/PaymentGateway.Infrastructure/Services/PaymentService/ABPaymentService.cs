using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Payments.Command.CreatePayment;
using PaymentGateway.Domain.DomainModel;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Contracts.Request;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Interfaces;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Services.PaymentService
{
    public class ABPaymentService : IPaymentService
    {
        private readonly IAcquiringBankClient _acquiringBankClient;

        public ABPaymentService(IAcquiringBankClient acquiringBankClient)
        {
            _acquiringBankClient = acquiringBankClient;
        }

        public Task<PaymentIntent> CreatePayment(CreatePaymentArgs args)
        {
            var paymentIntent = _acquiringBankClient.CreatePaymentIntent(new PaymentIntentCreateRequest {
                Amount = args.Amount,
                CardNumber = args.CardNumber,
                Currency = args.Currency,
                CustomerId = args.CustomerId,
                CVC = args.CVC,
                ExpirationMonth = args.ExpirationMonth,
                ExpirationYear = args.ExpirationYear
            });

            if (paymentIntent is null) {
                // We could log the issue

                throw new ArgumentNullException(nameof(args), "Error creating the payment");
            }

            return Task.FromResult(paymentIntent);
        }

        public Task<PaymentIntent> RetrievePayment(string id)
        {
            var paymentIntent = _acquiringBankClient.GetPaymentIntent(id);

            return Task.FromResult(paymentIntent);
        }
    }
}
