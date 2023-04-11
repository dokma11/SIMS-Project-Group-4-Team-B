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
using Sims2023.View.Guest1;
using Sims2023.Controller;
using System.Collections.ObjectModel;
using System.IO;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainView : Window
    {
        public User User { get; set; }

        private AccommodationReservationReschedulingController _accommodationReservationReschedulingController;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }
        public Guest1MainView(User guest1)
        {
            InitializeComponent();
            DataContext = this;

            User = guest1;
            _accommodationReservationReschedulingController = new AccommodationReservationReschedulingController();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingController.GetAllReservationReschedulings());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkForNotifications();
        }

        private void checkForNotifications()
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
