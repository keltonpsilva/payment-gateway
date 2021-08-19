using PaymentGateway.Domain.Common.Interfaces;
using System;

namespace PaymentGateway.Domain.DomainModel.Abstract
{
    public abstract class Auditable : IAuditable
    {
        public DateTime CreatedDateUTC { get; set; }

        public DateTime UpdatedDateUTC { get; set; }
    }
}
