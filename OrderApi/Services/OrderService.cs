using Shared.Models;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        public OrderService() { }
        public async Task Create(Order order)
        {
            // Create order
        }
    }

    public interface IOrderService
    {
        Task Create(Order order);
    }
}
