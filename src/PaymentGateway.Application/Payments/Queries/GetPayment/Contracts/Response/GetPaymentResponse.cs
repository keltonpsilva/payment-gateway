using AutoMapper;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Payments.Contracts.Response;
using PaymentGateway.Domain.Common.Enums;
using PaymentGateway.Domain.DomainModel;
using System.Collections.Generic;

namespace PaymentGateway.Application.Payments.Queries.GetPayment.Contracts.Response
{
    public class GetPaymentResponse : IMapFrom<PaymentIntent>
    {
        public string Id { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public List<CardDetailsResponseModel> Cards { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaymentIntent, GetPaymentResponse>()
                   .ForMember(s => s.Cards, options => options.MapFrom(source => source.Charges));
        }
    }
}
