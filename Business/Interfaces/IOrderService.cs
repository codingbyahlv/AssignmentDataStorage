using Business.Dtos;

namespace Business.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderRegistrationDto order, List<DemoProduct> productList);
        Task<IEnumerable<OrderDto>> ReadAllOrdersAsync();
        Task<OrderDto> ReadOneOrderAsync(int id);
        Task<OrderDto> UpdateOrderAsync(OrderDto order);
        Task<bool> DeleteOrderAsync(OrderDto order);
    }
}