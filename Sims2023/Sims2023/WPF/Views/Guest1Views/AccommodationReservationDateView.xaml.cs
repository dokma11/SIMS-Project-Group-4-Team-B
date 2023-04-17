using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationDateView : Window
    {
        public AccommodationReservationDateViewModel AccommodationReservationDateViewModel;
        public AccommodationReservationDateView(int reservationId, Accommodation selectedAccommodation, User guest1)
        {
            InitializeComponent();
            AccommodationReservationDateViewModel = new AccommodationReservationDateViewModel(this, reservationId, selectedAccommodation, guest1);
            DataContext = AccommodationReservationDateViewModel;
        }

        private void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationDateViewModel.MakeReservation_Click(sender, e);
        }

        private void ButtonDateConfirmation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationDateViewModel.ButtonDateConfirmation_Click(sender, e);
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationDateViewModel.ButtonDateCancelation_Click(sender, e);
        }
    }
}
