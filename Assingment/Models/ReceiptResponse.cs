namespace Assingment.Models
{
    public class ReceiptResponse
    {
        public Guid ReceiptId { get; set; }
        public string OrderNumber { get; set; }
        public decimal PayingAmount { get; set; }
        public DateTime TimePayed { get; set; }
    }
}
