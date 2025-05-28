using System.ComponentModel.DataAnnotations;
using Assingment.Enums;

namespace Assingment.Models
{
    public class OrderRequest
    {
        [Required]
        public string OrderNumber { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal PayableAmount { get; set; }

        [Required]
        [EnumDataType(typeof(PaymentType))]
        public PaymentType PaymentMethod { get; set; }

        public string? Description { get; set; }
    }
}
