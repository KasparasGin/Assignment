using Assingment.Models;

namespace Assingment.Interfaces
{
    public interface IBillingService
    {
        public Task<Receipt> ProcessOrder(Order order);
    }
}
