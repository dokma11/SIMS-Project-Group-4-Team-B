using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for NewAccommodationReservationReschedulingRequest.xaml
    /// </summary>
    public partial class NewAccommodationReservationReschedulingRequestViewModel : Window, IObserver
    {
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationReservationService _accommodationReservationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        List<AccommodationReservation> FilteredData = new List<AccommodationReservation>();
        public User User { get; set; }

        NewAccommodationReservationReschedulingRequestView NewAccommodationReservationReschedulingRequestView;

        public NewAccommodationReservationReschedulingRequestViewModel(NewAccommodationReservationReschedulingRequestView newAccommodationReservationReschedulingRequestView, User guest1)
        {
            NewAccommodationReservationReschedulingRequestView = newAccommodationReservationReschedulingRequestView;

            User = guest1;

            _accommodationReservationService = new AccommodationReservationService();
            _accommodationReservationService.Subscribe(this);

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllReservations());

            FilteredData = _accommodationReservationService.FindSuitableUpcomingReservations(User);
            NewAccommodationReservationReschedulingRequestView.myDataGrid.ItemsSource = FilteredData;
        }

        public void makeRequest_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)NewAccommodationReservationReschedulingRequestView.myDataGrid.SelectedItem;
            if (SelectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte rezervaciju koji zelite obrisete.");
                return;
            }
            var accommodationReservationDateView = new AccommodationReservationDateView(SelectedAccommodationReservation.Id, SelectedAccommodationReservation.Accommodation, User);
            accommodationReservationDateView.Show();
            NewAccommodationReservationReschedulingRequestView.Close();
        }

        public void back_Click(object sender, RoutedEventArgs e)
        {
            NewAccommodationReservationReschedulingRequestView.Close();
        }

        public void Update()
        {
            FilteredData.Clear();
            FilteredData = _accommodationReservationService.FindSuitableUpcomingReservations(User);
            NewAccommodationReservationReschedulingRequestView.myDataGrid.ItemsSource = FilteredData;
        }
    }
}
