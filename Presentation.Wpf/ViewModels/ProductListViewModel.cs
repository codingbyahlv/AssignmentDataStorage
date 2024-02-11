using Business.Dtos;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class ProductListViewModel(IServiceProvider sp, ProductService productService) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;
    private readonly ProductService _productService = productService;

    [ObservableProperty]
    public IEnumerable<ProductDto> products = [];

    // method: read the products from db
    [RelayCommand]
    public async Task ReadAllProducts()
    {
        IEnumerable<ProductDto> result = await _productService.ReadAllProductsAsync();
        if (result.Any())
        {
            Products = result;
        }
    }

    // method: navigation to add order view
    [RelayCommand]
    private void NavigateToAddView()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<ProductAddViewModel>();
    }

    // method: navigation to update order view
    [RelayCommand]
    private void NavigateToUpdateView(ProductDto product)
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        ProductUpdateViewModel updateProductViewModel = _sp.GetRequiredService<ProductUpdateViewModel>();
        updateProductViewModel.Product = product;
        mainViewModel.CurrentViewModel = updateProductViewModel;
    }

    // method: navigation to customer view
    [RelayCommand]
    private void NavigateToCustomers()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
    }

    //method: navigation to products view
    [RelayCommand]
    private void NavigateToOrders()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<OrderListViewModel>();
    }
}
