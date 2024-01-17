﻿using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Wpf.ViewModels;
using Presentation.Wpf.Views;
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

                    services.AddSingleton<StartViewModel>();
                    services.AddSingleton<StartView>();

                    services.AddSingleton<CustomerListViewModel>();
                    services.AddSingleton<CustomerListView>();
                
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
