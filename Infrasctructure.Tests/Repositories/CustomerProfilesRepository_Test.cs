using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class CustomerProfilesRepository_Test
{
    private readonly ILogger _logger;
    private readonly CustomersOrdersDbContext _context = new(new DbContextOptionsBuilder<CustomersOrdersDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly CustomerProfileEntity demoCustomerProfile = new()
    {
        CustomerId = 1,
        FirstName = "Förnamn2",
        LastName = "Efternamn2",
        PhoneNumber = "0700-112233",
        AddressId = 1
    };


    [Fact]
    public async Task ExistsAsync_Should_CheckExistCustomerByEmail_Return_True()
    {
        //Arrange
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        await customerProfilesRepository.CreateAsync(demoCustomerProfile);

        //Act
        bool result = await customerProfilesRepository.ExistsAsync(x => x.CustomerId == demoCustomerProfile.CustomerId);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateCustomerProfileEntityInDb_Return_CustomerProfileEntityWithIdOne()
    {
        //Arrange
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);

        //Act
        CustomerProfileEntity result = await customerProfilesRepository.CreateAsync(demoCustomerProfile);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.CustomerId);
    }

    [Fact]
    public async Task ReadAllAsync_Should_ReadAllCustomerProfiles_Return_IEnumerableOfCustomerProfileEntities()
    {
        //Arrange
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);

        //Act
        IEnumerable<CustomerProfileEntity> result = await customerProfilesRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerProfileEntity>>(result);
    }

    [Fact]
    public async Task ReadOneAsync_Should_ReadCustomerProfileById_Return_OneCustomerProfileEntity()
    {
        //Arrange
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        await customerProfilesRepository.CreateAsync(demoCustomerProfile);

        //Act
        CustomerProfileEntity result = await customerProfilesRepository.ReadOneAsync(x => x.CustomerId == demoCustomerProfile.CustomerId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoCustomerProfile.CustomerId, result.CustomerId);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingCustomerProfile_Return_UpdatedEntity()
    {
        //Arrange
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        CustomerProfileEntity customerProfileEntity = await customerProfilesRepository.CreateAsync(demoCustomerProfile);

        //Act
        customerProfileEntity.FirstName= "Förnamn NYTT";
        CustomerProfileEntity result = await customerProfilesRepository.UpdateAsync(x => x.CustomerId == customerProfileEntity.CustomerId, customerProfileEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(customerProfileEntity.CustomerId, result.CustomerId);
        Assert.Equal("Förnamn NYTT", result.FirstName);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneCustomerProfileById_Return_True()
    {
        //Arrange
        CustomerProfilesRepository customerProfilesRepository = new(_context, _logger);
        await customerProfilesRepository.CreateAsync(demoCustomerProfile);

        //Act
        bool result = await customerProfilesRepository.DeleteAsync(x => x.CustomerId == demoCustomerProfile.CustomerId);

        //Assert
        Assert.True(result);
    }
}
