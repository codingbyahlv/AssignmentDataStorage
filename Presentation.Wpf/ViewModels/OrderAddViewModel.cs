using Business.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class OrderAddViewModel(IServiceProvider sp, OrderService orderService) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;
    private readonly OrderService _orderService = orderService;


    //**************************************** DEMO PRODUCTS TO ORDER ****************************************************//

    readonly List<DemoProduct> productList =
        [
            new DemoProduct { ProductId = "A100", UnitPrice = 100 },
            new DemoProduct { ProductId = "A200", UnitPrice = 200 },
            new DemoProduct { ProductId = "A300", UnitPrice = 300 },
        ];

    //*************************************************************************************************************//

    // instantiate: holds the new orders
    [ObservableProperty]
    private OrderRegistrationDto _newOrder = new();

    //method: creates a new order
    [RelayCommand]
    private async Task CreateOrder()
    {
        
        bool result = await _orderService.CreateOrderAsync(NewOrder, productList);
        if (result)
        {
            NewOrder = new OrderRegistrationDto();
            NavigateToListView();
        }
    }

    //method: navigation back to list view
    [RelayCommand]
    private void NavigateToListView()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<OrderListViewModel>();
    }
}
