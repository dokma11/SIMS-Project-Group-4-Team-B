using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class SchedulingRenovationAppointmentsViewModel
    {
        public List<AccommodationStay> stays = new List<AccommodationStay>();

        private ObservableCollection<AccommodationStay> _stays = new ObservableCollection<AccommodationStay>();

        private AccommodationService _accommodationService;

        private AccommodationRenovationService _accommodationRenovationService;

        public List<DateTime> AvailableDates = new List<DateTime>();

        public SchedulingRenovationAppointmentsView view;

        public Accommodation selectedAccommodation { get; set; }

        private AccommodationReservationService _accommodationReservationService;
        int daysNumber;
        private List<AccommodationRenovation> accommodationRenovations = new();

        public SchedulingRenovationAppointmentsViewModel(SchedulingRenovationAppointmentsView View, Accommodation selectedAccommodationn)
        {
            view = View;
            selectedAccommodation = selectedAccommodationn;
            view.datePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            view.datePicker1.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            _accommodationReservationService = new AccommodationReservationService();
            stays = new List<AccommodationStay>();
            AvailableDates = new List<DateTime>();
            _accommodationRenovationService = new AccommodationRenovationService();
            List<AccommodationRenovation> accommodationRenovations = _accommodationRenovationService.GetAll();
        }

        public void FindDates()
        {

            if (!CheckDateRequirments())
            {
                return;
            }
            DateTime startDateSelected = view.datePicker.SelectedDate.Value;
            DateTime endDateSelected = view.datePicker1.SelectedDate.Value;
            int stayLength = (int)view.numberOfDays.Value;
            daysNumber = stayLength;
            _accommodationReservationService.CheckDates(selectedAccommodation, startDateSelected, endDateSelected, stayLength, AvailableDates, accommodationRenovations);
            OriginalAvailableDatesFound(AvailableDates, stayLength);

            var dates = new AvailableDatesView(selectedAccommodation, stays);
            FrameManager.Instance.MainFrame.Navigate(dates);

        }

        public void OriginalAvailableDatesFound(List<DateTime> availableDates, int stayLength)
        {
            CreateAvailableStayList(availableDates, stayLength);
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
            if (view.datePicker.SelectedDate.Value == null || view.datePicker1.SelectedDate.Value == null)
            {
                MessageBox.Show("Molimo Vas da selektujete datume.");
                return false;
            }
            DateTime startDateSelected = view.datePicker.SelectedDate.Value;
            DateTime endDateSelected = view.datePicker1.SelectedDate.Value;
            int stayLength = (int)view.numberOfDays.Value;

            if (DateTime.Compare(startDateSelected, endDateSelected) > 0)
            {
                MessageBox.Show("Molimo Vas selektujete pravilno datume.");
                return false;
            }

            if ((endDateSelected - startDateSelected).TotalDays < daysNumber)
            {
                MessageBox.Show($"Niste dobro uneli podatke. Vremensko ogranicenje nije dovoljno da bi se rezervisao {daysNumber} dana");
                return false;
            }
            return true;

        }
    }
}
