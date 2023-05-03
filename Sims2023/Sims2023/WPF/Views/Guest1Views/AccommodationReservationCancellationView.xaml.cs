using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;
using System.Windows.Controls;

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

        private void cancellation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationCancellationViewModel.cancellation_Click(sender, e);
        }
    }
}
