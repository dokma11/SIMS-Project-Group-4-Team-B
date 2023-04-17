using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>

    public partial class AccommodationReservationConfirmationViewModel : Window
    {
        public User User { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationStay SelectedAccommodationStay { get; set; }

        private AccommodationReservationService _accommodationReservationService;

        private AccommodationReservationReschedulingService _accommodationReservationReschedulingController;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }
        public int ReservationId { get; set; }

        int days;

        AccommodationReservationConfirmationView AccommodationReservationConfirmationView;
        public AccommodationReservationConfirmationViewModel(AccommodationReservationConfirmationView accommodationReservationConfirmationView, int reservationId, Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay, int daysNumber, User guest1)
        {
            AccommodationReservationConfirmationView = accommodationReservationConfirmationView;

            User = guest1;
            SelectedAccommodation = selectedAccommodation;
            SelectedAccommodationStay = selectedAccommodationStay;
            days = daysNumber;
            ReservationId = reservationId;

            _accommodationReservationService = new AccommodationReservationService();

            _accommodationReservationReschedulingController = new AccommodationReservationReschedulingService();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingController.GetAllReservationReschedulings());

            FillTextBoxes(SelectedAccommodation, SelectedAccommodationStay);

        }

        public void FillTextBoxes(Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay)
        {
            AccommodationReservationConfirmationView.accommodatioNameTextBox.Text = selectedAccommodation.Name;
            AccommodationReservationConfirmationView.accommodatioCityTextBox.Text = selectedAccommodation.Location.City;
            AccommodationReservationConfirmationView.accommodatioCountryTextBox.Text = selectedAccommodation.Location.Country;
            AccommodationReservationConfirmationView.accommodatioTypeTextBox.Text = selectedAccommodation.Type.ToString();
            AccommodationReservationConfirmationView.accommodatioStartDateTextBox.Text = selectedAccommodationStay.StartDate.ToString("MM/dd/yyyy");
            AccommodationReservationConfirmationView.accommodatioEndDateTextBox.Text = selectedAccommodationStay.EndDate.ToString("MM/dd/yyyy");
            AccommodationReservationConfirmationView.PicturesListView.ItemsSource = selectedAccommodation.Imageurls;
        }

        public void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationId == -1)
            {
                MakeNewAccommodationReservation();
            }
            else
            {
                MakeNewAccommodationReservationRescheduling(ReservationId);
            }
            AccommodationReservationConfirmationView.Close();

        }

        public void MakeNewAccommodationReservation()
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation(-1, User, SelectedAccommodation, SelectedAccommodationStay.StartDate, SelectedAccommodationStay.EndDate, days, false);
            _accommodationReservationService.Create(accommodationReservation);

            MessageBox.Show("Uspesno ste rezervisali objekat!");
        }

        public void MakeNewAccommodationReservationRescheduling(int ReservationId)
        {
            AccommodationReservationRescheduling accommodationReservationRescheduling = new AccommodationReservationRescheduling();
            accommodationReservationRescheduling.AccommodationReservation = _accommodationReservationService.GetById(ReservationId);
            accommodationReservationRescheduling.Status = AccommodationReservationRescheduling.RequestStatus.Pending;
            accommodationReservationRescheduling.Notified = false;
            accommodationReservationRescheduling.NewStartDate = SelectedAccommodationStay.StartDate;
            accommodationReservationRescheduling.NewEndDate = SelectedAccommodationStay.EndDate;
            accommodationReservationRescheduling.Comment = "Trenutno nema komentara";
            _accommodationReservationReschedulingController.Create(accommodationReservationRescheduling);
            MessageBox.Show("Uspesno ste podneli zahtev za pomeranje rezervacije!");

        }
    }
}