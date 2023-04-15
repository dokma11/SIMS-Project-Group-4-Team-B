using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for OpenAccommodationRegistrationView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {

        public ObservableCollection<AccommodationCancellation> AccommodationCancellations { get; set; }

        public List<AccommodationReservation> Reservatons { get; set; }
        public List<AccommodationReservation> GradableGuests { get; set; }

        public OwnerViewModel ownerViewModel { get; set; }

        public User User { get; set; }
        public OwnerView(User owner)
        {

            InitializeComponent();
            DataContext = this;

            User = owner;
            ownerViewModel = new OwnerViewModel(User);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ownerViewModel.Window_Loaded();
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            ownerViewModel.Grade_Click();
        }

        private void AddAccommodation_Click(object sender, RoutedEventArgs e)
        {
            ownerViewModel.AddAccommodation_Click();
        }

        private void Grades_Given_From_Guests(object sender, RoutedEventArgs e)
        {
            ownerViewModel.Grades_Given_From_Guests();
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            ownerViewModel.Reservations_Click();
        }
    }
}