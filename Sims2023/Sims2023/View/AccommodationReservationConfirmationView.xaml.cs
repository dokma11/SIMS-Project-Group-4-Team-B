using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Model;
using System.Windows;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>

    public partial class AccommodationReservationConfirmationView : Window
    {
        public User User { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationStay SelectedAccommodationStay { get; set; }

        private AccommodationReservationService _accommodationReservationController;

        int days;
        public AccommodationReservationConfirmationView(Accommodation selectedAccommodationS, AccommodationStay selectedAccommodationStayS, int daysNumber, User guest1)
        {
            InitializeComponent();
            DataContext = this;

            User = guest1;
            SelectedAccommodation = selectedAccommodationS;
            SelectedAccommodationStay = selectedAccommodationStayS;
            days = daysNumber;

            _accommodationReservationController = new AccommodationReservationService();

            accommodatioNameTextBox.Text = selectedAccommodationS.Name;
            accommodatioCityTextBox.Text = selectedAccommodationS.Location.City;
            accommodatioCountryTextBox.Text = selectedAccommodationS.Location.Country;
            accommodatioTypeTextBox.Text = selectedAccommodationS.Type.ToString();
            accommodatioStartDateTextBox.Text = selectedAccommodationStayS.StartDate.ToString("MM/dd/yyyy");
            accommodatioEndDateTextBox.Text = selectedAccommodationStayS.EndDate.ToString("MM/dd/yyyy");
            PicturesListView.ItemsSource = SelectedAccommodation.Imageurls;
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation(-1, User, SelectedAccommodation, SelectedAccommodationStay.StartDate, SelectedAccommodationStay.EndDate, days, false);
            _accommodationReservationController.Create(accommodationReservation);

            MessageBox.Show("Uspesno ste rezervisali objekat!");

            Close();

        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
