using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationDetailedView.xaml
    /// </summary>
    public partial class AccommodationDetailedView : Window
    {
        AccommodationDetailedViewModel AccommodationDetailedViewModel;
        public User User { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationDetailedView(User guest1, Accommodation selectedAccommodation)
        {
            InitializeComponent();
            AccommodationDetailedViewModel = new AccommodationDetailedViewModel(this, guest1, selectedAccommodation);
            DataContext = AccommodationDetailedViewModel;

            SelectedAccommodation = selectedAccommodation;
            User = guest1;
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationDateView accommodationReservationDateView = new AccommodationReservationDateView(-1, SelectedAccommodation, User);
            accommodationReservationDateView.Show();
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
