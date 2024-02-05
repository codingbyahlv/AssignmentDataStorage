﻿using Business.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class OrderUpdateViewModel(IServiceProvider sp, OrderService orderService) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;
    private readonly OrderService _orderService = orderService;
    private OrderDto _order = null!;

    // prop: hold the current order information
    public OrderDto Order
    {
        get { return _order; }
        set
        {
            if (_order != value)
            {
                _order = value;
            }
        }
    }

    //**************************************** DEMO WITHOUT UI ****************************************************//
 
    readonly OrderDto updateOrder = new() { Id = 10, Date = new DateTime(), Status = "delivered", CustomerId = 22 };

    //*************************************************************************************************************//

    // method: update an order in db
    [RelayCommand]
    public async Task UpdateOrder()
    {
        OrderDto result = await _orderService.UpdateOrderAsync(_order);
        if(result != null) 
        {
            NavigateToListView();
        }

    }

    // method: delete an order in db
    [RelayCommand]
    public async Task DeleteOrder(OrderDto order)
    {
        bool result = await _orderService.DeleteOrderAsync(order);
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
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<OrderListViewModel>();
    }
}
