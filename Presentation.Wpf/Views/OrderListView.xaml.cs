﻿using Presentation.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation.Wpf.Views
{
    /// <summary>
    /// Interaction logic for OrderListView.xaml
    /// </summary>
    public partial class OrderListView : UserControl
    {
        public OrderListView()
        {
            InitializeComponent();
            Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is OrderListViewModel viewModel)
            {
                // Call the method in your ViewModel
                viewModel.ReadAllOrders();
            }
        }
    }
}