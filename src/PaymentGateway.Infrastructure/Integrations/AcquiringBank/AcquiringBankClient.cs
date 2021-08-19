using PaymentGateway.Domain.Common.Enums;
using PaymentGateway.Domain.DomainModel;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Contracts.Request;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentGateway.Infrastructure.Integrations.AcquiringBank
{
    public class AcquiringBankClient : IAcquiringBankClient
    {
        private readonly List<PaymentIntent> _paymentIntents;

        public AcquiringBankClient()
        {
            _paymentIntents = new List<PaymentIntent>();
        }

        public PaymentIntent CreatePaymentIntent(PaymentIntentCreateRequest paymentIntentCreateOptions)
        {
            PaymentIntent paymentIntent = new() {
                Id = Guid.NewGuid().ToString(),
                Amount = paymentIntentCreateOptions.Amount,
                CustomerId = paymentIntentCreateOptions.CustomerId,
                Charges = new List<Card>(){
                    new Card {
                        Id = Guid.NewGuid().ToString(),
                        CardNumber = paymentIntentCreateOptions.CardNumber,
                        CustomerId = paymentIntentCreateOptions.CustomerId,
                        CVC=  paymentIntentCreateOptions.CVC,
                        ExpirationMonth =  paymentIntentCreateOptions.ExpirationMonth,
                        ExpirationYear =  paymentIntentCreateOptions.ExpirationYear,
                    }
                },
                Currency = paymentIntentCreateOptions.Currency,
                PaymentStatus = PaymentStatus.Processing,
                CreatedDateUTC = DateTime.UtcNow,
                UpdatedDateUTC = DateTime.UtcNow,
            };

            _paymentIntents.Add(paymentIntent);

            return paymentIntent;
        }

        public PaymentIntent GetPaymentIntent(string id)
        {
            return _paymentIntents.FirstOrDefault(p => p.Id == id);
        }
    }
}
