using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class StartViewModel(IServiceProvider sp) : ObservableObject
{
    private readonly IServiceProvider _sp = sp;

    // method: navigation
    [RelayCommand]
    private void NavigateToCustomers()
    {
        MainViewModel mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<CustomerListViewModel>();
    }
}
