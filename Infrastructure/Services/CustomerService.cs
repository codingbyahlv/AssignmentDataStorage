using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public class CustomerService(CustomersRepository customersRepository, AddressesRepository addressesRepository, CustomerProfilesRepository customerProfilesRepository)
    {
        private readonly CustomersRepository _customersRepository = customersRepository;
        private readonly AddressesRepository _addressesRepository = addressesRepository;
        private readonly CustomerProfilesRepository _customerProfilesRepository = customerProfilesRepository;

        public bool CreateCustomer(CustomerRegistrationDto customer)
        {

            var customerEntity = new CustomerEntity
            {
                Email = customer.Email,
                Password = customer.Password
            };

            customerEntity = _customersRepository.Create(customerEntity);

            var addressEntity = new AddressEntity
            {
                StreetName = customer.StreetName,
                StreetNumber = customer.StreetNumber,
                PostalCode = customer.PostalCode,
                City = customer.City,
            };

            addressEntity = _addressesRepository.Create(addressEntity);

            var customerProfileEntity = new CustomerProfileEntity
            {
                CustomerId = customerEntity.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                AddressId = addressEntity.Id,
            };

            customerProfileEntity = _customerProfilesRepository.Create(customerProfileEntity);

            return true;
        }
    }
}
