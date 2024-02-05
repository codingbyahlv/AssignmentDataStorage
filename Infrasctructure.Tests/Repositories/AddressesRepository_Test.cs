using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class AddressesRepository_Test
{
    private readonly ILogger _logger;
    private readonly CustomersOrdersDbContext _context = new(new DbContextOptionsBuilder<CustomersOrdersDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly AddressEntity demoAddress = new()
    {
        Id = 1,
        StreetName = "Förnamn2",
        StreetNumber = "3",
        PostalCode = "Efternamn2",
        City = "0700-112233",
    };


    [Fact]
    public async Task ExistsAsync_Should_CheckExistAddress_Return_True()
    {
        //Arrange
        AddressesRepository addressesRepository = new(_context, _logger);
        await addressesRepository.CreateAsync(demoAddress);

        //Act
        bool result = await addressesRepository.ExistsAsync(x => x.StreetName == demoAddress.StreetName && x.StreetNumber == demoAddress.StreetNumber && x.PostalCode == demoAddress.PostalCode && x.City == demoAddress.City);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateAddressEntityInDb_Return_AddressEntityWithIdOne()
    {
        //Arrange
        AddressesRepository addressesRepository = new(_context, _logger);

        //Act
        AddressEntity result = await addressesRepository.CreateAsync(demoAddress);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task ReadAllAsync_Should_ReadAllAddresses_Return_IEnumerableOfAddressEntities()
    {
        //Arrange
        AddressesRepository addressesRepository = new(_context, _logger);

        //Act
        IEnumerable<AddressEntity> result = await addressesRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AddressEntity>>(result);
    }

    [Fact]
    public async Task ReadOneAsync_Should_ReadAddressesByEmail_Return_OneAddressEntity()
    {
        //Arrange
        AddressesRepository addressesRepository = new(_context, _logger);
        await addressesRepository.CreateAsync(demoAddress);

        //Act
        AddressEntity result = await addressesRepository.ReadOneAsync(x => x.Id == demoAddress.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoAddress.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingAddress_Return_UpdatedEntity()
    {
        //Arrange
        AddressesRepository addressesRepository = new(_context, _logger);
        AddressEntity addressEntity = await addressesRepository.CreateAsync(demoAddress);

        //Act
        addressEntity.StreetName = "Gatunamn NYTT";
        AddressEntity result = await addressesRepository.UpdateAsync(x => x.Id == addressEntity.Id, addressEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(addressEntity.Id, result.Id);
        Assert.Equal("Gatunamn NYTT", result.StreetName);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneAddressById_Return_True()
    {
        //Arrange
        AddressesRepository addressesRepository = new(_context, _logger);
        await addressesRepository.CreateAsync(demoAddress);

        //Act
        bool result = await addressesRepository.DeleteAsync(x => x.Id == demoAddress.Id);

        //Assert
        Assert.True(result);
    }
}
