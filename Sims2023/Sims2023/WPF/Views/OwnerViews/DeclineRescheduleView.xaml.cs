﻿using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for DeclineRescheduleView.xaml
    /// </summary>
    public partial class DeclineRescheduleView : Window
    {
        public AccommodationReservationRescheduling chosenPerson { get; set; }

        private ReschedulingDetailsView _reschedulingDetailsView;

        private DeclineRescheduleViewModel DeclineRescheduleViewModel;

        public ObservableCollection<AccommodationReservationRescheduling> peoplee;
        public DeclineRescheduleView(AccommodationReservationRescheduling SelectedGuest, ReschedulingDetailsView reschedulingDetailsView, ObservableCollection<AccommodationReservationRescheduling> people)
        {
            InitializeComponent();
            DeclineRescheduleViewModel = new DeclineRescheduleViewModel(this, SelectedGuest, reschedulingDetailsView, people);
            DataContext = DeclineRescheduleViewModel;

        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            DeclineRescheduleViewModel.Decline_Click(sender, e);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}