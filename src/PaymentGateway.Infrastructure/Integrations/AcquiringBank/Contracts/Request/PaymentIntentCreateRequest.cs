namespace PaymentGateway.Infrastructure.Integrations.AcquiringBank.Contracts.Request
{
    public class PaymentIntentCreateRequest
    {
        public string CustomerId { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string CardNumber { get; set; }

        public string CVC { get; set; }

        /// <summary>
        /// Two-digit number representing the card’s expiration month.
        /// </summary>
        public int ExpirationMonth { get; set; }

        /// <summary>
        /// Four-digit number representing the card’s expiration year.
        /// </summary>
        public int ExpirationYear { get; set; }
    }
}
