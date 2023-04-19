using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationDateView : Window
    {
        public AccommodationReservationDateViewModel AccommodationReservationDateViewModel;

        AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public Accommodation SelectedAccommodation { get; set; }
        public User User { get; set; }
        public int ReservationId { get; set; }
        public AccommodationStay SelectedAccommodationStay { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        public AccommodationReservationDateView(int reservationId, Accommodation selectedAccommodation, User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings, AccommodationReservationReschedulingService accommodationReservationReschedulingService)
        {
            InitializeComponent();
            AccommodationReservationDateViewModel = new AccommodationReservationDateViewModel(this, reservationId, selectedAccommodation, guest1);
            DataContext = AccommodationReservationDateViewModel;

            User = guest1;
            ReservationId = reservationId;
            SelectedAccommodation = selectedAccommodation;
            AccommodationReservationReschedulings = accommodationReservationReschedulings;
            _accommodationReservationReschedulingService = accommodationReservationReschedulingService;
        }

        private void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationDateViewModel.MakeReservation_Click(sender, e);
        }

        private void ButtonDateConfirmation_Click(object sender, RoutedEventArgs e)
        {
            int daysNumber = (int)numberOfDays.Value;
            int guestsNumber = (int)numberOfGuests.Value;
            SelectedAccommodationStay = (AccommodationStay)availableDatesGrid.SelectedItem;
            if (AccommodationReservationDateViewModel.ButtonDateConfirmation_Check(SelectedAccommodationStay))
            {
                AccommodationReservationConfirmationView accommodationReservationConfirmationView = new AccommodationReservationConfirmationView(ReservationId, SelectedAccommodation, SelectedAccommodationStay, daysNumber, guestsNumber, User, AccommodationReservationReschedulings,_accommodationReservationReschedulingService);
                accommodationReservationConfirmationView.Show();
                Close();
            }
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
