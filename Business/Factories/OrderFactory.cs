using Business.Dtos;
using Infrastructure.Entities;

namespace Business.Factories;

public static class OrderFactory
{
    public static OrderEntity Create(string status, int customerId)
    {
        return new OrderEntity 
        { 
            Date = DateTime.Now,
            Status = status,
            CustomerId = customerId
        };
    }

    public static OrderEntity Create(OrderDto order)
    {
        return new OrderEntity
        {
            Id = order.Id,
            Date = order.Date,
            Status = order.Status,
            CustomerId = order.CustomerId
        };
    }

    public static OrderRowEntity Create(int rowId, int orderId, string productId, int productQty, decimal unitPrice) 
    {
        return new OrderRowEntity
        {
            RowId = rowId,
            OrderId = orderId,
            ProductId = productId,
            ProductQty = productQty,
            UnitPrice = unitPrice
        };
    }

  
    public static OrderDto Create(int id, DateTime date, string status, int customerId)
    {
        return new OrderDto
        {   
            Id = id,
            Date = date,
            Status = status,
            CustomerId = customerId
        };
    }

    public static OrderDto Create(OrderEntity orderEntity)
    {
        return new OrderDto
        {
            Id = orderEntity.Id,
            Date = orderEntity.Date,
            Status = orderEntity.Status,
            CustomerId = orderEntity.CustomerId
        };
    }


    public static IEnumerable<OrderDto> Create(IEnumerable<OrderEntity> orderEntities)
    {
        List<OrderDto> dtoList = [];
        foreach (OrderEntity orderEntity in orderEntities)
        {
            dtoList.Add(Create(orderEntity));
        }
        return dtoList;
    }






    //skapa produkt till order



    //lista med ordrar?



    //lista med orderns produkter?













}
