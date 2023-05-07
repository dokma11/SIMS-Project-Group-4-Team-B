using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationCancellationView.xaml
    /// </summary>
    public partial class AccommodationReservationCancellationView : Page
    {
        AccommodationReservationCancellationViewModel AccommodationReservationCancellationViewModel;
        public AccommodationReservationCancellationView(User guest1)
        {
            InitializeComponent();
            AccommodationReservationCancellationViewModel = new AccommodationReservationCancellationViewModel(this, guest1);
            DataContext = AccommodationReservationCancellationViewModel;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void CancelReservation(object sender, ExecutedRoutedEventArgs e)
        {
            if (AccommodationReservationCancellationViewModel.cancellation_Click())
            {
                NavigationService navigationService = NavigationService.GetNavigationService(this);

                if (navigationService.CanGoBack)
                {
                    navigationService.GoBack();
                }
            }
        }
    }
}
