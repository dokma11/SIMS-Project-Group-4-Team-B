using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AllGuestOneReservationsView.xaml
    /// </summary>
    public partial class AllGuestOneReservationsView : Page
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

            User = guest1;
            _accommodationReservationService = new AccommodationReservationService();
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GradeAccommodation(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (AllGuestOneReservationsViewModel.GradingIsPossible(SelectedAccommodationReservation))
            {
                AllGuestOneReservationsViewModel.BackgroundShading();
                var AccommodationAndOwnerGradingView = new AccommodationAndOwnerGradingView(SelectedAccommodationReservation, User, _accommodationReservationService);
                AccommodationAndOwnerGradingView.ShowDialog();
                AllGuestOneReservationsViewModel.BackgroundUnshading();
                AllGuestOneReservationsViewModel.Update();
            }
        }

        public void NotImplemented(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Ova opcija jos uvek nije dostupna.");
            return;
        }
    }
}
