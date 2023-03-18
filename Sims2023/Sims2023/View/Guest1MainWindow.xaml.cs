﻿using System;
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
using System.Windows.Shapes;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainWindow : Window
    {
        public Guest1MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void viewAccommodationClick(object sender, RoutedEventArgs e)
        {
            var AccommodationListWindow = new AccommodationListWindow();
            AccommodationListWindow.Show();
        }
    }
}