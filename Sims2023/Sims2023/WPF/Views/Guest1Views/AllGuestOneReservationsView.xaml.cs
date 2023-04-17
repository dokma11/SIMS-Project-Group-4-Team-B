using Sims2023.Domain.Models;
using Sims2023.Observer;
﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AllGuestOneReservationsView.xaml
    /// </summary>
    public partial class AllGuestOneReservationsView : Window
    {
        AllGuestOneReservationsViewModel AllGuestOneReservationsViewModel;

        private AccommodationReservationService _accommodationReservationService;
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        public User User { get; set; }
        public AllGuestOneReservationsView(User guest1)
        {
            InitializeComponent();
            AllGuestOneReservationsViewModel = new AllGuestOneReservationsViewModel(this, guest1);
            DataContext = AllGuestOneReservationsViewModel;

        private void grading_Click(object sender, RoutedEventArgs e)
        {
            AllGuestOneReservationsViewModel.grading_Click(sender, e);
            _accommodationReservationService = new AccommodationReservationService();
            User = guest1;
        }

        private void grading_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (AllGuestOneReservationsViewModel.GradingIsPossible(SelectedAccommodationReservation))
            {
                var AccommodationAndOwnerGradingView = new AccommodationAndOwnerGradingView(SelectedAccommodationReservation, User, _accommodationReservationService);
                AccommodationAndOwnerGradingView.ShowDialog();
                AllGuestOneReservationsViewModel.Update();
            }
        }

        private void renovation_Click(object sender, RoutedEventArgs e)
        {
            AllGuestOneReservationsViewModel.renovation_Click(sender, e);
            MessageBox.Show("Ova opcija jos uvek nije dostupna.");
            return;
        }
    }
}
