using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class OrderRowsRepository_Test
{
    private readonly ILogger _logger;
    private readonly CustomersOrdersDbContext _context = new(new DbContextOptionsBuilder<CustomersOrdersDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly OrderRowEntity demoOrderRow = new()
    {
        RowId = 1,
        OrderId = 2,
        ProductId = "A100",
        ProductQty = 1,
        UnitPrice = 100,
    };


    [Fact]
    public async Task ExistsAsync_Should_CheckExistOrderRow_Return_True()
    {
        //Arrange
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        await orderRowsRepository.CreateAsync(demoOrderRow);

        //Act
        bool result = await orderRowsRepository.ExistsAsync(x => x.RowId == demoOrderRow.RowId && x.OrderId == demoOrderRow.OrderId);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateOrderRowEntityInDb_Return_OrderRowEntityWithIdOne()
    {
        //Arrange
        OrderRowsRepository orderRowsRepository = new(_context, _logger);

        //Act
        OrderRowEntity result = await orderRowsRepository.CreateAsync(demoOrderRow);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.RowId);
        Assert.Equal(2, result.OrderId);
    }

    [Fact]
    public async Task ReadAllAsync_Should_ReadAllOrdersRows_Return_IEnumerableOfOrderRowEntities()
    {
        //Arrange
        OrderRowsRepository orderRowsRepository = new(_context, _logger);

        //Act
        IEnumerable<OrderRowEntity> result = await orderRowsRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<OrderRowEntity>>(result);
    }

    [Fact]
    public async Task ReadOneAsync_Should_ReadOrderById_Return_OneOrderEntity()
    {
        //Arrange
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        await orderRowsRepository.CreateAsync(demoOrderRow);

        //Act
        OrderRowEntity result = await orderRowsRepository.ReadOneAsync(x => x.RowId == demoOrderRow.RowId && x.OrderId == demoOrderRow.OrderId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoOrderRow.RowId, result.RowId);
        Assert.Equal(demoOrderRow.OrderId, result.OrderId);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingOrderRow_Return_UpdatedEntity()
    {
        //Arrange
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        OrderRowEntity orderRowEntity = await orderRowsRepository.CreateAsync(demoOrderRow);

        //Act
        orderRowEntity.UnitPrice = 200;
        OrderRowEntity result = await orderRowsRepository.UpdateAsync(x => x.RowId == demoOrderRow.RowId && x.OrderId == demoOrderRow.OrderId, orderRowEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(orderRowEntity.RowId, result.RowId);
        Assert.Equal(200, result.UnitPrice);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneAddressById_Return_True()
    {
        //Arrange
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        await orderRowsRepository.CreateAsync(demoOrderRow);

        //Act
        bool result = await orderRowsRepository.DeleteAsync(x => x.RowId == demoOrderRow.RowId && x.OrderId == demoOrderRow.OrderId);

        //Assert
        Assert.True(result);
    }
}
