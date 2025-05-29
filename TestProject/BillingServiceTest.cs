using Assingment.Enums;
using Assingment.Interfaces;
using Assingment.Models;
using Assingment.Services;
using Moq;

namespace TestProject;

public class BillingServiceTest
{
    Order order = new Order
    {
        OrderNumber = "123456789",
        UserId = new Guid(),
        PayableAmount = 120,
        PaymentMethod = PaymentType.PayPal,
        Description = "this is my description"
    };
    
    [Fact]
    public async Task ProcessOrder_ReturnsReceipt_WhenPaymentSuccessful()
    {
        var mockPaymentGateway = new Mock<IPaymentGateway>();
        mockPaymentGateway.Setup(x => x.ProcessPayment(It.IsAny<Order>())).ReturnsAsync(true);
        var mockPaymentGatewayFactory = new Mock<IPaymentGatewayFactory>();
        mockPaymentGatewayFactory.Setup(x => x.CreateGateway(It.IsAny<PaymentType>()))
            .Returns(mockPaymentGateway.Object);

        var billingService = new BillingService(mockPaymentGatewayFactory.Object);

        var result = await billingService.ProcessOrder(order);
        
        Assert.NotNull(result);
        Assert.IsType<Receipt>(result);
        Assert.Equal(order.OrderNumber, result.OrderNumber);
        Assert.Equal(order.PayableAmount, result.PayingAmount);
    }
    
    [Fact]
    public async Task ProcessOrder_ThrowsException_WhenPaymentFailed()
    {
        var mockPaymentGateway = new Mock<IPaymentGateway>();
        mockPaymentGateway.Setup(x => x.ProcessPayment(It.IsAny<Order>())).ReturnsAsync(false);
        var mockPaymentGatewayFactory = new Mock<IPaymentGatewayFactory>();
        mockPaymentGatewayFactory.Setup(x => x.CreateGateway(It.IsAny<PaymentType>()))
            .Returns(mockPaymentGateway.Object);

        var billingService = new BillingService(mockPaymentGatewayFactory.Object);

        var result = await Assert.ThrowsAsync<Exception>(() =>
            billingService.ProcessOrder(order));
        
        Assert.Equal("Payment failed", result.Message);
    }
    
    [Fact]
    public async Task ProcessOrder_ThrowsException_WhenInvalidPayment()
    {
        var mockPaymentGatewayFactory = new Mock<IPaymentGatewayFactory>();
        mockPaymentGatewayFactory.Setup(x => x.CreateGateway(It.IsAny<PaymentType>()))
            .Throws(new Exception("Unsupported payment gateway"));

        var billingService = new BillingService(mockPaymentGatewayFactory.Object);

        var result = await Assert.ThrowsAsync<Exception>(() =>
            billingService.ProcessOrder(order));
        
        Assert.Equal("Unsupported payment gateway", result.Message);
    }
}