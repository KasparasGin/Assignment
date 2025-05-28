using Assingment.Enums;

namespace Assingment.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public Guid UserId { get; set; }
        public decimal PayableAmount { get; set; }
        public PaymentType PaymentMethod { get; set; }
        public string Description { get; set; }
    }
}
