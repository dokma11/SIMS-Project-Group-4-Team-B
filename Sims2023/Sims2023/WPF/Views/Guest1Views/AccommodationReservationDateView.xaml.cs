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

        public void Command1View_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int daysNumber = (int)numberOfDays.Value;
            int guestsNumber = (int)numberOfGuests.Value;
            SelectedAccommodationStay = (AccommodationStay)availableDatesGrid.SelectedItem;
            if (AccommodationReservationDateViewModel.ButtonDateConfirmation_Check(SelectedAccommodationStay))
            {
                AccommodationReservationConfirmationView accommodationReservationConfirmationView = new AccommodationReservationConfirmationView(ReservationId, SelectedAccommodation, SelectedAccommodationStay, daysNumber, guestsNumber, User, AccommodationReservationReschedulings, _accommodationReservationReschedulingService);
                accommodationReservationConfirmationView.Show();
            }
            MainFrame.Navigate(new AccommodationReservationDateView(-1, SelectedAccommodation, User, AccommodationReservationReschedulings, _accommodationReservationReschedulingService, MainFrame));
        }

        public void Command2View_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AccommodationReservationDateViewModel.MakeReservation_Click();
        }

        public void Command3View_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }
    }
}
