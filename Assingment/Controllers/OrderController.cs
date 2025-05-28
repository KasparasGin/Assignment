using Assingment.Enums;
using Assingment.Interfaces;
using Assingment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assingment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IBillingService billingService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ReceiptResponse>> PostOrder(OrderRequest orderRequest)
        {
            try
            {
                var order = new Order
                {
                    OrderNumber = orderRequest.OrderNumber,
                    UserId = orderRequest.UserId,
                    PayableAmount = orderRequest.PayableAmount,
                    PaymentMethod = orderRequest.PaymentMethod,
                    Description = orderRequest.Description ?? String.Empty
                };

                var receipt = await billingService.ProcessOrder(order);

                return new ReceiptResponse()
                {
                    OrderNumber = receipt.OrderNumber,
                    PayingAmount = receipt.PayingAmount,
                    ReceiptId = receipt.ReceiptId,
                    TimePayed = receipt.TimePayed
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
