using AutoMapper;
using MediatR;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Payments.Queries.GetPayment.Contracts.Response;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Payments.Queries.GetPayment
{
    public class GetPaymentQuery : IRequest<GetPaymentResponse>
    {
        /// <summary>
        /// The Payment Id
        /// </summary>
        public string Id { get; set; }
    }

    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, GetPaymentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public GetPaymentQueryHandler(
            IMapper mapper,
            IPaymentService paymentService)
        {
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<GetPaymentResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var paymentIntent = await _paymentService.RetrievePayment(request.Id);

            return _mapper.Map<GetPaymentResponse>(paymentIntent);

        }
    }

}
