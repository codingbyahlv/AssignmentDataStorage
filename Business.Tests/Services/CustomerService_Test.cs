using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Business.Tests.Services;

public class CustomerService_Test
{
    private readonly ILogger _logger;
    private readonly CustomersOrdersDbContext _context = new(new DbContextOptionsBuilder<CustomersOrdersDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    private readonly CustomerRegistrationDto customer = new()
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


    //CreateCustomerAsync
    [Fact]
    public async Task CreateCustomerAsync_Should_CreateRoleEntityInDb_Return_CustomerEntityWithIdOne()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);

        //Act
        bool result = await customerService.CreateCustomerAsync(customer);

        //Assert
        Assert.True(result);
    }

    //DeleteCustomerAsync

    //ReadAllCustomersAsync

    //ReadCustomersAsync

    //ReadCustomerAsync

    //ReadOneCustomerAsync

    //UpdateCustomerAsync
}
