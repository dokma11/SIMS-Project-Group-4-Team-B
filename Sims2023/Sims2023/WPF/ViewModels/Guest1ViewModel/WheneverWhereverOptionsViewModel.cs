using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class WheneverWhereverOptionsViewModel
    {
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

        public List<DateTime> AvailableDates = new ();
        public List<DateTime> AdditionalAvailableDates = new ();
        public List<Accommodation> FilteredData = new();
        public List<Accommodation> AvailableAccommodations = new();
        bool todaysDay;
        int daysNumber;
        int guestsNumber;
        public DateTime startDateSelected;
        public DateTime endDateSelected;
        public Frame mainFrame;

        public WheneverWhereverOptionsView WheneverWhereverOptionsView;


        public WheneverWhereverOptionsViewModel(WheneverWhereverOptionsView wheneverWhereverOptionsView, User user, int stayLength, int numberOfGuests, DateTime startDateSelected, DateTime endDateSelected,Frame mainFrame)
        {
            WheneverWhereverOptionsView = wheneverWhereverOptionsView;
            User = user;
            daysNumber = stayLength;
            guestsNumber = numberOfGuests;
            this.startDateSelected = startDateSelected;
            this.endDateSelected = endDateSelected;
            this.mainFrame = mainFrame;

            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
            _accommodationRenovationService = new AccommodationRenovationService();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodations());
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllReservations());

            AvailableDates = new List<DateTime>();
            AdditionalAvailableDates = new List<DateTime>();
            FindAccommodations();
            WheneverWhereverOptionsView.myDataGrid.ItemsSource = FilteredData;
        }

        public void FindAccommodations()
        {
            AvailableAccommodations = _accommodationService.GetAllAccommodations();
            foreach (Accommodation accommodation in AvailableAccommodations)
            {
                AddAccommodationToFilteredData(accommodation);
            }
            return;
        }

        private void AddAccommodationToFilteredData(Accommodation accommodation)
        {
            int numberOfAvailableDates = 0;
            numberOfAvailableDates = _accommodationReservationService.CheckDates(accommodation, startDateSelected, endDateSelected, daysNumber, AvailableDates, accommodationRenovations);
            if (numberOfAvailableDates > 0 && accommodation.MaxGuests >= guestsNumber && accommodation.MinDays<=daysNumber)
            {
                FilteredData.Add(accommodation);
            }
        }

        public void ConfirmReservation()
        {
            SelectedAccommodationStay = (AccommodationStay)WheneverWhereverOptionsView.availableDatesGrid.SelectedItem;
            AccommodationReservationReschedulingService _accommodationReservationReschedulingService = new AccommodationReservationReschedulingService();
            ObservableCollection<AccommodationReservationRescheduling> AccommodationReservationReschedulings = new ObservableCollection<AccommodationReservationRescheduling>(_accommodationReservationReschedulingService.GetAllReservationReschedulings());
            if (ButtonDateConfirmation_Check(SelectedAccommodationStay))
            {
                mainFrame.Navigate(new AccommodationReservationConfirmationView(-1, SelectedAccommodation, SelectedAccommodationStay, daysNumber, guestsNumber, User, AccommodationReservationReschedulings, _accommodationReservationReschedulingService, mainFrame));
            }
        }        

        public void FindDates()
        {
            if (SelectedAccommodation != null)
            {
                _stays.Clear();
                WheneverWhereverOptionsView.availableDatesGrid.ItemsSource = _stays;
                SelectedAccommodation = (Accommodation)WheneverWhereverOptionsView.myDataGrid.SelectedItem;

                int possibleDatesNumber = _accommodationReservationService.CheckDates(SelectedAccommodation, startDateSelected, endDateSelected, daysNumber, AvailableDates, accommodationRenovations);

                if (AvailableDates.Count > 0)
                {
                    OriginalAvailableDatesFound(AvailableDates, daysNumber);
                }

                if (AvailableDates.Count == 0)
                {
                    MessageBox.Show("Za datume koje ste izabrali nismo uspeli da pronađemo nijedan slobodan termin. Došlo je do greške.");
                }
                return;
            }
            MessageBox.Show("Izaberite smeštaj za koji želite da pretražite rezervacije.");
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
                _stays.Add(stay);
            }

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
                MessageBox.Show("Doslo je do greške.");
                return false;
            }
            return true;
        }
        public void GoBack()
        {
            NavigationService navigationService = NavigationService.GetNavigationService(WheneverWhereverOptionsView);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }

        public void DetailedView()
        {
            if(SelectedAccommodation!= null)
            {
                mainFrame.Navigate(new WhereverWheneverDetailedView(SelectedAccommodation, daysNumber, guestsNumber, User, startDateSelected, endDateSelected, mainFrame));
            }
            else
            {
                MessageBox.Show("Izaberite smeštaj koji želite da prikažete.");
            }
        }

        internal void SelectingFirstDataGrid(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Tab && Keyboard.Modifiers == ModifierKeys.None)
            {
                e.Handled = true;
                WheneverWhereverOptionsView.availableDatesGrid.Focus();
            }
        }

        internal void SelectingSecondDataGrid(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && Keyboard.Modifiers == ModifierKeys.None)
            {
                e.Handled = true;
                WheneverWhereverOptionsView.myDataGrid.Focus();
            }            
        }
    }
}
