using Assingment.Enums;

namespace Assingment.Interfaces
{
    public interface IPaymentGatewayFactory
    {
        public IPaymentGateway CreateGateway(PaymentType type);
    }
}
