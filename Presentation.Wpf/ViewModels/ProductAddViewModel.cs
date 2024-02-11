using Business.Dtos;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class ProductAddViewModel(IServiceProvider sp, ProductService productService) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;
    private readonly ProductService _productService = productService;


    // instantiate: holds the new product
    [ObservableProperty]
    private ProductRegistrationDto _newProduct = new();

    //method: creates a new product
    [RelayCommand]
    private async Task CreateProduct()
    {

        bool result = await _productService.CreateProductAsync(NewProduct);
        if (result)
        {
            NewProduct = new ProductRegistrationDto();
            NavigateToListView();
        }
    }

    //method: navigation back to list view
    [RelayCommand]
    private void NavigateToListView()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<ProductListViewModel>();
    }
}
