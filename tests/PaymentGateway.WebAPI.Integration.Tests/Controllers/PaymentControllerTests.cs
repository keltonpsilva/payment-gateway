using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using PaymentGateway.Application.Payments.Command.CreatePayment.Contracts.Response;
using PaymentGateway.Domain.Common.Enums;
using PaymentGateway.WebAPI.Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.WebAPI.Integration.Tests.Controllers
{
    [TestFixture]
    public class PaymentControllerTests : IntegrationTestBaseClass
    {
        [Test]
        public async Task Post_ValidPaymentCreateModel_ShouldReturnOkActionResultAndObjectTypeOfCreatePaymentResponse()
        {
            // Assert
            var model = new PaymentCreateModel {
                Amount = 123M,
                CardNumber = "1234-1234-1234-1234",
                Currency = "EUR",
                CustomerId = "12345",
                CVC = "123",
                ExpirationMonth = 12,
                ExpirationYear = 2322,
            };

            // Act
            var body = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"api/payment", body);
            var actionResultContent = JsonConvert.DeserializeObject<CreatePaymentResponse>(await response.Content.ReadAsStringAsync());

            // Arrange
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            actionResultContent.Should().NotBeNull();
            actionResultContent.PaymentStatus.Should().Be(PaymentStatus.Processing);
        }
    }
}
