﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmationWindow.xaml
    /// </summary>
    public partial class AccommodationReservationDateViewModel : Window
    {
        public List<AccommodationStay> stays = new List<AccommodationStay>();

        private List<AccommodationRenovation> accommodationRenovations = new();

        private ObservableCollection<AccommodationStay> _stays = new ObservableCollection<AccommodationStay>();

        private AccommodationService _accommodationService;
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        private AccommodationReservationService _accommodationReservationService;

        private AccommodationRenovationService _accommodationRenovationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationStay SelectedAccommodationStay { get; set; }
        public User User { get; set; }
        public int ReservationId { get; set; }

        public List<DateTime> AvailableDates = new List<DateTime>();
        public List<DateTime> AdditionalAvailableDates = new List<DateTime>();
        bool todaysDay;
        int daysNumber;
        int guestsNumber;

        private AccommodationReservationDateView AccommodationReservationDateView;

        public AccommodationReservationDateViewModel(AccommodationReservationDateView accommodationReservationDateView, int reservationId, Accommodation selectedAccommodation, User guest1)
        {
            AccommodationReservationDateView = accommodationReservationDateView;

            User = guest1;
            ReservationId = reservationId;
            SelectedAccommodation = selectedAccommodation;

            stays = new List<AccommodationStay>();
            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodations());

            _accommodationReservationService = new AccommodationReservationService();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllReservations());
            _accommodationRenovationService = new AccommodationRenovationService();
            List<AccommodationRenovation> accommodationRenovations = _accommodationRenovationService.GetAll();

            AvailableDates = new List<DateTime>();
            AdditionalAvailableDates = new List<DateTime>();

            AccommodationReservationDateView.startDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            AccommodationReservationDateView.endDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));

            todaysDay = false;

        }

        public void MakeReservation_Click()
        {
            _stays.Clear();
            stays.Clear();
            int possibleDatesNumber = 0;
            AccommodationReservationDateView.availableDatesGrid.ItemsSource = _stays;

            if (!CheckDateRequirments())
            {
                return;
            }

            DateTime startDateSelected = AccommodationReservationDateView.startDatePicker.SelectedDate.Value;
            DateTime endDateSelected = AccommodationReservationDateView.endDatePicker.SelectedDate.Value;
            int stayLength = (int)AccommodationReservationDateView.numberOfDays.Value;
            int numberOfGuests = (int)AccommodationReservationDateView.numberOfGuests.Value;
            daysNumber = stayLength;
            guestsNumber = numberOfGuests;

            possibleDatesNumber = _accommodationReservationService.CheckDates(SelectedAccommodation, startDateSelected, endDateSelected, stayLength, AvailableDates, accommodationRenovations);

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
            MessageBox.Show("Za datume koje ste izabrali nismo uspeli da pronađemo nijedan slobodan termin. Ukoliko želite možete izabrati neki od slobodnih termina koji su najsličniji onima koji ste želeli.");
            int datesFound = 0;

            while (datesFound < 3)
            {
                if (todaysDay)
                {
                    datesFound = _accommodationReservationService.CheckDates(SelectedAccommodation, startDateSelected, endDateSelected = endDateSelected.AddDays(1), stayLength, AdditionalAvailableDates, accommodationRenovations);
                }
                else
                {
                    datesFound = _accommodationReservationService.CheckDates(SelectedAccommodation, startDateSelected = startDateSelected.AddDays(-1), endDateSelected = endDateSelected.AddDays(1), stayLength, AdditionalAvailableDates, accommodationRenovations);
                }

            }
            if (AdditionalAvailableDates.Count > 0)
            {
                CreateAvailableStayList(AdditionalAvailableDates, stayLength);
            }
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
            if (AccommodationReservationDateView.startDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Molimo Vas da selektujete datum početka.");
                return false;
            }
            if (AccommodationReservationDateView.endDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Molimo Vas da selektujete datum kraja.");
                return false;
            }

            DateTime startDateSelected = AccommodationReservationDateView.startDatePicker.SelectedDate.Value;
            DateTime endDateSelected = AccommodationReservationDateView.endDatePicker.SelectedDate.Value;
            int stayLength = (int)AccommodationReservationDateView.numberOfDays.Value;
            int numberOfGuests = (int)AccommodationReservationDateView.numberOfGuests.Value;

            int minDays = SelectedAccommodation.MinDays;

            if (startDateSelected == DateTime.Today)
            {
                todaysDay = true;
            }
            if (numberOfGuests > SelectedAccommodation.MaxGuests)
            {
                MessageBox.Show($"Ovaj smeštaj nije u mogućnosti da primi toliko ljudi. Kapacitet je {SelectedAccommodation.MaxGuests}.");
                return false;
            }
            if (DateTime.Compare(startDateSelected, endDateSelected) > 0)
            {
                MessageBox.Show("Molimo Vas selektujete pravilno datume.");
                return false;
            }
            if (stayLength < SelectedAccommodation.MinDays)
            {
                MessageBox.Show($"Za ovaj smeštaj minimalni broj dana koji možete rezervisati je  {minDays}.");
                return false;
            }
            if ((endDateSelected - startDateSelected).TotalDays < daysNumber)
            {
                MessageBox.Show($"Niste dobro uneli podatke. Vremensko ograničenje nije dovoljno da bi se rezervisao {daysNumber} dana.");
                return false;
            }
            return true;

        }

        public bool ButtonDateConfirmation_Check(AccommodationStay selectedAccommodationStay)
        {
            if (selectedAccommodationStay == null)
            {
                MessageBox.Show("Molimo Vas selektujte datume koje zelite da rezervišete.");
                return false;
            }
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Došlo je do greške.");
                return false;
            }
            return true;
        }
    }
}
