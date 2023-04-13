using Sims2023.Domain.Models;
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
using System.Collections.ObjectModel;
using System.IO;
using Sims2023.WPF.Views.Guest1Views;
using Sims2023.Application.Services;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainViewModel : Window
    {
        public User User { get; set; }

        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        Guest1MainView Guest1MainView;

        public Guest1MainViewModel(Guest1MainView guest1MainView,User guest1)
        {
            Guest1MainView = guest1MainView;

            User = guest1;
            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _accommodationReservationReschedulingService.checkForNotifications(User);
        }

        public void VewAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationListView = new AccommodationListView(User);
            AccommodationListView.Show();
        }

        public void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            Guest1MainView.Close();
        }

        public void buttonGrading_Click(object sender, RoutedEventArgs e)
        {
            var AllGuestOneReservationsView = new AllGuestOneReservationsView(User);
            AllGuestOneReservationsView.Show();
        }

        public void AccommodationCancellation_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationReservationCancellationView = new AccommodationReservationCancellationView(User);
            AccommodationReservationCancellationView.Show();
        }

        public void buttonReservationMove_Click(object sender, RoutedEventArgs e)
        {
            var AccommodationReservationReschedulingView = new AccommodationReservationReschedulingView(User);
            AccommodationReservationReschedulingView.Show();
        }
    }
}
