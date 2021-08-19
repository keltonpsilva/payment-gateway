using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Payments.Command.CreatePayment;
using PaymentGateway.Application.Payments.Command.CreatePayment.Contracts.Response;
using PaymentGateway.Application.Payments.Queries.GetPayment;
using PaymentGateway.Application.Payments.Queries.GetPayment.Contracts.Response;
using PaymentGateway.WebAPI.Models;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PaymentGateway.WebAPI.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new Payment Intent
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/payment
        ///     {
        ///           "customerId": "12345678",
        ///           "currency": "EUR",
        ///           "amount": 123,
        ///           "cardNumber": "1234-1234-1234-1234",
        ///           "cvc": "123",
        ///           "expirationMonth": 12,
        ///           "expirationYear": 2433
        ///     }
        /// 
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Return an object of type CreatePaymentResponse</returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatePaymentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(PaymentCreateModel model)
        {
            var payment = await _mediator.Send(new CreatePaymentCommand() {
                Amount = model.Amount,
                CardNumber = model.CardNumber,
                Currency = model.Currency,
                CustomerId = model.CustomerId,
                CVC = model.CVC,
                ExpirationMonth = model.ExpirationMonth,
                ExpirationYear = model.ExpirationYear
            });

            if (payment is null) {
                return NotFound();
            }

            return Created($"api/payment/{payment.Id}", payment);
        }

        /// <summary>
        /// Get payment Information
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/payment/
        /// </remarks>
        /// <param name="id">The Id of the payment</param>
        /// <returns>Return an object of type GetPaymentResponse</returns>
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPaymentResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(string id)
        {
            var payment = await _mediator.Send(new GetPaymentQuery { Id = id });

            if (payment is null) {
                return NotFound();
            }

            return Ok(payment);
        }

    }
}
