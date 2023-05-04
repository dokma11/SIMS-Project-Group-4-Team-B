using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
using System;
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

        private AccommodationReservationService _accommodationReservationService;

        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        List<AccommodationReservation> FilteredData = new List<AccommodationReservation>();
        public User User { get; set; }

        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        NewAccommodationReservationReschedulingRequestView NewAccommodationReservationReschedulingRequestView;

        public NewAccommodationReservationReschedulingRequestViewModel(NewAccommodationReservationReschedulingRequestView newAccommodationReservationReschedulingRequestView, User guest1)
        {
            NewAccommodationReservationReschedulingRequestView = newAccommodationReservationReschedulingRequestView;

            User = guest1;

            _accommodationReservationService = new AccommodationReservationService();
            _accommodationReservationService.Subscribe(this);
            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllReservations());

            FilteredData = _accommodationReservationService.FindSuitableUpcomingReservations(User);
            NewAccommodationReservationReschedulingRequestView.myDataGrid.ItemsSource = FilteredData;
        }

        public void Update()
        {
            FilteredData.Clear();
            FilteredData = _accommodationReservationService.FindSuitableUpcomingReservations(User);
            NewAccommodationReservationReschedulingRequestView.myDataGrid.ItemsSource = FilteredData;
        }

        public bool CheckIfPossible(AccommodationReservation selectedAccommodationReservation)
        {
            if(selectedAccommodationReservation==null)
            {
                MessageBox.Show("Molimo Vas selektujte rezervaciju koji zelite obrisete.");
                return false;
            }
            if (_accommodationReservationReschedulingService.CheckForActiveRequest(selectedAccommodationReservation))
            {
                MessageBox.Show("Vec ste podneli zahtev za pomeranje ove rezervacije");
                return false;
            }
            return true;
        }
    }
}
