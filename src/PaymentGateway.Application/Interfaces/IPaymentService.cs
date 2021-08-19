using PaymentGateway.Application.Payments.Command.CreatePayment;
using PaymentGateway.Domain.DomainModel;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentIntent> CreatePayment(CreatePaymentArgs args);

        Task<PaymentIntent> RetrievePayment(string id);
    }
}
