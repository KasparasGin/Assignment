using Assingment.Interfaces;
using Assingment.Models;

namespace Assingment.PaymentGateways
{
    public class PayPalGateway : IPaymentGateway
    {
        public async Task<bool> ProcessPayment(Order order)
        {
            return await Task.FromResult(true);
        }
    }
}
