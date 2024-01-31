using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class CustomerListViewModel : ObservableObject
{

    private readonly IServiceProvider _sp;
    private readonly CustomerService _customerService;

    public CustomerListViewModel(IServiceProvider sp, CustomerService customerService)
    {
        _sp = sp;
        _customerService = customerService;
        ReadAllDemoCustomers();
    }

    //FUNKAR EJ 100, UPPDATERAS INTE SOM DEN SKA - ATT JOBBA PÅ!!!!!
    [ObservableProperty]
    public IEnumerable<CustomerDto> customers = [];
    //public IEnumerable<CustomerDto> Customers { get; private set; } = [];

    // method: read the customers from db
    [RelayCommand]
    public async Task ReadAllDemoCustomers()
    {
        IEnumerable<CustomerDto> result = await _customerService.ReadAllCustomersAllInfoAsync();
        if (result.Any())
        {
            Customers = result;
        }
    }

    // method: navigation between views
    [RelayCommand]
    private void NavigateToOverview()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<StartViewModel>();
    }

    // method: navigation between views
    [RelayCommand]
    private void NavigateToAddView()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerAddViewModel>();
    }

    // method: navigation between views
    [RelayCommand]
    private void NavigateToUpdateView(CustomerDto customer)
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        CustomerUpdateViewModel updateCustomerViewModel = _sp.GetRequiredService<CustomerUpdateViewModel>();
        updateCustomerViewModel.Customer = customer;
        mainViewModel.CurrentViewModel = updateCustomerViewModel;
    }

    //[RelayCommand]
    //private void NavigateToOrdersView()
    //{
    //    MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
    //    mainViewModel.CurrentViewModel = _sp.GetRequiredService<ViewOrdersViewModel>();
    //}
}

    ////hämtar all info om en customer
    //[RelayCommand]
    //private async Task ReadOneDemoCustomer()
    //{
    //    int id = 11;
    //    CustomerDto result = await _customerService.ReadOneCustomerAllInfoAsync(id);
    //    Debug.WriteLine($"Customer {result.Id} {result.Email} {result.FirstName} {result.StreetName}");
    //}

