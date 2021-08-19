using AutoMapper;
using MediatR;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Payments.Command.CreatePayment.Contracts.Response;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Payments.Command.CreatePayment
{
    public class CreatePaymentCommand : IRequest<CreatePaymentResponse>
    {
        public string CustomerId { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string CardNumber { get; set; }

        public string CVC { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentResponse>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(
            IPaymentService paymentService,
            IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<CreatePaymentResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentIntent = await _paymentService.CreatePayment(new CreatePaymentArgs {
                Amount = request.Amount,
                CardNumber = request.CardNumber,
                Currency = request.Currency,
                CustomerId = request.CustomerId,
                CVC = request.CVC,
                ExpirationMonth = request.ExpirationMonth,
                ExpirationYear = request.ExpirationYear
            });

            return _mapper.Map<CreatePaymentResponse>(paymentIntent);

        }
    }
}
