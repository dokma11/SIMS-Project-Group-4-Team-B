using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for ReschedulingDetailsView.xaml
    /// </summary>
    public partial class ReschedulingDetailsView : Page
    {
        public AccommodationReservationRescheduling guest { get; set; }
        public AccommodationReservation UpdatedReservationStatus { get; set; }

        public ObservableCollection<AccommodationReservationRescheduling> peoplee;

        public AccommodationReservationReschedulingService _reschedulingController;

        public ReschedulingDetailsViewModel ReschedulingDetailsViewModel;

        public string welcomeString { get; set; }
        public ReschedulingDetailsView(AccommodationReservationRescheduling SelectedGuest, ObservableCollection<AccommodationReservationRescheduling> people)
        {
            InitializeComponent();
            ReschedulingDetailsViewModel = new ReschedulingDetailsViewModel(SelectedGuest, people, this);
            guest = SelectedGuest;
            DataContext = guest;
            peoplee = people;

            welcomeString = welcome_string();
            string welcome_string()
            {
                var message = "Detalji zahtjeva o pomijeranju\nrezervacije za gosta " + SelectedGuest.AccommodationReservation.Guest.Name + " " + SelectedGuest.AccommodationReservation.Guest.Surname;
                return message;

            }

        }


        public string isAccommodationFree
        {
            get
            {
                var message = ReschedulingDetailsViewModel.isAccommodationFree(guest) ? "NIJE" : "JESTE";
                return $"Smještaj {message} zauzet u novotraženom terminu";
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            var decline = new DeclineRescheduleView(guest, this, peoplee);
            FrameManager.Instance.MainFrame.Navigate(decline);
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            ReschedulingDetailsViewModel.Accept_Click(sender, e);
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }
    }
}