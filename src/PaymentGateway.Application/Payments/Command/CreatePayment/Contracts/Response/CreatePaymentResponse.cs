using PaymentGateway.Application.Interfaces;
using PaymentGateway.Domain.Common.Enums;
using PaymentGateway.Domain.DomainModel;

namespace PaymentGateway.Application.Payments.Command.CreatePayment.Contracts.Response
{
    public class CreatePaymentResponse : IMapFrom<PaymentIntent>
    {
        public string Id { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

    }
}
