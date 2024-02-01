using Business.Dtos;
using Business.Factories;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Shared.Interfaces;

namespace Infrastructure.Services;

public class OrderService(OrderRowsRepository orderRowsRepository, OrdersRepository ordersRepository, CustomersRepository customersRepository, ILogger logger)
{
    private readonly OrderRowsRepository _orderRowsRepository = orderRowsRepository;
    private readonly OrdersRepository _ordersRepository = ordersRepository;
    private readonly CustomersRepository _customersRepository = customersRepository;
    private readonly ILogger _logger = logger;

    // method: create a new order
    public async Task<bool> CreateOrderAsync(OrderRegistrationDto order, List<DemoProduct> productList)
    {
        try
        {
            if (await _customersRepository.ExistsAsync(x => x.Id == order.CustomerId))
            {
                OrderEntity orderEntity = await _ordersRepository.CreateAsync(OrderFactory.Create(order.Status, order.CustomerId));

                int productQty = 1;
                for (int i = 0; i < productList.Count; i++)
                {
                    DemoProduct product = productList[i];
                    OrderRowEntity orderRowEntity = await _orderRowsRepository.CreateAsync(OrderFactory.Create(i, orderEntity.Id, product.ProductId, productQty, product.UnitPrice));
                }
                return true;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "OrderService - CreateOrderAsync"); }
        return false;
    }

    // method: read all orders
    public async Task<IEnumerable<OrderDto>> ReadAllOrdersAsync()
    {
        try
        {
            IEnumerable<OrderEntity> orderEntities = await _ordersRepository.ReadAllAsync();
            IEnumerable<OrderDto> orderDtos = OrderFactory.Create(orderEntities);
            return orderDtos;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "OrderService - RealAllOrdersAsync"); }
        return null!;
    }





    // method: read one order incl the orderRows
   




    //method: update order
    public async Task<bool> UpdateOrderAsync(OrderDto order)
    {
        try
        {
            //the user should only be abel to update the status
            OrderEntity orderEntity = await _ordersRepository.UpdateAsync(x => x.Id == order.Id, OrderFactory.Create(order));
        }
        catch (Exception ex) { _logger.Log(ex.Message, "OrderService - UpdateOrderAsync"); }
        return false;
    }






    // method: update orderRows in order






    //method: delete order
    public async Task<bool> DeleteOrderAsync(OrderDto order)
    {
        try
        {
            if (await _ordersRepository.ExistsAsync(x => x.Id == order.Id))
            {
                bool orderResult = await _ordersRepository.DeleteAsync(x => x.Id == order.Id);

                //EV LÄGGA IN ATT TA BORT KOPPLADE ORDER ROWS OCKSÅ

                if (orderResult)
                {
                    return true;
                }
            };
        }
        catch (Exception ex) { _logger.Log(ex.Message, "OrderService - UpdateOrderAsync"); }
        return false;
    }








}
