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
            try
            {
                CustomerEntity customerEntity = new()
                {
                    Email = customer.Email,
                    Password = customer.Password
                };

                customerEntity = _customersRepository.Create(customerEntity);

                AddressEntity addressEntity = new()
                {
                    StreetName = customer.StreetName,
                    StreetNumber = customer.StreetNumber,
                    PostalCode = customer.PostalCode,
                    City = customer.City,
                };

                addressEntity = _addressesRepository.Create(addressEntity);

                CustomerProfileEntity customerProfileEntity = new()
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
            catch (Exception ex) { }
            return false;
        }
        public void ReadAllCustomers()
        {

        }

        public void ReadOneCustomer()
        {

        }


        public void UpdateCustomer(CustomerRegistrationDto customer)
        {
            
        }

        public void DeleteCustomer()
        {

        }
    }
}
