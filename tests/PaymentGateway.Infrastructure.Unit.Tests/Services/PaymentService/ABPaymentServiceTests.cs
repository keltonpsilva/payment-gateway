using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PaymentGateway.Application.Payments.Command.CreatePayment;
using PaymentGateway.Domain.DomainModel;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Contracts.Request;
using PaymentGateway.Infrastructure.Integrations.AcquiringBank.Interfaces;
using PaymentGateway.Infrastructure.Services.PaymentService;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Unit.Tests.Services.PaymentService
{
    [TestFixture]
    public class ABPaymentServiceTests
    {
        private ABPaymentService _sut;
        private Fixture _fixture;
        private Mock<IAcquiringBankClient> acquiringBankClient;

        [SetUp]
        public void SetUpt()
        {
            acquiringBankClient = new Mock<IAcquiringBankClient>();

            _fixture = new Fixture();

            _sut = new ABPaymentService(acquiringBankClient.Object);
        }

        [Test]
        public async Task CreatePayment_ValidRequest_ShoudlCallPaymentGatewayAndReturnObjectTypePaymentIntent()
        {
            // Arrange
            var expectedPayment = _fixture.Create<PaymentIntent>();

            var args = new CreatePaymentArgs {
                Amount = expectedPayment.Amount,
                CardNumber = expectedPayment.Charges[0].CardNumber,
                Currency = expectedPayment.Currency,
                CustomerId = expectedPayment.CustomerId,
                CVC = expectedPayment.Charges[0].CVC,
                ExpirationMonth = expectedPayment.Charges[0].ExpirationMonth,
                ExpirationYear = expectedPayment.Charges[0].ExpirationYear,
            };

            acquiringBankClient.Setup(s => s.CreatePaymentIntent(It.IsAny<PaymentIntentCreateRequest>())).Returns(() => expectedPayment);

            // Act
            var paymentIntentResponse = await _sut.CreatePayment(args);

            // Assert
            paymentIntentResponse.Should().BeSameAs(expectedPayment);
        }


        [Test]
        public void CreatePayment_InvalidRequest_ShoudlThrowArgumentNullException()
        {
            // Arrange
            var args = _fixture.Create<CreatePaymentArgs>();
            acquiringBankClient.Setup(s => s.CreatePaymentIntent(It.IsAny<PaymentIntentCreateRequest>())).Returns(() => null);

            // Act
            Func<object> functionResult = () => _sut.CreatePayment(args);

            // Assert
            functionResult.Should().Throw<ArgumentNullException>();
        }


    }
}
