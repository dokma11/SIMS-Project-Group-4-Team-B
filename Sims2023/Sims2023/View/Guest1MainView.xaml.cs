﻿using Sims2023.Model;
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
using System.Windows.Shapes;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainView : Window
    {
        public Guest1MainView(User user)
        {
            InitializeComponent();
            DataContext = this;
        }

        private void VewAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationListView = new AccommodationListView();
            AccommodationListView.Show();
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonGrading_Click(object sender, RoutedEventArgs e)
        {
            var AllGuestOneReservationsView = new AllGuestOneReservationsView();
            AllGuestOneReservationsView.Show();
        }
    }
}
