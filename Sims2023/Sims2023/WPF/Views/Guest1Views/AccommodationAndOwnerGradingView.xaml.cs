using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using Sims2023.WPF.Views.Guest1Views.Guest1HelpViews;
using System.Windows;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerGradingView.xaml
    /// </summary>
    public partial class AccommodationAndOwnerGradingView : Window
    {
        public AccommodationAndOwnerGradingViewModel AccommodationAndOwnerGradingViewModel;
        private AccommodationGradeService _accommodationGradeService;
        AccommodationGrade Grade;

        public AccommodationAndOwnerGradingView(AccommodationReservation SelectedAccommodationReservationn, User guest1, AccommodationReservationService accommodationReservationController, AccommodationGradeService accommodationGradeService)
        {
            InitializeComponent();
            _accommodationGradeService = accommodationGradeService;
            AccommodationAndOwnerGradingViewModel = new AccommodationAndOwnerGradingViewModel(this, SelectedAccommodationReservationn, guest1, accommodationReservationController, _accommodationGradeService);
            DataContext = AccommodationAndOwnerGradingViewModel;
        }

        public void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationAndOwnerGradingViewModel.RemoveButton_Click(sender, e);
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void CreateRecension(object sender, ExecutedRoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da ostavite ovu recenziju?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                AccommodationAndOwnerGradingViewModel.AddCreatedGrade(Grade);
                Close();
            }
            else
            {
                return;
            }
        }

        public void GoBack(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        public void AddPicture(object sender, ExecutedRoutedEventArgs e)
        {
            AccommodationAndOwnerGradingViewModel.addPicture_Click(sender, e);
        }

        public void OpenHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var GuestOneMainHelpView = new GuestOneMainHelpView("AccommodationAndOwnerGradingView");
            GuestOneMainHelpView.Show();
        }
    }
}
