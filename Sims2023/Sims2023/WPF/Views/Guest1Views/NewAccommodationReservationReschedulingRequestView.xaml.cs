using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for NewAccommodationReservationReschedulingRequest.xaml
    /// </summary>
    public partial class NewAccommodationReservationReschedulingRequestView : Page
    {
        NewAccommodationReservationReschedulingRequestViewModel NewAccommodationReservationReschedulingRequestViewModel;

        AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        public User User { get; set; }
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }

        Frame MainFrame;

        public NewAccommodationReservationReschedulingRequestView(User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings, AccommodationReservationReschedulingService accommodationReservationReschedulingService,Frame mainFrame)
        {
            InitializeComponent();
            NewAccommodationReservationReschedulingRequestViewModel = new NewAccommodationReservationReschedulingRequestViewModel(this, guest1);
            DataContext = NewAccommodationReservationReschedulingRequestViewModel;
            User = guest1;
            MainFrame = mainFrame;
            AccommodationReservationReschedulings = accommodationReservationReschedulings;
            _accommodationReservationReschedulingService=accommodationReservationReschedulingService;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void ReschedulReservation(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (NewAccommodationReservationReschedulingRequestViewModel.CheckIfPossible(SelectedAccommodationReservation))
            {
                MainFrame.Navigate(new AccommodationReservationDateView(SelectedAccommodationReservation.Id, SelectedAccommodationReservation.Accommodation, User, AccommodationReservationReschedulings, _accommodationReservationReschedulingService, MainFrame));
                NewAccommodationReservationReschedulingRequestViewModel.Update();
            }
            
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
