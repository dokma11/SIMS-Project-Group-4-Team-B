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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationWindow : Window
    {
        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation selectedAccommodation { get; set; }

        private AccommodationReservationController _accommodationReservationController;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        List<DateTime> availableDates = new List<DateTime>();

        public AccommodationReservationWindow(Accommodation selectedAccommodationL)
        {
            InitializeComponent();
            selectedAccommodation = selectedAccommodationL;

            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllAccommodations()); 

            _accommodationReservationController = new AccommodationReservationController();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetAllReservations());
            List<DateTime> availableDates = new List<DateTime>();

            startDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            endDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }

        private void makeReservation_Click(object sender, RoutedEventArgs e)
        {
            if(!checkDateRequirments())
            {
                return;
            }

            DateTime startDateSelected = startDatePicker.SelectedDate.Value;
            DateTime endDateSelected = endDatePicker.SelectedDate.Value;
            int daysNumber = (int)numberOfDays.Value;

            bool isAvailable = true;

            DateTime endDate = startDateSelected.AddDays(daysNumber);

            for (DateTime startDate = startDateSelected; startDate <= endDateSelected.AddDays(-daysNumber); startDate = startDate.AddDays(1))
            {
                isAvailable = isDateAvailable(startDate,endDate);
                if (isAvailable)
                {
                    availableDates.Add(startDate);
                }
            }

            AccommodationReservationConfirmationWindow accommodationReservationConfirmationWindow = new AccommodationReservationConfirmationWindow(selectedAccommodation);
            accommodationReservationConfirmationWindow.Show();


        }
        private bool checkDateRequirments()
        {
            if (startDatePicker.SelectedDate == null || endDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Molimo Vas da selektujete datume.");
                return false;
            }
            DateTime startDateSelected = startDatePicker.SelectedDate.Value;
            DateTime endDateSelected = endDatePicker.SelectedDate.Value;
            int daysNumber = (int)numberOfDays.Value;

            int minDays = selectedAccommodation.minDays;

            if (DateTime.Compare(startDateSelected, endDateSelected) > 0)
            {
                MessageBox.Show("Molimo Vas selektujete pravilno datume.");
                return false;
            }
            if (daysNumber < selectedAccommodation.minDays)
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
        private bool isDateAvailable(DateTime startDate, DateTime endDate)
        {

            foreach (AccommodationReservation reservation in _accommodationReservationController.GetAllReservations())
            {

                if (reservation.AccommodationId==selectedAccommodation.id)
                {
                    for (DateTime i=reservation.StartDate; i <= reservation.EndDate; i.AddDays(1))
                    {
                        for(DateTime j = startDate; j <= endDate; j.AddDays(1))
                        {
                            if (j == i)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

    }
}
