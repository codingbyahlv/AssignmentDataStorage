using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Business.Tests.Services;

public class OrderService_Test
{
    private readonly ILogger _logger;
    private readonly CustomersOrdersDbContext _context = new(new DbContextOptionsBuilder<CustomersOrdersDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly CustomerRegistrationDto demoCustomer = new()
    {
        FirstName = "Förnamn",
        LastName = "Efternamn",
        PhoneNumber = "0700-112233",
        Email = "test@mail.se",
        StreetName = "Testgatan",
        StreetNumber = "1",
        PostalCode = "12345",
        City = "Teststaden",
        Password = "BytMig123!"
    };
    private readonly OrderRegistrationDto demoRegDto = new()
    {
        Status = "Confirmed",
        CustomerId = 1,
    };

    private readonly OrderDto demoOrderDto = new()
    {
        Id = 1,
        Date = DateTime.Now,
        Status = "In progress",
        CustomerId = 1
    };
    private readonly List<DemoProduct> productList =
    [
        new DemoProduct { ProductId = "A100", UnitPrice = 100 },
        new DemoProduct { ProductId = "A200", UnitPrice = 200 },
        new DemoProduct { ProductId = "A300", UnitPrice = 300 },
    ];

    [Fact]
    public async Task CreateOrderAsync_Should_CreateOrderEntityInDb_Return_True()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        OrderService orderService = new(orderRowsRepository, ordersRepository, customersRepository, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);
        await customerService.CreateCustomerAsync(demoCustomer);


        //Act
        bool result = await orderService.CreateOrderAsync(demoRegDto, productList);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ReadAllOrdersAsync_Should_ReadAllOrders_Return_IEnumerableOfOrderDtos()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        CustomersRepository customersRepository = new(_context, _logger);
        OrderService orderService = new(orderRowsRepository, ordersRepository, customersRepository, _logger);

        //Act
        IEnumerable<OrderDto> result = await orderService.ReadAllOrdersAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<OrderDto>>(result);
    }

    [Fact]
    public async Task ReadOneOrderAsync_Should_ReadOneOrder_Return_OrderDto()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        OrderService orderService = new(orderRowsRepository, ordersRepository, customersRepository, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);
        await customerService.CreateCustomerAsync(demoCustomer);
        await orderService.CreateOrderAsync(demoRegDto, productList);

        //Act
        OrderDto result = await orderService.ReadOneOrderAsync(1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoOrderDto.Id, result.Id);
    }

    [Fact]
    public async Task UpdateOrderAsync_Should_UpdateOrder_Return_UpdatedOrderrDto()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        OrderService orderService = new(orderRowsRepository, ordersRepository, customersRepository, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);
        await customerService.CreateCustomerAsync(demoCustomer);
        await orderService.CreateOrderAsync(demoRegDto, productList);

        //Act
        OrderDto result = await orderService.UpdateOrderAsync(demoOrderDto);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoOrderDto.Id, result.Id);
    }

    [Fact]
    public async Task DeleteOrderAsync_Should_DeleteOrderFromDb_Return_True()
    {
        //Arrange
        OrdersRepository ordersRepository = new(_context, _logger);
        OrderRowsRepository orderRowsRepository = new(_context, _logger);
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        OrderService orderService = new(orderRowsRepository, ordersRepository, customersRepository, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);
        await customerService.CreateCustomerAsync(demoCustomer);
        await orderService.CreateOrderAsync(demoRegDto, productList);

        //Act
        bool result = await orderService.DeleteOrderAsync(demoOrderDto);

        //Assert
        Assert.True(result);
    }
}
