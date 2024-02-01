using Business.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class OrderListViewModel(IServiceProvider sp, OrderService orderService) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;
    private readonly OrderService _orderService = orderService;

    [ObservableProperty]
    public IEnumerable<OrderDto> orders = [];

    // method: read the orders from db
    [RelayCommand]
    public async Task ReadAllOrders()
    {
        IEnumerable<OrderDto> result = await _orderService.ReadAllOrdersAsync();
        if (result.Any())
        {
            Orders = result;
        }
    }

    // method: navigation to add order view
    [RelayCommand]
    private void NavigateToAddView()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<OrderAddViewModel>();
    }

    // method: navigation to update order view
    [RelayCommand]
    private void NavigateToUpdateView(OrderDto order)
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        OrderUpdateViewModel updateOrderViewModel = _sp.GetRequiredService<OrderUpdateViewModel>();
        updateOrderViewModel.Order = order;
        mainViewModel.CurrentViewModel = updateOrderViewModel;
    }

    // method: navigation to customer view
    [RelayCommand]
    private void NavigateToCustomers()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
    }

    // method: navigation to products view
    //[RelayCommand]
    // private void NavigateToProducts()
    // {
    //     MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
    //     mainViewModel.CurrentViewModel = _sp.GetRequiredService<ProductListViewModel>();
    // }

}






//// method: delete an order in db
//[RelayCommand]
//public async Task DeleteOrder(OrderDto order)
//{
//    bool result = await _orderService.DeleteOrderAsync(deleteOrder);
//    if(result)
//    {
//        NavigateToListView();
//    }
//}





////method: navigation back to list view
//[RelayCommand]
//private void NavigateToListView()
//{
//    MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
//    mainViewModel.CurrentViewModel = _sp.GetRequiredService<OrderListViewModel>();
//}




//COMMENT BACK WHEN NO DEMO!!!!!!!!!!!!!!!!!
// instantiate: holds the new contact
//[ObservableProperty]
//private OrderRegistrationDto _newOrder = new();

////method: creates a new order
//[RelayCommand]
//private async Task CreateOrder()
//{
//    bool result = await _orderService.CreateOrderAsync(NewOrder, productList);
//    if (result)
//    {
//        NavigateToListView();
//    }
//}



