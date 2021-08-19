using PaymentGateway.Domain.Common.Enums;
using PaymentGateway.Domain.DomainModel.Abstract;
using System.Collections.Generic;

namespace PaymentGateway.Domain.DomainModel
{
    public class PaymentIntent : Auditable
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public List<Card> Charges { get; set; }
    }
}
