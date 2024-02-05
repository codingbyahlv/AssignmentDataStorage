using Infrastructure.Dtos;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerRegistrationDto customer);
        Task<IEnumerable<CustomerDto>> ReadAllCustomersAsync();
        Task<CustomerDto> ReadOneCustomerAsync(int id);
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto customer);
        Task<bool> DeleteCustomerAsync(CustomerDto customer);
    }
}