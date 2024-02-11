using Infrastructure.Contexts;
using Infrastructure.Dtos;
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

    private readonly CustomerDto demoNewCustomer = new()
    {
        Id = 1,
        FirstName = "Förnamn2",
        LastName = "Efternamn2",
        PhoneNumber = "0700-112233",
        Email = "test@mail.se",
        StreetName = "Testgatan2",
        StreetNumber = "1",
        PostalCode = "12345",
        City = "Teststaden2",
    };

    [Fact]
    public async Task CreateCustomerAsync_Should_CreateCustomerEntityInDb_Return_True()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);

        //Act
        bool result = await customerService.CreateCustomerAsync(demoCustomer);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ReadAllCustomersAsync_Should_ReadAllCustomers_Return_IEnumerableOfCustomerDtos()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);

        //Act
        IEnumerable<CustomerDto> result = await customerService.ReadAllCustomersAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(result);
    }

    [Fact]
    public async Task ReadOneCustomerAsync_Should_ReadOneCustomer_Return_CustomerDto()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);
        await customerService.CreateCustomerAsync(demoCustomer);

        //Act
        CustomerDto result = await customerService.ReadOneCustomerAsync(1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoCustomer.Email, result.Email);
    }

    [Fact]
    public async Task UpdateCustomerAsync_Should_UpdateCustomer_Return_UpdatedCustomerDto()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);
        await customerService.CreateCustomerAsync(demoCustomer);

        //Act
        CustomerDto result = await customerService.UpdateCustomerAsync(demoNewCustomer);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoNewCustomer.FirstName, result.FirstName);
    }

    [Fact]
    public async Task DeleteCustomerAsync_Should_DeleteCustomerFromDb_Return_True()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        AddressesRepository addressesRepository = new(_context, _logger);
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        CustomerService customerService = new(customersRepository, addressesRepository, customerProfilesRepository, _logger);
        await customerService.CreateCustomerAsync(demoCustomer);

        //Act
        bool result = await customerService.DeleteCustomerAsync(demoNewCustomer);

        //Assert
        Assert.True(result);
    }
}
