using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
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

        private AccommodationGradeService _accommodationGradeService;

        AccommodationGrade Grade;
        public AllGuestOneReservationsView(User guest1)
        {
            InitializeComponent();
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationGradeService = new AccommodationGradeService();
            AllGuestOneReservationsViewModel = new AllGuestOneReservationsViewModel(this, guest1, _accommodationReservationService, _accommodationGradeService);
            DataContext = AllGuestOneReservationsViewModel;
            User = guest1;

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
                var AccommodationAndOwnerGradingView = new AccommodationAndOwnerGradingView(SelectedAccommodationReservation, User, _accommodationReservationService, _accommodationGradeService);
                AccommodationAndOwnerGradingView.ShowDialog();
                AllGuestOneReservationsViewModel.BackgroundUnshading();
                AllGuestOneReservationsViewModel.Update();
            }
        }

        public void MakeRenovationRecommedation(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedAccommodationReservation = (AccommodationReservation)myDataGrid.SelectedItem;
            if (AllGuestOneReservationsViewModel.RecommodtionIsPossible(SelectedAccommodationReservation))
            {
                AllGuestOneReservationsViewModel.BackgroundShading();
                Grade = AllGuestOneReservationsViewModel.FindAccommodationGrade(SelectedAccommodationReservation);
                if (Grade != null)
                {
                    var AccommodationRenovationRecommodationView = new AccommodationRenovationRecommodationView(Grade, SelectedAccommodationReservation, User, _accommodationReservationService, _accommodationGradeService);
                    AccommodationRenovationRecommodationView.ShowDialog();
                    AllGuestOneReservationsViewModel.BackgroundUnshading();
                    AllGuestOneReservationsViewModel.Update();
                }
            }
        }
    }
}
