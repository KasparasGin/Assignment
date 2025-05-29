using System.Runtime.CompilerServices;
using Assingment.Controllers;
using Assingment.Enums;
using Assingment.Interfaces;
using Assingment.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
    public class OrderControllerTest
    {
        Receipt receipt = new Receipt
        {
            ReceiptId = new Guid(),
            OrderNumber = "123456789",
            PayingAmount = 120,
            TimePayed = new DateTime()
        };

        OrderRequest orderRequest = new OrderRequest
        {
            OrderNumber = "123456789",
            UserId = new Guid(),
            PayableAmount = 120,
            PaymentMethod = PaymentType.PayPal,
            Description = "this is my description"
        };


        [Fact]
        public async Task PostOrder_ReturnsOk_WhenValidRequest()
        {
            var mockBillingService = new Mock<IBillingService>();

            mockBillingService.Setup(x => x.ProcessOrder(It.IsAny<Order>())).ReturnsAsync(receipt);

            var controller = new OrderController(mockBillingService.Object);

            var result = await controller.PostOrder(orderRequest);

            Assert.NotNull(result);
            Assert.IsType<ReceiptResponse>(result.Value);
        }

        [Fact]
        public async Task PostOrder_ReturnsBadRequest_WhenPaymentFails()
        {
            var mockBillingService = new Mock<IBillingService>();

            mockBillingService.Setup(x => x.ProcessOrder(It.IsAny<Order>())).ThrowsAsync(new Exception("Payment failed"));

            var controller = new OrderController(mockBillingService.Object);

            var result = await controller.PostOrder(orderRequest);

            Assert.NotNull(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(badRequest.Value, "Payment failed");
        }
        
        [Fact]
        public async Task PostOrder_ReturnsBadRequest_WhenInvalidPayment()
        {
            var mockBillingService = new Mock<IBillingService>();

            mockBillingService.Setup(x => x.ProcessOrder(It.IsAny<Order>())).ThrowsAsync(new Exception("Unsupported payment gateway"));

            var controller = new OrderController(mockBillingService.Object);

            var result = await controller.PostOrder(orderRequest);

            Assert.NotNull(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(badRequest.Value, "Unsupported payment gateway");
        }
    }
}