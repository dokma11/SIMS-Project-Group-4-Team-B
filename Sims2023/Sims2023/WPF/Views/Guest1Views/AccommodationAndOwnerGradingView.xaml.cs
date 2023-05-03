using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerGradingView.xaml
    /// </summary>
    public partial class AccommodationAndOwnerGradingView : Window
    {
        public AccommodationAndOwnerGradingViewModel AccommodationAndOwnerGradingViewModel;
        public AccommodationAndOwnerGradingView(AccommodationReservation SelectedAccommodationReservationn, User guest1, AccommodationReservationService accommodationReservationController)
        {
            InitializeComponent();
            AccommodationAndOwnerGradingViewModel = new AccommodationAndOwnerGradingViewModel(this, SelectedAccommodationReservationn, guest1, accommodationReservationController);
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
            var result = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da ostavite ovu recenziju?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                AccommodationAndOwnerGradingViewModel.AddCreatedGrade();
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

    }
}
