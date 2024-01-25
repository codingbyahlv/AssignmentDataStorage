using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerRegistrationDto customer);
        Task<bool> DeleteCustomerAsync(CustomerDto customer);
        Task<IEnumerable<CustomerDto>> ReadAllCustomersAsync();
        Task<CustomerEntity> ReadCustomerAsync(int id);
        Task<IEnumerable<CustomerDto>> ReadCustomersAsync();
        Task<CustomerEntity> ReadOneCustomerAsync(int id);
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto customer);
    }
}