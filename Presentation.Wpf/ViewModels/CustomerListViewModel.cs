using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

    [ObservableProperty]
    private IEnumerable<CustomerDto> customers = [];



    //skapar en ny customer
    [RelayCommand]
    private void CreateDemoCustomer()
    {

        CustomerRegistrationDto customer = new CustomerRegistrationDto
        {
            Email = "update14@mail.se",
            Password = "BytMig123!",
            StreetName = "Storgatan",
            StreetNumber = "14",
            PostalCode = "44360",
            City = "Stenkullen",
            FirstName = "Ann-Helen",
            LastName = "Lausmaa",
            PhoneNumber = "0700112233"
        };
        var result = _customerService.CreateCustomerAsync(customer);

    }

    //uppdaterar en customer
    [RelayCommand]
    private void UpdateDemoCustomer()
    {
        CustomerDto demoCustomer = new CustomerDto
        {
            Id = 12,
            Email = "update11@mail.se",
            StreetName = "NY12 Storgatan",
            StreetNumber = "12",
            PostalCode = "44360",
            City = "Stenkullen",
            FirstName = "NY12 Ann-Helen",
            LastName = "U12 VL",
            PhoneNumber = "0700112233"
        };

        var result = _customerService.UpdateCustomerAsync(demoCustomer);
    }

    //hämtar all info om alla customers
    [RelayCommand]
    private async Task ReadAllDemoCustomers()
    {
        IEnumerable<CustomerDto> result = await _customerService.ReadAllCustomersAllInfoAsync();
        if (result.Any())
            //Customers = result.ToList();
            Customers = result;
        //var customers = new List<CustomerDto>();
        //foreach (var item in result)
        //{
        //    Customers.Add(item);
        //}
        //foreach (var item in Customers)
        //{
        //    Debug.WriteLine($"Customer {item.Id} {item.Email} {item.FirstName} {item.StreetName}");
        //}
    }

    //hämtar all info om en customer
    [RelayCommand]
    private async Task ReadOneDemoCustomer()
    {
        int id = 11;
        CustomerDto result = await _customerService.ReadOneCustomerAllInfoAsync(id);
        Debug.WriteLine($"Customer {result.Id} {result.Email} {result.FirstName} {result.StreetName}");
    }

    //tar bort en customer
    [RelayCommand]
    private void DeleteDemoCustomer()
    {
        CustomerDto demoCustomer = new CustomerDto
        {
            Id = 12,
            Email = "hampus@mail.se",
            StreetName = "Storgatan",
            StreetNumber = "12",
            PostalCode = "44360",
            City = "Stenkullen",
            FirstName = "Ann-Helen",
            LastName = "VL",
            PhoneNumber = "0700112233"
        };

        var result = _customerService.DeleteCustomerAsync(demoCustomer);
    }

    //för att navigera till en viss vy
    [RelayCommand]
    private void NavigateToOverview()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<StartViewModel>();
    }

    [RelayCommand]
    private void Test()
    {
        Debug.WriteLine("KLICKAD!!!!!!!!!!!!!!!!!!!!!!");
    }






}
