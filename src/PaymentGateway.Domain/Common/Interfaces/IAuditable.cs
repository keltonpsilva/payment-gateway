using System;

namespace PaymentGateway.Domain.Common.Interfaces
{
    public interface IAuditable
    {
        DateTime CreatedDateUTC { get; set; }
        DateTime UpdatedDateUTC { get; set; }
    }
}
