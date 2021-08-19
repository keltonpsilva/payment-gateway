namespace PaymentGateway.Application.Payments.Command.CreatePayment
{
    public class CreatePaymentArgs
    {
        public string CustomerId { get; set; }

        /// <summary>
        /// Three-letter ISO currency code, in lowercase.         
        /// </summary>
        /// <remarks>
        /// Support Currencies:
        /// - GBP
        /// - EUR       
        /// </remarks>
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
