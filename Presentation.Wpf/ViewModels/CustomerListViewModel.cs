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

    [RelayCommand]
    private void CreateDemoCustomer()
    {
        var result = _customerService.CreateCustomerAsync(new CustomerRegistrationDto
        {
            Email = "hampus@mail.se",
            Password = "BytMig123!",
            StreetName = "Storgatan",
            StreetNumber = "12",
            PostalCode = "44360",
            City = "Stenkullen",
            FirstName = "Hampus",
            LastName = "VL",
            PhoneNumber = "0700112233"
        });
    }

    //för att navigera till en viss vy
    [RelayCommand]
    private void NavigateToCustomers()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<StartViewModel>();
    }
}
