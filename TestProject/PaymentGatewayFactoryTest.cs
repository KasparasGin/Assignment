using Assingment.Enums;
using Assingment.PaymentGateways;

namespace TestProject;

public class PaymentGatewayFactoryTest
{
    [Fact]
    public void CreateGateway_ReturnsCredit_WhenCredit()
    {
        var payment = PaymentType.Credit;

        var gatewayFactory = new PaymentGatewayFactory();

        var result = gatewayFactory.CreateGateway(payment);

        Assert.NotNull(result);
        Assert.IsType<CreditGateway>(result);
    }
    
    [Fact]
    public void CreateGateway_ThrowsException_WhenInvalidPayment()
    {
        var payment = (PaymentType)123;

        var gatewayFactory = new PaymentGatewayFactory();

        var result = Assert.Throws<ArgumentException>(() => gatewayFactory.CreateGateway(payment));
        
        Assert.Equal("Unsupported payment gateway", result.Message);
    }
}