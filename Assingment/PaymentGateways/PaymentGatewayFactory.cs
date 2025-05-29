using Assingment.Enums;
using Assingment.Interfaces;

namespace Assingment.PaymentGateways
{
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        public IPaymentGateway CreateGateway(PaymentType type)
        {
            switch (type)
            {
                case PaymentType.PayPal:
                    return new PayPalGateway();

                case PaymentType.Stripe:
                    return new StripeGateway();

                case PaymentType.Credit:
                    return new CreditGateway();

                default:
                    throw new ArgumentException("Unsupported payment gateway");

            }
        }
    }
}
