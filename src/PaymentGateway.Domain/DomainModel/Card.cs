namespace PaymentGateway.Domain.DomainModel
{
    public class Card
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string CardNumber { get; set; }

        public string CVC { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

    }
}
