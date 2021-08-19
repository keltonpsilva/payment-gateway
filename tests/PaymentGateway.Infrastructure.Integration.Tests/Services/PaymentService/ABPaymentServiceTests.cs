using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using PaymentGateway.Application.Payments.Command.CreatePayment;
using PaymentGateway.Domain.Common.Enums;
using PaymentGateway.Domain.DomainModel;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Interfaces;
using PaymentGateway.Infrastructure.Services.PaymentService;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Integration.Tests.Services.PaymentService
{
    [TestFixture]
    public class ABPaymentServiceTests
    {
        private ABPaymentService _sut;
        private Fixture _fixture;

        private IAcquiringBankClient acquiringBankClient;

        public ABPaymentServiceTests()
        {
            acquiringBankClient = new AcquiringBankClient();
            _fixture = new Fixture();
            _sut = new ABPaymentService(acquiringBankClient);
        }


        [Test]
        public async Task CreatePayment_ValidRequest_ShoudlCallPaymentGatewayAndReturnObjectTypePaymentIntent()
        {
            // Arrange
            var expectedCard = _fixture.Build<Card>().Create();

            var expectedPayment = _fixture.Build<PaymentIntent>()
                                          .With(p => p.PaymentStatus, PaymentStatus.Processing)
                                          .With(p => p.Charges, new System.Collections.Generic.List<Card> { expectedCard })
                                          .Create();

            var args = new CreatePaymentArgs {
                Amount = expectedPayment.Amount,
                CardNumber = expectedPayment.Charges[0].CardNumber,
                Currency = expectedPayment.Currency,
                CustomerId = expectedPayment.CustomerId,
                CVC = expectedPayment.Charges[0].CVC,
                ExpirationMonth = expectedPayment.Charges[0].ExpirationMonth,
                ExpirationYear = expectedPayment.Charges[0].ExpirationYear,
            };

            // Act
            var paymentIntentResponse = await _sut.CreatePayment(args);

            // Assert
            paymentIntentResponse.Should()
                                 .BeEquivalentTo(expectedPayment, opt => opt.Excluding(e => e.CustomerId)
                                                                            .Excluding(e => e.Id)
                                                                            .Excluding(e => e.Charges)
                                                                            .Excluding(e => e.CreatedDateUTC)
                                                                            .Excluding(e => e.UpdatedDateUTC));
        }
    }
}
