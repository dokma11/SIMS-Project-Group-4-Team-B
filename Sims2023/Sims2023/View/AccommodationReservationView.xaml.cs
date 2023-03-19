using Sims2023.Controller;
using Sims2023.Model;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationView : Window
    {
        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        private AccommodationReservationController _accommodationReservationController;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        public List<DateTime> AvailableDates = new List<DateTime>();
        public List<DateTime> AdditionalAvailableDates = new List<DateTime>();
        public List<AccommodationStay> Stays = new List<AccommodationStay>();
        bool todaysDay;

        public AccommodationReservationView(Accommodation selectedAccommodationL)
        {
            InitializeComponent();
            DataContext = this;

            SelectedAccommodation = selectedAccommodationL;

            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllAccommodations()); 

            _accommodationReservationController = new AccommodationReservationController();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetAllReservations());

            List<DateTime> AvailableDates = new List<DateTime>();
            List<DateTime> AdditionalAvailableDates = new List<DateTime>();
            List<AccommodationStay> Stays = new List<AccommodationStay>();

            startDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            endDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));

            todaysDay = false;
        }

        private void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckDateRequirments())
            {
                return;
            }

            DateTime startDateSelected = startDatePicker.SelectedDate.Value;
            DateTime endDateSelected = endDatePicker.SelectedDate.Value;
            int stayLength = (int)numberOfDays.Value;
            
            int possibleDatesNumber = CheckDates(startDateSelected, endDateSelected, stayLength, AvailableDates);

            if (AvailableDates.Count > 0)
            {
                CreateAvailableStayList(AvailableDates, stayLength);
            }

            if (AvailableDates.Count == 0)
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

            AccommodationReservationDateView accommodationReservationConfirmationView = new AccommodationReservationDateView(SelectedAccommodation, Stays, stayLength);
            accommodationReservationConfirmationView.Show();

        }
        private int CheckDates(DateTime startDateSelected, DateTime endDateSelected,int stayLength, List<DateTime> datesList)
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
        private void CreateAvailableStayList(List<DateTime> availableStayDates, int stayLength)
        {
            foreach (DateTime startDate in availableStayDates)
            {
                DateTime endDate = startDate.AddDays(stayLength);
                AccommodationStay stay = new AccommodationStay();
                stay.StartDate = startDate;
                stay.EndDate= endDate;
                Stays.Add(stay);
            }

        }

        private bool CheckDateRequirments()
        {
            if (startDatePicker.SelectedDate == null || endDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Molimo Vas da selektujete datume.");
                return false;
            }
            DateTime startDateSelected = startDatePicker.SelectedDate.Value;
            DateTime endDateSelected = endDatePicker.SelectedDate.Value;
            int daysNumber = (int)numberOfDays.Value;

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
        private bool IsDateSpanAvailable(DateTime startDate, DateTime endDate)
        {
            foreach (AccommodationReservation reservation in _accommodationReservationController.GetAllReservations())
            {
                if (reservation.AccommodationId==SelectedAccommodation.Id)
                {
                    for (DateTime i=reservation.StartDate; i <= reservation.EndDate; i = i.AddDays(1))
                    {
                        for (DateTime j = startDate; j <= endDate;j = j.AddDays(1))
                        {
                            if (i==j)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
