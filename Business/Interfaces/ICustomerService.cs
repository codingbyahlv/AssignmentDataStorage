using Infrastructure.Dtos;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerRegistrationDto customer);
        Task<bool> DeleteCustomerAsync(CustomerDto customer);
        //Task<IEnumerable<CustomerDto>> ReadAllCustomersAllInfoAsync();
        Task<IEnumerable<CustomerDto>> ReadAllCustomersAsync();
        //Task<CustomerDto> ReadOneCustomerAllInfoAsync(int id);
        Task<CustomerDto> ReadOneCustomerAsync(int id);
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto customer);
    }
}