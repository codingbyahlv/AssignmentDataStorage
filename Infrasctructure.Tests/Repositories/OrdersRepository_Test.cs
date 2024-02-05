using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class OrdersRepository_Test
{
    private readonly ILogger _logger;
    private readonly CustomersOrdersDbContext _context = new(new DbContextOptionsBuilder<CustomersOrdersDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly OrderEntity demoOrder = new()
    {
        Id = 1,
        Date = DateTime.Now,
        Status = "Confirmed",
        CustomerId = 1,
    };


    [Fact]
    public async Task ExistsAsync_Should_CheckExistOrder_Return_True()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        await ordersRepository.CreateAsync(demoOrder);

        //Act
        bool result = await ordersRepository.ExistsAsync(x => x.Id == demoOrder.Id);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateOrderEntityInDb_Return_OrderEntityWithIdOne()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);

        //Act
        OrderEntity result = await ordersRepository.CreateAsync(demoOrder);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task ReadAllAsync_Should_ReadAllOrders_Return_IEnumerableOfOrderEntities()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);

        //Act
        IEnumerable<OrderEntity> result = await ordersRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<OrderEntity>>(result);
    }

    [Fact]
    public async Task ReadOneAsync_Should_ReadOrderById_Return_OneOrderEntity()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        await ordersRepository.CreateAsync(demoOrder);

        //Act
        OrderEntity result = await ordersRepository.ReadOneAsync(x => x.Id == demoOrder.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoOrder.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingOrder_Return_UpdatedEntity()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        OrderEntity orderEntity = await ordersRepository.CreateAsync(demoOrder);

        //Act
        orderEntity.Status = "Delivered";
        OrderEntity result = await ordersRepository.UpdateAsync(x => x.Id == orderEntity.Id, orderEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(orderEntity.Id, result.Id);
        Assert.Equal("Delivered", result.Status);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneAddressById_Return_True()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        await ordersRepository.CreateAsync(demoOrder);

        //Act
        bool result = await ordersRepository.DeleteAsync(x => x.Id == demoOrder.Id);

        //Assert
        Assert.True(result);
    }
}
