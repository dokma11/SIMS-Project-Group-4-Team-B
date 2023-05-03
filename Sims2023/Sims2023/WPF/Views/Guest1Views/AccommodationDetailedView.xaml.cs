using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationDetailedView.xaml
    /// </summary>
    public partial class AccommodationDetailedView : Page
    {
        AccommodationDetailedViewModel AccommodationDetailedViewModel;

        Frame MainFrame;
        public User User { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;

        public AccommodationDetailedView(User guest1, Accommodation selectedAccommodation,Frame mainFrame)
        {
            InitializeComponent();
            AccommodationDetailedViewModel = new AccommodationDetailedViewModel(this, guest1, selectedAccommodation);
            DataContext = AccommodationDetailedViewModel;

            MainFrame = mainFrame;
            SelectedAccommodation = selectedAccommodation;
            _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
            User = guest1;
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AccommodationReservationDateView(-1, SelectedAccommodation, User, AccommodationReservationReschedulings, _accommodationReservationReschedulingService, MainFrame));
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }
    }
}
