﻿using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationReschedulingView.xaml
    /// </summary>
    public partial class AccommodationReservationReschedulingView : Window, IObserver
    {
        AccommodationReservationReschedulingViewModel AccommodationReservationReschedulingViewModel;
        public User User { get; set; }

        public AccommodationReservationReschedulingView(User guest1)
        {
            InitializeComponent();
            AccommodationReservationReschedulingViewModel = new AccommodationReservationReschedulingViewModel(this, guest1);
            DataContext = AccommodationReservationReschedulingViewModel;

            User = guest1;
        }

        private void newRequest_Click(object sender, RoutedEventArgs e)
        {
            var newAccommodationReservationReschedulingRequest = new NewAccommodationReservationReschedulingRequestView(User);
            newAccommodationReservationReschedulingRequest.Show();
            AccommodationReservationReschedulingViewModel.Update();
        }

        private void report_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.report_Click(sender, e);
        }

        private void comment_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.comment_Click(sender, e);
        }

        public void Update()
        {
            AccommodationReservationReschedulingViewModel.Update();
        }
    }
}
