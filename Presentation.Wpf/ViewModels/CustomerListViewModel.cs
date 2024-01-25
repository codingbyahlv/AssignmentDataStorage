using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Dtos;
using Infrastructure.Services;

namespace Presentation.Wpf.ViewModels;

public class CustomerListViewModel(IServiceProvider sp, CustomerService customerService) : ObservableObject
{

    private readonly IServiceProvider _sp = sp;
    private readonly CustomerService _customerService = customerService;

    private void CreateDemoCustomer()
    {
        //bool result = _customerService.CreateCustomer(new CustomerRegistrationDto
        //{
        //    Email = "molly@mail.se",
        //    Password = "BytMig123!",
        //    StreetName = "Storgatan",
        //    StreetNumber = "12",
        //    PostalCode = "44360",
        //    City = "Stenkullen",
        //    FirstName = "Molly",
        //    LastName = "VL",
        //    PhoneNumber = "0700112233"
        //});
    }
}
