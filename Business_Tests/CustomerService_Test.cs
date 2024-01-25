using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Tests;

public class CustomerService_Test
{
    private readonly ILogger _logger;
    private readonly CustomersOrdersDbContext _context = new(new DbContextOptionsBuilder<CustomersOrdersDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    //CreateCustomerAsync
    [Fact]
    public async Task CreateCustomerAsync_Should_CreateRoleEntityInDb_Return_CustomerEntityWithIdOne()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        CustomerService customerService = new CustomerService(customersRepository);
        CustomerEntity customerEntity = new() { Email = "test@mail.se", Password = "Bytmig123!" };

        //Act
        CustomerEntity result = await customersRepository.CreateCustomerAsync(customerEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }


}
