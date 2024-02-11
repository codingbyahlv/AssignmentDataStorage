using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class CustomerListViewModel(IServiceProvider sp, CustomerService customerService) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;
    private readonly CustomerService _customerService = customerService;

    [ObservableProperty]
    public IEnumerable<CustomerDto> customers = [];

    // method: read the customers from db
    [RelayCommand]
    public async Task ReadAllCustomers()
    {
        IEnumerable<CustomerDto> result = await _customerService.ReadAllCustomersAsync();
        if (result.Any())
        {
            Customers = result;
        }
    }

    // method: navigation to add customer view
    [RelayCommand]
    private void NavigateToAddView()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerAddViewModel>();
    }

    // method: navigation to update customer view
    [RelayCommand]
    private void NavigateToUpdateView(CustomerDto customer)
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        CustomerUpdateViewModel updateCustomerViewModel = _sp.GetRequiredService<CustomerUpdateViewModel>();
        updateCustomerViewModel.Customer = customer;
        mainViewModel.CurrentViewModel = updateCustomerViewModel;
    }

    // method: navigation to customer view
    [RelayCommand]
    private void NavigateToCustomers()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
    }

    // method: navigation to order view
    [RelayCommand]
    private void NavigateToOrders()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<OrderListViewModel>();
    }

    // method: navigation to products view
    [RelayCommand]
    private void NavigateToProducts()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<ProductListViewModel>();
    }
}











    ////hämtar all info om en customer
    //[RelayCommand]
    //private async Task ReadOneDemoCustomer()
    //{
    //    int id = 11;
    //    CustomerDto result = await _customerService.ReadOneCustomerAllInfoAsync(id);
    //    Debug.WriteLine($"Customer {result.Id} {result.Email} {result.FirstName} {result.StreetName}");
    //}


    //public IEnumerable<CustomerDto> Customers { get; private set; } = [];