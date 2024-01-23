using Business.Factories;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Services
{
    public class CustomerService(ICustomerRepository customersRepository, IAddressesRepository addressesRepository, ICustomerProfilesRepository customerProfilesRepository, ILogger logger)
    {
        private readonly ICustomerRepository _customersRepository = customersRepository;
        private readonly IAddressesRepository _addressesRepository = addressesRepository;
        private readonly ICustomerProfilesRepository _customerProfilesRepository = customerProfilesRepository;
        private readonly ILogger _logger = logger;

        //method: create a new customer
        public async Task<bool>CreateCustomerAsync(CustomerRegistrationDto customer)
        {
            try
            {
                if(!await _customersRepository.ExistsAsync(x => x.Email == customer.Email))
                {
                    CustomerEntity customerEntity = await _customersRepository.CreateAsync(CustomerFactory.Create(customer.Email, customer.Password));
                    AddressEntity addressEntity = await _addressesRepository.CreateAsync(CustomerFactory.Create(customer.StreetName, customer.StreetNumber, customer.PostalCode, customer.City));
                    CustomerProfileEntity customerProfileEntity = await _customerProfilesRepository.CreateAsync(CustomerFactory.Create(customerEntity.Id, customer.FirstName, customer.LastName, customer.PhoneNumber, addressEntity.Id));
                    return true;
                }
            }
            catch (Exception ex) { _logger.Log(ex.Message, "CustomerService - CreateCustomerAsync"); }
            return false;
        }

        //method: read all customers including the ref-tables
        public async Task<IEnumerable<CustomerDto>> ReadAllCustomersAsync()
        {
            try
            {
                IEnumerable<CustomerEntity> customerEntities = await _customersRepository.ReadAllWithAllInfoAsync();
                IEnumerable<CustomerDto> allCustomerDtos = CustomerFactory.Create(customerEntities);
                return allCustomerDtos ?? null!;
            }
            catch (Exception ex) { _logger.Log(ex.Message, "CustomerService - ReadAllCustomersAsync"); }
            return null!;
        }

        //method: read one customer including the ref-tables
        public async Task<CustomerEntity> ReadOneCustomerAsync(int id)
        {
            try
            {
                CustomerEntity customerEntity = await _customersRepository.ReadOneWithAllInfoAsync(x => x.Id == id);
                return customerEntity ?? null!;
            }
            catch (Exception ex) { _logger.Log(ex.Message, "CustomerService - ReadOneCustomer"); }
            return null!;
        }

        //method: update customer information - the user should not be able to update email and password
        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customer)
        {
            try
            {
                int? addressId = customer.AddressId;
                AddressEntity addressEntity = new();
                CustomerProfileEntity customerProfileEntity = new();

                if (customer.StreetName != null && customer.StreetNumber != null && customer.PostalCode != null && customer.City != null)
                {
                    addressEntity = await _addressesRepository.UpdateAsync(
                        x => x.Id == customer.AddressId,
                        CustomerFactory.Create(customer.StreetName, customer.StreetNumber, customer.PostalCode, customer.City));
                    addressId = addressEntity.Id; 
                }

                if (customer.FirstName != null && customer.LastName != null)
                {
                    customerProfileEntity = await _customerProfilesRepository.UpdateAsync(
                        x => x.CustomerId == customer.Id,
                        CustomerFactory.Create(customer.Id, customer.FirstName, customer.LastName, customer.PhoneNumber, addressId));
                }

                CustomerDto customerDto = CustomerFactory.Create(customer, addressEntity, customerProfileEntity);

                return customerDto ?? null!;
            }
            catch (Exception ex) { _logger.Log(ex.Message, "CustomerService - DeleteCustomerAsync"); }
            return null!;
        }

        public async Task<bool> DeleteCustomerAsync(CustomerDto customer)
        {
            try
            {
                return true;
            }
            catch (Exception ex) { _logger.Log(ex.Message, "CustomerService - DeleteCustomerAsync"); }
            return false;
        }
    }
}
