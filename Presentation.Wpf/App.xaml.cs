using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Wpf.ViewModels;
using Presentation.Wpf.Views;
using Shared.Interfaces;
using Shared.Utils;
using System.Windows;

namespace Presentation.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost builder;

        public App()
        {
                
            builder = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddDbContext<CustomersOrdersDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Work\EC\4-datastorage\Assignment\Infrastructure\Data\ass_db_customers_orders.mdf;Integrated Security=True;Connect Timeout=30"));
                    services.AddDbContext<ProductCatalogContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Work\EC\4-datastorage\Assignment\Infrastructure\Data\ass_db_productCatalog.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                    services.AddSingleton<CustomerListViewModel>();
                    services.AddSingleton<CustomerListView>();
                    services.AddSingleton<CustomerAddViewModel>();
                    services.AddSingleton<CustomerAddView>();
                    services.AddSingleton<CustomerUpdateViewModel>();
                    services.AddSingleton<CustomerUpdateView>();

                    services.AddSingleton<OrderListViewModel>();
                    services.AddSingleton<OrderListView>();
                    services.AddSingleton<OrderAddViewModel>();
                    services.AddSingleton<OrderAddView>();
                    services.AddSingleton<OrderUpdateViewModel>();
                    services.AddSingleton<OrderUpdateView>();

                    services.AddSingleton<ProductListViewModel>();
                    services.AddSingleton<ProductListView>();
                    services.AddSingleton<ProductAddViewModel>();
                    services.AddSingleton<ProductAddView>();
                    services.AddSingleton<ProductUpdateViewModel>();
                    services.AddSingleton<ProductUpdateView>();

                    //services.AddSingleton<ILogger>(new Logger(@"c:\Work\EC\4-datastorage\log.txt"));
                    services.AddSingleton<ILogger>(new Logger());
                    services.AddScoped<CustomerService>();
                    services.AddScoped<CustomersRepository>();
                    services.AddScoped<AddressesRepository>();
                    services.AddScoped<CustomerProfilesRepository>();

                    services.AddScoped<OrderService>();
                    services.AddScoped<OrdersRepository>();
                    services.AddScoped<OrderRowsRepository>();

                    services.AddScoped<ProductService>();
                    services.AddScoped<ProductsRepository>();
                    services.AddScoped<ProductDetailsRepository>();
                    services.AddScoped<BrandsRepository>();
                    services.AddScoped<CategoriesRepository>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            builder.Start();
            MainWindow MainWindow = builder.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }
    }
}
