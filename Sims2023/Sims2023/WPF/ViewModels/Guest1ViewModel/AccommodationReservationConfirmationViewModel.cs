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

        private AccommodationReservationReschedulingService _accommodationReservationReschedulingService;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }
        public int ReservationId { get; set; }

        int days;

        int guests;

        private UserService _userService;

        AccommodationReservationConfirmationView AccommodationReservationConfirmationView;
        public AccommodationReservationConfirmationViewModel(AccommodationReservationConfirmationView accommodationReservationConfirmationView, int reservationId, Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay, int daysNumber, int guestsNumber, User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings, AccommodationReservationReschedulingService accommodationReservationReschedulingService)
        {
            AccommodationReservationConfirmationView = accommodationReservationConfirmationView;

            User = guest1;
            SelectedAccommodation = selectedAccommodation;
            SelectedAccommodationStay = selectedAccommodationStay;
            days = daysNumber;
            guests = guestsNumber;
            ReservationId = reservationId;

            _accommodationReservationService = new AccommodationReservationService();
            _userService = new UserService();

            _accommodationReservationReschedulingService = accommodationReservationReschedulingService;
            AccommodationReservationReschedulings = accommodationReservationReschedulings;

            FillTextBoxes(SelectedAccommodation, SelectedAccommodationStay);

        }

        public void FillTextBoxes(Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay)
        {
            AccommodationReservationConfirmationView.accommodatioNameLabel.Content = "Naziv smeštaja: " + selectedAccommodation.Name;
            AccommodationReservationConfirmationView.accommodatioCityLabel.Content = "Grad: " + selectedAccommodation.Location.City;
            AccommodationReservationConfirmationView.accommodatioCountryLabel.Content = "Država: " + selectedAccommodation.Location.Country;
            AccommodationReservationConfirmationView.accommodatioTypeLabel.Content = "Tip smeštaja: " + selectedAccommodation.Type.ToString();
            AccommodationReservationConfirmationView.accommodatioStartDateLabel.Content = "Datum početka: " + selectedAccommodationStay.StartDate.ToString("MM/dd/yyyy");
            AccommodationReservationConfirmationView.accommodatioEndDateLabel.Content = "Datum kraja: " + selectedAccommodationStay.EndDate.ToString("MM/dd/yyyy");
            AccommodationReservationConfirmationView.PicturesListView.ItemsSource = selectedAccommodation.Imageurls;
        }

        public void ReservationButton_Click()
        {
            if (ReservationId == -1)
            {
                MakeNewAccommodationReservation();
            }
            else
            {
                MakeNewAccommodationReservationRescheduling(ReservationId);
            }

        }

        public void MakeNewAccommodationReservation()
        {
            RegulateUserStatus();

            AccommodationReservation accommodationReservation = new AccommodationReservation(-1, User, SelectedAccommodation, SelectedAccommodationStay.StartDate, SelectedAccommodationStay.EndDate, days, guests, false, false);
            _accommodationReservationService.Create(accommodationReservation);

            MessageBox.Show("Uspešno ste rezervisali objekat!");
        }

        private void RegulateUserStatus()
        {
            if (User.SuperGuest1)
            {
                _userService.RemovePoint(User);
            }
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
            _accommodationReservationReschedulingService.Create(accommodationReservationRescheduling);
            AccommodationReservationReschedulings.Add(accommodationReservationRescheduling);
            MessageBox.Show("Uspešno ste podneli zahtev za pomeranje rezervacije!");
        }
    }
}