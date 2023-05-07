using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>

    public partial class AccommodationReservationConfirmationView : Page
    {
        public AccommodationReservationConfirmationViewModel AccommodationReservationConfirmationViewModel;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        AccommodationReservationReschedulingService _accommodationReservationReschedulingService;

        Frame MainFrame;

        public AccommodationReservationConfirmationView(int reservationId, Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay, int daysNumber, int guestsNumber, User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings, AccommodationReservationReschedulingService accommodationReservationReschedulingService, Frame mainFrame)
        {
            InitializeComponent();
            AccommodationReservationReschedulings = accommodationReservationReschedulings;
            _accommodationReservationReschedulingService = accommodationReservationReschedulingService;
            MainFrame = mainFrame;
            AccommodationReservationConfirmationViewModel = new AccommodationReservationConfirmationViewModel(this, reservationId, selectedAccommodation, selectedAccommodationStay, daysNumber, guestsNumber, guest1, accommodationReservationReschedulings, _accommodationReservationReschedulingService);
            DataContext = AccommodationReservationConfirmationViewModel;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void ConfirmReservation(object sender, ExecutedRoutedEventArgs e)
        {
            AccommodationReservationConfirmationViewModel.ReservationButton_Click();
            MainFrame.Navigate(new GuestOneStartView());
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