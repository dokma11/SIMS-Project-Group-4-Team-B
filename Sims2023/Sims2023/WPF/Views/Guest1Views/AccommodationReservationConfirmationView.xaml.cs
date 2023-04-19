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

    public partial class AccommodationReservationConfirmationView : Window
    {
        public AccommodationReservationConfirmationViewModel AccommodationReservationConfirmationViewModel;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        AccommodationReservationReschedulingService _accommodationReservationReschedulingService;

        public AccommodationReservationConfirmationView(int reservationId, Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay, int daysNumber,int guestsNumber, User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings, AccommodationReservationReschedulingService accommodationReservationReschedulingService)
        {
            InitializeComponent();
            AccommodationReservationReschedulings = accommodationReservationReschedulings;
            _accommodationReservationReschedulingService = accommodationReservationReschedulingService;
            AccommodationReservationConfirmationViewModel = new AccommodationReservationConfirmationViewModel(this, reservationId, selectedAccommodation, selectedAccommodationStay, daysNumber, guestsNumber, guest1, accommodationReservationReschedulings,_accommodationReservationReschedulingService);
            DataContext = AccommodationReservationConfirmationViewModel;
        }
        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationConfirmationViewModel.ReservationButton_Click();
            Close();
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}