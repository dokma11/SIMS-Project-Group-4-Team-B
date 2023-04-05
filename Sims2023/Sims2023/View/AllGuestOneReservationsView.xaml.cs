using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Sims2023.Model;
using Sims2023.View.Guest1;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for AllGuestOneReservationsView.xaml
    /// </summary>
    public partial class AllGuestOneReservationsView : Window
    {
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationReservationController _accommodationReservationController;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        private AccomodationLocationController _accommodationLocationController;
        public ObservableCollection<AccommodationLocation> AccommodationLocations { get; set; }

        public AllGuestOneReservationsView()
        {
            InitializeComponent();
            DataContext = this;
            _accommodationReservationController = new AccommodationReservationController();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetAllReservations());

            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllAccommodations());

            _accommodationLocationController = new AccomodationLocationController();
            AccommodationLocations = new ObservableCollection<AccommodationLocation>(_accommodationLocationController.GetAllAccommodationLocations());

            AddLocationToAccommodation(AccommodationLocations, Accommodations);
            getAccommodationNameCityOwner(AccommodationReservations,Accommodations);
        }

        private void grading_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (SelectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smestaj koji zelite da ocenite.");
                return;
            }
            var AccommodationAndOwnerGradingView = new AccommodationAndOwnerGradingView(SelectedAccommodationReservation);
            AccommodationAndOwnerGradingView.ShowDialog();
        }

        private void renovation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (SelectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smestaj koji zelite da ocenite.");
                return;
            }
            var AccommodationAndOwnerGradingView = new AccommodationAndOwnerGradingView(SelectedAccommodationReservation);
            AccommodationAndOwnerGradingView.ShowDialog();
        }

        private void AddLocationToAccommodation(ObservableCollection<AccommodationLocation> accommodationLocations, ObservableCollection<Accommodation> accommodations)
        {
            foreach (var accommodation in accommodations)
            {
                foreach (var location in accommodationLocations)
                {
                    if (accommodation.LocationId == location.Id)
                    {
                        accommodation.City = location.City;
                        accommodation.Country = location.Country;
                    }
                }
            }
        }
        private void getAccommodationNameCityOwner(ObservableCollection<AccommodationReservation> accommodationReservations, ObservableCollection<Accommodation> accommodations)
        {
            foreach (AccommodationReservation accommodationReservation in AccommodationReservations)
            {
                findAccommodationInfo(accommodationReservation, accommodations);
            }
        }
        public void findAccommodationInfo(AccommodationReservation accommodationReservation, ObservableCollection<Accommodation> accommodations)
        {
            foreach (Accommodation accommodation in Accommodations)
            {
                if (accommodationReservation.AccommodationId == accommodation.Id)
                {
                    accommodationReservation.AccommodationName = accommodation.Name;
                    accommodationReservation.AccommodationCity = accommodation.City;
                    accommodationReservation.Name = "Pera";
                    accommodationReservation.Surname = "Peric";
                }
            }
        }
    }
}
