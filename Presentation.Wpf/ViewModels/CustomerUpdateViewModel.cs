using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class CustomerUpdateViewModel(IServiceProvider sp, CustomerService customerService) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;
    private readonly CustomerService _customerService = customerService;
    private CustomerDto _customer = null!;

    // prop: hold the current customer information
    public CustomerDto Customer
    {
        get { return _customer; }
        set
        {
            if (_customer != value)
            {
                _customer = value;
            }
        }
    }

    // method: updates a customer in db
    [RelayCommand]
    public async Task UpdateCustomer()
    {
        CustomerDto result = await _customerService.UpdateCustomerAsync(_customer);
        if (result != null)
        {
            NavigateToListView();   
        }
    }

    // method: delete a customer in db
    [RelayCommand]
    public async Task DeleteCustomer(CustomerDto customer)
    {
        bool result = await _customerService.DeleteCustomerAsync(customer);
        if (result)
        {
            NavigateToListView();
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
