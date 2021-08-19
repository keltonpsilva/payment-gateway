using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PaymentGateway.Application.Payments.Command.CreatePayment;
using PaymentGateway.Application.Payments.Command.CreatePayment.Contracts.Response;
using PaymentGateway.Application.Payments.Queries.GetPayment;
using PaymentGateway.Application.Payments.Queries.GetPayment.Contracts.Response;
using PaymentGateway.Domain.Common.Enums;
using PaymentGateway.WebAPI.Controllers;
using PaymentGateway.WebAPI.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.WebAPI.Unit.Tests.Controllers
{
    [TestFixture]
    public class PaymentControllerTests
    {
        private PaymentController _sut;
        private Fixture _fixture;

        private Mock<IMediator> mediator;

        [SetUp]
        public void SetUp()
        {
            mediator = new Mock<IMediator>();

            _fixture = new Fixture();

            _sut = new PaymentController(mediator.Object);

        }

        [Test]
        public async Task Post_ValidPaymentCreateModel_ShouldReturnOkActionResultAndObjectTypeOfCreatePaymentResponse()
        {
            // Assert
            var expectedResponse = _fixture.Build<CreatePaymentResponse>()
                                           .With(p => p.PaymentStatus, PaymentStatus.Processing)
                                           .Create();
            var model = _fixture.Build<PaymentCreateModel>().Create();
            mediator.Setup(m => m.Send(It.IsAny<CreatePaymentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => expectedResponse);

            // Act
            var actionResult = await _sut.Post(model);
            var actionResultContent = (CreatePaymentResponse)(actionResult as CreatedResult).Value;

            // Arrange
            actionResult.Should().BeOfType<CreatedResult>();
            actionResultContent.Should().NotBeNull();
            actionResultContent.Should().BeEquivalentTo(expectedResponse);
            actionResultContent.PaymentStatus.Should().Be(PaymentStatus.Processing);
        }


        [Test]
        public async Task Get_ValidPaymentId_ShouldReturnOkActionResultAndObjectTypeOfGetPaymentResponse()
        {
            // Assert
            var expectedResponse = _fixture.Build<GetPaymentResponse>().Create();
            var paymentId = expectedResponse.Id;
            mediator.Setup(m => m.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => expectedResponse);

            // Act
            var actionResult = await _sut.Get(paymentId);
            var actionResultContent = (GetPaymentResponse)(actionResult as OkObjectResult).Value;

            // Arrange
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResultContent.Should().NotBeNull();
            actionResultContent.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public async Task Get_PaymentIdIsNotValid_ShouldReturnActionResultNotFound()
        {
            // Assert
            var paymentId = Guid.NewGuid().ToString();
            mediator.Setup(m => m.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => null);

            // Act
            var actionResult = await _sut.Get(paymentId);

            // Arrange
            actionResult.Should().BeOfType<NotFoundResult>();
        }

    }
}
