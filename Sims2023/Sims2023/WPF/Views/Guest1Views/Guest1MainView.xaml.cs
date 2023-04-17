using Sims2023.Domain.Models;
using Sims2023.Model;
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
using Sims2023.Controller;
using System.Collections.ObjectModel;
using System.IO;
using Sims2023.WPF.ViewModels.Guest1ViewModel;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainView : Window
    {
        Guest1MainViewModel Guest1MainViewModel;
        public User User { get; set; }

        public Guest1MainView(User guest1)
        {
            InitializeComponent();
            Guest1MainViewModel = new Guest1MainViewModel(this, guest1);
            DataContext = Guest1MainViewModel;

            User = guest1;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Guest1MainViewModel.Window_Loaded(sender, e);
        }

        private void VewAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationListView = new AccommodationListView(User);
            AccommodationListView.Show();
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonGrading_Click(object sender, RoutedEventArgs e)
        {
            var AllGuestOneReservationsView = new AllGuestOneReservationsView(User);
            AllGuestOneReservationsView.Show();
        }

        private void AccommodationCancellation_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationReservationCancellationView = new AccommodationReservationCancellationView(User);
            AccommodationReservationCancellationView.Show();
        }

        private void buttonReservationMove_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationReservationReschedulingView = new AccommodationReservationReschedulingView(User);
            AccommodationReservationReschedulingView.Show();
        }
    }
}
