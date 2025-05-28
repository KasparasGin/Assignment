using Assingment.Models;

namespace Assingment.Interfaces
{
    public interface IPaymentGateway
    {
        Task<bool> ProcessPayment(Order order);
    }
}
