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
using System.Windows.Shapes;
using Sims2023.Controller;
using System.Collections.ObjectModel;
using System.IO;
using Sims2023.WPF.Views.Guest1Views;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainViewModel : Window
    {
        public User User { get; set; }

        private AccommodationReservationReschedulingController _accommodationReservationReschedulingController;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        Guest1MainView Guest1MainView;

        public Guest1MainViewModel(Guest1MainView guest1MainView,User guest1)
        {
            Guest1MainView = guest1MainView;

            User = guest1;
            _accommodationReservationReschedulingController = new AccommodationReservationReschedulingController();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingController.GetAllReservationReschedulings());
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkForNotifications();
        }

        public void checkForNotifications()
        {
            foreach (AccommodationReservationRescheduling accommodationReservationRescheduling in AccommodationReservationReschedulings)
            {
                if (Notify(accommodationReservationRescheduling))
                {
                    MessageBox.Show($" Vlasnik smestaja {accommodationReservationRescheduling.AccommodationReservation.Accommodation.Name} je promienio status vaseg zahteva za pomeranje rezervacije. Vas zahtev je {accommodationReservationRescheduling.Status}!");
                    accommodationReservationRescheduling.Notified = true;
                    _accommodationReservationReschedulingController.Update(accommodationReservationRescheduling);
                }
            }
        }

        public bool Notify(AccommodationReservationRescheduling accommodationReservationRescheduling)
        {
            if (accommodationReservationRescheduling.Notified == false && accommodationReservationRescheduling.AccommodationReservation.Guest.Id == User.Id && accommodationReservationRescheduling.Status.ToString() != "Pending")
            {
                return true;
            }
            return false;
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
