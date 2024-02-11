using Business.Dtos;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class ProductUpdateViewModel(IServiceProvider sp, ProductService productService) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;
    private readonly ProductService _productService = productService;
    private ProductDto _product = null!;

    // prop: hold the current order information
    public ProductDto Product
    {
        get { return _product; }
        set
        {
            if (_product != value)
            {
                _product = value;
            }
        }
    }

    // method: update an order in db
    [RelayCommand]
    public async Task UpdateProduct()
    {
        ProductDto result = await _productService.UpdateProductAsync(_product);
        if (result != null)
        {
            NavigateToListView();
        }
    }

    // method: delete an order in db
    [RelayCommand]
    public async Task DeleteProduct(ProductDto product)
    {
        bool result = await _productService.DeleteProductAsync(product);
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
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<ProductListViewModel>();
    }



}
