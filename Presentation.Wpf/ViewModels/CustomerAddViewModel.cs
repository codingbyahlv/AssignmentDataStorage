using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class CustomerAddViewModel(IServiceProvider serviceProvider, CustomerService customerService) : ObservableObject
{
    private readonly IServiceProvider _sp = serviceProvider;
    private readonly CustomerService _customerService = customerService;

    // instantiate: holds the new contact
    [ObservableProperty]
    private CustomerRegistrationDto _newCustomer = new();

    // method: creates a new customer
    [RelayCommand]
    private async Task CreateCustomer()
    {
        bool result = await _customerService.CreateCustomerAsync(NewCustomer);
        if (result) 
        {
            NavigateToListView();
            NewCustomer = new CustomerRegistrationDto();
        }
    }

    // method: navigation back to list view
    [RelayCommand]
    private void NavigateToListView()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
    }
}
