using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;

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

        private AccommodationReservationReschedulingController _accommodationReservationReschedulingController;
        public ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings { get; set; }
        public int ReservationId { get; set; }

        int days;
        public AccommodationReservationConfirmationView(int reservationId,Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay, int daysNumber, User guest1)
        {
            InitializeComponent();
            DataContext = this;

            User = guest1;
            SelectedAccommodation = selectedAccommodation;
            SelectedAccommodationStay = selectedAccommodationStay;
            days = daysNumber;
            ReservationId = reservationId;

            _accommodationReservationController = new AccommodationReservationService();

            _accommodationReservationReschedulingController = new AccommodationReservationReschedulingController();
            AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingController.GetAllReservationReschedulings());

            FillTextBoxes(SelectedAccommodation,SelectedAccommodationStay);
            
        }

        private void FillTextBoxes(Accommodation selectedAccommodation, AccommodationStay selectedAccommodationStay)
        {
            accommodatioNameTextBox.Text = selectedAccommodation.Name;
            accommodatioCityTextBox.Text = selectedAccommodation.Location.City;
            accommodatioCountryTextBox.Text = selectedAccommodation.Location.Country;
            accommodatioTypeTextBox.Text = selectedAccommodation.Type.ToString();
            accommodatioStartDateTextBox.Text = selectedAccommodationStay.StartDate.ToString("MM/dd/yyyy");
            accommodatioEndDateTextBox.Text = selectedAccommodationStay.EndDate.ToString("MM/dd/yyyy");
            PicturesListView.ItemsSource = selectedAccommodation.Imageurls;
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationId == -1)
            {
                MakeNewAccommodationReservation();
            }
            else
            {
                MakeNewAccommodationReservationRescheduling(ReservationId);
            }
            Close();

        }

        private void MakeNewAccommodationReservation()
        {
            AccommodationReservation accommodationReservation = new AccommodationReservation(-1, User, SelectedAccommodation, SelectedAccommodationStay.StartDate, SelectedAccommodationStay.EndDate, days, false);
            _accommodationReservationController.Create(accommodationReservation);

            MessageBox.Show("Uspesno ste rezervisali objekat!");
        }

        private void MakeNewAccommodationReservationRescheduling(int ReservationId)
        {
            AccommodationReservationRescheduling accommodationReservationRescheduling = new AccommodationReservationRescheduling();
            accommodationReservationRescheduling.AccommodationReservation = _accommodationReservationController.GetById(ReservationId);
            accommodationReservationRescheduling.Status = AccommodationReservationRescheduling.RequestStatus.Pending;
            accommodationReservationRescheduling.Notified = false;
            accommodationReservationRescheduling.NewStartDate = SelectedAccommodationStay.StartDate;
            accommodationReservationRescheduling.NewEndDate = SelectedAccommodationStay.EndDate;
            accommodationReservationRescheduling.Comment = "Nema komentara";
            _accommodationReservationReschedulingController.Create(accommodationReservationRescheduling);
            MessageBox.Show("Uspesno ste podneli zahtev za pomeranje rezervacije!");

        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
