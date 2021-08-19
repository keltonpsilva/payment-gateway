using AutoMapper;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Domain.DomainModel;

namespace PaymentGateway.Application.Payments.Contracts.Response
{
    public class CardDetailsResponseModel : IMapFrom<Card>
    {
        public string CardNumber { get; set; }

        public string CVC { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Card, CardDetailsResponseModel>()
                   .ForMember(s => s.CardNumber, options => options.MapFrom(source => CardExtentions.AsMaskedCard(source.CardNumber)));
        }
    }

    internal static class CardExtentions
    {
        public static string AsMaskedCard(string cardNumber)
        {
            string hiddenString = cardNumber[^4..].PadLeft(cardNumber.Length, '*');
            return hiddenString;
        }
    }
}
