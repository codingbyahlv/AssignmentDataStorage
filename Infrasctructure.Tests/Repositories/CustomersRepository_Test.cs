using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class CustomersRepository_Test
{
    private readonly ILogger _logger;
    private readonly CustomersOrdersDbContext _context = new(new DbContextOptionsBuilder<CustomersOrdersDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    //Base: ExistsAsync
    [Fact]
    public async Task ExistsAsync_Should_CheckExistCustomerByEmail_Return_True()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        CustomerEntity customerEntity = new() { Email = "test@mail.se", Password = "Bytmig123!" };
        await customersRepository.CreateAsync(customerEntity);

        //Act
        bool result = await customersRepository.ExistsAsync(x => x.Email == customerEntity.Email);

        //Assert
        Assert.True(result);
    }

    //BaseRepository: CreateAsync
    [Fact]
    public async Task CreateAsync_Should_CreateCustomerEntityInDb_Return_CustomerEntityWithIdOne()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        CustomerEntity customerEntity = new() { Email = "test@mail.se", Password= "Bytmig123!" };

        //Act
        CustomerEntity result = await customersRepository.CreateAsync(customerEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    //BaseRepository: ReadAllAsync
    [Fact]
    public async Task ReadAllAsync_Should_GetAllCustomers_Return_IEnumerableOfCustomerEntities()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);

        //Act
        IEnumerable<CustomerEntity> result = await customersRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
    }

    //BaseRepository: ReadOneAsync
    [Fact]
    public async Task ReadOneAsync_Should_GetCustomerByEmail_Return_OneCustomerEntity()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        CustomerEntity customerEntity = new() { Email = "test@mail.se", Password = "Bytmig123!" };
        await customersRepository.CreateAsync(customerEntity);

        //Act
        CustomerEntity result = await customersRepository.ReadOneAsync(x => x.Email == customerEntity.Email);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(customerEntity.Email, result.Email);
    }

    //BaseRepository: UpdateAsync
    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingCustomer_Return_UpdatedEntity()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        CustomerEntity customerEntity = new() { Email = "test@mail.se", Password = "Bytmig123!" };
        customerEntity = await customersRepository.CreateAsync(customerEntity);

        //Act
        customerEntity.Email = "test2@mail.se";
        var result = await customersRepository.UpdateAsync(x => x.Id == customerEntity.Id, customerEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(customerEntity.Email, result.Email);
        Assert.Equal("test2@mail.se", result.Email);
    }

    //BaseRepository: DeleteAsync
    [Fact]
    public async Task DeleteAsync_Should_RemoveOneCustomerByEmail_Return_True()
    {
        //Arrange
        CustomersRepository customersRepository = new(_context, _logger);
        CustomerEntity customerEntity = new() { Email = "test@mail.se", Password = "Bytmig123!" };
        await customersRepository.CreateAsync(customerEntity);

        //Act
        var result = await customersRepository.DeleteAsync(x => x.Email == customerEntity.Email);

        //Assert
        Assert.True(result);
    }

    ////Customer: ReadAllWithAllInfoAsync
    //[Fact]
    //public async Task ReadAllWithAllInfoAsync_Should_GetAllCustomers_Return_IEnumerableOfCustomerEntities()
    //{
    //    //Arrange
    //    CustomersRepository customersRepository = new(_context, _logger);

    //    //Act
    //    IEnumerable<CustomerEntity> result = await customersRepository.ReadAllWithAllInfoAsync();

    //    //Assert
    //    Assert.NotNull(result);
    //    Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
    //}

    ////Customer: ReadOneWithAllInfoAsync
    //[Fact]
    //public async Task ReadOneWithAllInfoAsync_Should_GetCustomerById_Return_OneCustomerEntity()
    //{
    //    //Arrange
    //    CustomersRepository customersRepository = new(_context, _logger);
    //    CustomerEntity customerEntity = new() { Id=1, Email = "test@mail.se", Password = "Bytmig123!" };
    //    await customersRepository.CreateAsync(customerEntity);

    //    //Act
    //    CustomerEntity result = await customersRepository.ReadOneWithAllInfoAsync(x => x.Id == customerEntity.Id);

    //    //Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(customerEntity.Id, result.Id);
    //}


























}
