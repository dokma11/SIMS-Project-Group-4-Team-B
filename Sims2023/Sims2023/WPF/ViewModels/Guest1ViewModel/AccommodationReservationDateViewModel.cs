using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.View;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationDateViewModel : Window
    {
        public List<AccommodationStay> stays = new List<AccommodationStay>();

        private ObservableCollection<AccommodationStay> _stays = new ObservableCollection<AccommodationStay>();
        public AccommodationStay SelectedAccommodationStay { get; set; }

        private AccommodationService _accommodationService;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        private AccommodationReservationService _accommodationReservationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public User User { get; set; }
        public int ReservationId { get; set; }

        public List<DateTime> AvailableDates = new List<DateTime>();
        public List<DateTime> AdditionalAvailableDates = new List<DateTime>();
        bool todaysDay;
        int daysNumber;

        private AccommodationReservationDateView AccommodationReservationDateView;

        public AccommodationReservationDateViewModel(AccommodationReservationDateView accommodationReservationDateView, int reservationId, Accommodation selectedAccommodation, User guest1)
        {
            AccommodationReservationDateView = accommodationReservationDateView;

            User = guest1;
            ReservationId = reservationId;
            stays = new List<AccommodationStay>();

            SelectedAccommodation = selectedAccommodation;


            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodations());

            _accommodationReservationService = new AccommodationReservationService();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllReservations());

            AvailableDates = new List<DateTime>();
            AdditionalAvailableDates = new List<DateTime>();

            AccommodationReservationDateView.startDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            AccommodationReservationDateView.endDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));

            todaysDay = false;

        }

        public void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            _stays.Clear();
            AccommodationReservationDateView.availableDatesGrid.ItemsSource = _stays;

            if (!CheckDateRequirments())
            {
                return;
            }

            DateTime startDateSelected = AccommodationReservationDateView.startDatePicker.SelectedDate.Value;
            DateTime endDateSelected = AccommodationReservationDateView.endDatePicker.SelectedDate.Value;
            int stayLength = (int)AccommodationReservationDateView.numberOfDays.Value;
            daysNumber = stayLength;

            int possibleDatesNumber = CheckDates(startDateSelected, endDateSelected, stayLength, AvailableDates);

            if (AvailableDates.Count > 0)
            {
                OriginalAvailableDatesFound(AvailableDates, stayLength);
            }

            if (AvailableDates.Count == 0)
            {
                ExtendedAvailableDatesSearch(startDateSelected, endDateSelected, stayLength, AvailableDates);
            }
        }

        public void OriginalAvailableDatesFound(List<DateTime> availableDates, int stayLength)
        {
            CreateAvailableStayList(availableDates, stayLength);
        }

        public void ExtendedAvailableDatesSearch(DateTime startDateSelected, DateTime endDateSelected, int stayLength, List<DateTime> availableDates)
        {
            MessageBox.Show("Za datume koje ste izabrali nismo uspeli da pronademo nijedan slobodan termin.Ukoliko zelite mozete izabrati neki od slobodnih termina koji su najslicniji onima koji ste zeleli.");
            int datesFound = 0;

            while (datesFound < 3)
            {
                if (todaysDay)
                {
                    datesFound = CheckDates(startDateSelected, endDateSelected = endDateSelected.AddDays(1), stayLength, AdditionalAvailableDates);
                }
                else
                {
                    datesFound = CheckDates(startDateSelected = startDateSelected.AddDays(-1), endDateSelected = endDateSelected.AddDays(1), stayLength, AdditionalAvailableDates);
                }

            }
            if (AdditionalAvailableDates.Count > 0)
            {
                CreateAvailableStayList(AdditionalAvailableDates, stayLength);
            }
        }

        public int CheckDates(DateTime startDateSelected, DateTime endDateSelected, int stayLength, List<DateTime> datesList)
        {
            DateTime endDate = startDateSelected.AddDays(stayLength);
            bool isAvailable;

            for (DateTime startDate = startDateSelected; startDate <= endDateSelected.AddDays(-stayLength); startDate = startDate.AddDays(1), endDate = endDate.AddDays(1))
            {
                isAvailable = IsDateSpanAvailable(startDate, endDate);
                if (isAvailable)
                {
                    if (datesList.Count == 0)
                    {
                        datesList.Add(startDate);
                    }
                    else
                    {
                        if (!datesList.Contains(startDate))
                        {
                            datesList.Add(startDate);
                        }

                    }
                }
            }
            return datesList.Count;
        }
        public void CreateAvailableStayList(List<DateTime> availableStayDates, int stayLength)
        {
            foreach (DateTime startDate in availableStayDates)
            {
                DateTime endDate = startDate.AddDays(stayLength);
                AccommodationStay stay = new AccommodationStay();
                stay.StartDate = startDate;
                stay.EndDate = endDate;
                stays.Add(stay);
                _stays.Add(stay);
            }

        }

        public bool CheckDateRequirments()
        {
            if (AccommodationReservationDateView.startDatePicker.SelectedDate == null || AccommodationReservationDateView.endDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Molimo Vas da selektujete datume.");
                return false;
            }
            DateTime startDateSelected = AccommodationReservationDateView.startDatePicker.SelectedDate.Value;
            DateTime endDateSelected = AccommodationReservationDateView.endDatePicker.SelectedDate.Value;
            int daysNumber = (int)AccommodationReservationDateView.numberOfDays.Value;

            int minDays = SelectedAccommodation.MinDays;

            if (startDateSelected == DateTime.Today)
            {
                todaysDay = true;
            }

            if (DateTime.Compare(startDateSelected, endDateSelected) > 0)
            {
                MessageBox.Show("Molimo Vas selektujete pravilno datume.");
                return false;
            }
            if (daysNumber < SelectedAccommodation.MinDays)
            {
                MessageBox.Show($"Za ovaj smestaj minimalni broj dana koji mozete rezervisati je  {minDays} .");
                return false;
            }
            if ((endDateSelected - startDateSelected).TotalDays < daysNumber)
            {
                MessageBox.Show($"Niste dobro uneli podatke. Vremensko ogranicenje nije dovoljno da bi se rezervisao {daysNumber} dana");
                return false;
            }
            return true;

        }
        public bool IsDateSpanAvailable(DateTime startDate, DateTime endDate)
        {
            foreach (AccommodationReservation reservation in _accommodationReservationService.GetAllReservations())
            {
                if (reservation.Accommodation.Id == SelectedAccommodation.Id)
                {
                    for (DateTime i = reservation.StartDate; i <= reservation.EndDate; i = i.AddDays(1))
                    {
                        for (DateTime j = startDate; j <= endDate; j = j.AddDays(1))
                        {
                            if (i == j)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ButtonDateConfirmation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodationStay = (AccommodationStay)AccommodationReservationDateView.availableDatesGrid.SelectedItem;
            if (SelectedAccommodationStay == null)
            {
                MessageBox.Show("Molimo Vas selektujte datume koje zelite da rezervisete.");
                return;
            }
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Doslo je do greske.");
                return;
            }
            AccommodationReservationConfirmationView accommodationReservationConfirmationView = new AccommodationReservationConfirmationView(ReservationId, SelectedAccommodation, SelectedAccommodationStay, daysNumber, User);
            accommodationReservationConfirmationView.Show();
        }

        public void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationDateView.Close();
        }
    }
}
