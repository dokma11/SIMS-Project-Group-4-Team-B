using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using Sims2023.WPF.Views.Guest1Views.Guest1HelpViews;
using System.Windows;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationRenovationRecommodationView.xaml
    /// </summary>
    public partial class AccommodationRenovationRecommodationView : Window
    {
        AccommodationRenovationRecommodationViewModel AccommodationRenovationRecommodationViewModel;

        private AccommodationGradeService _accommodationGradeService;

        private AccommodationGrade Grade;

        public AccommodationRenovationRecommodationView(AccommodationGrade grade, AccommodationReservation SelectedAccommodationReservationn, User guest1, AccommodationReservationService accommodationReservationController, AccommodationGradeService accommodationGradeService)
        {
            InitializeComponent();
            _accommodationGradeService = accommodationGradeService;
            Grade = grade;
            AccommodationRenovationRecommodationViewModel = new AccommodationRenovationRecommodationViewModel(this, grade, SelectedAccommodationReservationn, guest1, accommodationReservationController, _accommodationGradeService);
            DataContext = AccommodationRenovationRecommodationViewModel;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void CreateRecommodation(object sender, ExecutedRoutedEventArgs e)
        {   if(AccommodationRenovationRecommodationViewModel.CheckIfAllFiledsFilled())
            {
                var result = System.Windows.MessageBox.Show("Da li ste sigurni da želite da ostavite ovu preporuku?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    AccommodationRenovationRecommodationViewModel.AddRenovationRecommodation();
                    Close();
                }
            }
            else
            {
                AccommodationRenovationRecommodationViewModel.NotAllFieldsAreFiled();
            }
            
        }

        public void GoBack(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        public void OpenHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var GuestOneMainHelpView = new GuestOneMainHelpView("AccommodationRenovationRecommodationView");
            GuestOneMainHelpView.Show();
        }
    }
}
