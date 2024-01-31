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


                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                    services.AddSingleton<CustomerListViewModel>();
                    services.AddSingleton<CustomerListView>();
                    services.AddSingleton<CustomerAddViewModel>();
                    services.AddSingleton<CustomerAddView>();
                    services.AddSingleton<CustomerUpdateViewModel>();
                    services.AddSingleton<CustomerUpdateView>();

                    //services.AddSingleton<ILogger>(new Logger(@"c:\Work\EC\4-datastorage\log.txt"));
                    services.AddSingleton<ILogger>(new Logger());
                    services.AddScoped<CustomerService>();
                    services.AddScoped<CustomersRepository>();
                    services.AddScoped<AddressesRepository>();
                    services.AddScoped<CustomerProfilesRepository>();

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
