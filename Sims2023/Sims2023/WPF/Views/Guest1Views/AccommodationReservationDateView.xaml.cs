using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationDateView : Page
    {
        public AccommodationReservationDateViewModel AccommodationReservationDateViewModel;

        public Frame MainFrame;

        AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public Accommodation SelectedAccommodation { get; set; }
        public User User { get; set; }
        public int ReservationId { get; set; }
        public AccommodationStay SelectedAccommodationStay { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        public AccommodationReservationDateView(int reservationId, Accommodation selectedAccommodation, User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings, AccommodationReservationReschedulingService accommodationReservationReschedulingService,Frame mainFrame)
        {
            InitializeComponent();
            AccommodationReservationDateViewModel = new AccommodationReservationDateViewModel(this, reservationId, selectedAccommodation, guest1);
            DataContext = AccommodationReservationDateViewModel;

            User = guest1;
            MainFrame = mainFrame;
            ReservationId = reservationId;
            SelectedAccommodation = selectedAccommodation;
            AccommodationReservationReschedulings = accommodationReservationReschedulings;
            _accommodationReservationReschedulingService = accommodationReservationReschedulingService;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void MakeReservation(object sender, ExecutedRoutedEventArgs e)
        {
            int daysNumber = (int)numberOfDays.Value;
            int guestsNumber = (int)numberOfGuests.Value;
            SelectedAccommodationStay = (AccommodationStay)availableDatesGrid.SelectedItem;
            if (AccommodationReservationDateViewModel.ButtonDateConfirmation_Check(SelectedAccommodationStay))
            {
                MainFrame.Navigate(new AccommodationReservationConfirmationView(ReservationId, SelectedAccommodation, SelectedAccommodationStay, daysNumber, guestsNumber, User, AccommodationReservationReschedulings, _accommodationReservationReschedulingService, MainFrame));
            }
            
        }

        public void ConfirmReservation(object sender, ExecutedRoutedEventArgs e)
        {
            AccommodationReservationDateViewModel.MakeReservation_Click();
        }

        public void GoBack(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }
    }
}
