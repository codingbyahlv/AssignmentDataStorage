using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity Create(string email, string password)
    {
        return new CustomerEntity
        {
            Email = email,
            Password = password
        };
    }

    public static AddressEntity Create(string streetName, string streetNumber, string postalCode, string city)
    {
        return new AddressEntity
        {
            StreetName = streetName,
            StreetNumber = streetNumber,
            PostalCode = postalCode,
            City = city
        };
    }

    public static CustomerProfileEntity Create(int customerId, string firstName, string lastName, string? phoneNumber, int? adressId)
    {
        return new CustomerProfileEntity
        {
            CustomerId = customerId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            AddressId = adressId
        };
    }

    public static CustomerDto Create(CustomerEntity customerEntity)
    {
        return new CustomerDto
        {
            Id = customerEntity.Id,
            Email = customerEntity.Email,

            FirstName = customerEntity.CustomerProfile?.FirstName,
            LastName = customerEntity.CustomerProfile?.LastName,
            PhoneNumber = customerEntity.CustomerProfile?.PhoneNumber,
            AddressId = customerEntity.CustomerProfile?.Address?.Id,

            StreetName = customerEntity.CustomerProfile?.Address?.StreetName,
            StreetNumber = customerEntity.CustomerProfile?.Address?.StreetNumber,
            PostalCode = customerEntity.CustomerProfile?.Address?.PostalCode,
            City = customerEntity.CustomerProfile?.Address?.City,
        };
    }

    public static CustomerDto Create(CustomerDto customerDto, AddressEntity addressEntity, CustomerProfileEntity customerProfilesEntity)
    {
        return new CustomerDto
        {
            Id = customerDto.Id,
            Email = customerDto.Email,
            
            FirstName = customerProfilesEntity.FirstName,
            LastName = customerProfilesEntity.LastName,
            PhoneNumber = customerProfilesEntity.PhoneNumber,
            AddressId = addressEntity.Id,
            
            StreetName = addressEntity.StreetName,
            StreetNumber = addressEntity.StreetNumber,
            PostalCode = addressEntity.PostalCode,
            City = addressEntity.City,
        };
    }

    public static IEnumerable<CustomerDto> Create(IEnumerable<CustomerEntity> customerEntities)
    {
        List<CustomerDto> dtoList = [];
        foreach (CustomerEntity customerEntity in customerEntities)
        {
            dtoList.Add(Create(customerEntity));
        }
        return dtoList;
    }
}
