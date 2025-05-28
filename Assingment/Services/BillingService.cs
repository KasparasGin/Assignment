using Assingment.Interfaces;
using Assingment.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Assingment.Services
{
    public class BillingService(IPaymentGatewayFactory paymentGatewayFactory): IBillingService
    {
        public async Task<Receipt> ProcessOrder(Order order)
        {
            var paymentGateway = paymentGatewayFactory.CreateGateway(order.PaymentMethod);
            
            var result = await paymentGateway.ProcessPayment(order);

            if (!result) throw new Exception("Payment failed");

            return new Receipt()
            {
                ReceiptId = Guid.NewGuid(),
                OrderNumber = order.OrderNumber,
                PayingAmount = order.PayableAmount,
                TimePayed = DateTime.Now
            };
        }
    }
}
