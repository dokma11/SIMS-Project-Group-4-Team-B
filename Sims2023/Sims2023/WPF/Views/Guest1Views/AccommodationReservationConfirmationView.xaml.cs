using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Controller;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;
using Sims2023.WPF.ViewModels.Guest1ViewModel;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>

    public partial class AccommodationReservationConfirmationView : Window
    {
        public AccommodationReservationConfirmationViewModel AccommodationReservationConfirmationViewModel;

        public AccommodationReservationConfirmationView(int reservationId, Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay, int daysNumber, User guest1)
        {
            InitializeComponent();
            AccommodationReservationConfirmationViewModel = new AccommodationReservationConfirmationViewModel(this, reservationId, selectedAccommodation, selectedAccommodationStay, daysNumber, guest1);
            DataContext = AccommodationReservationConfirmationViewModel;
        }
        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationConfirmationViewModel.ReservationButton_Click(sender, e);
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}