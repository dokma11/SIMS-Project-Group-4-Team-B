using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AllGuestOneReservationsView.xaml
    /// </summary>
    public partial class AllGuestOneReservationsViewModel : Window, IObserver
    {
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationReservationService _accommodationReservationService;
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }

        List<AccommodationReservation> FilteredData = new List<AccommodationReservation>();
        public User User { get; set; }

        public AllGuestOneReservationsView AllGuestOneReservationsView;

        private AccommodationGradeService _accommodationGradeService;

        public AllGuestOneReservationsViewModel(AllGuestOneReservationsView allGuestOneReservationsView, User guest1, AccommodationReservationService accommodationReservationService, AccommodationGradeService accommodationGradeService)
        {
            _accommodationReservationService = accommodationReservationService;
            _accommodationReservationService.Subscribe(this);
            _accommodationGradeService = accommodationGradeService;
            AllGuestOneReservationsView = allGuestOneReservationsView;
            User = guest1;

            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationService.GetAllReservations());

            FilteredData = _accommodationReservationService.FindSuitablePastReservations(User);
            AllGuestOneReservationsView.myDataGrid.ItemsSource = FilteredData;
        }

        public bool GradingIsPossible(AccommodationReservation selectedAccommodationReservation)
        {
            if (SelectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smestaj koji zelite da ocenite.");
                return false;
            }
            if (SelectedAccommodationReservation.Graded)
            {
                MessageBox.Show("Vec ste ocenili ovaj smestaj.");
                return false;
            }
            TimeSpan difference = DateTime.Today - selectedAccommodationReservation.EndDate;
            if (difference.TotalDays >= 5)
            {
                MessageBox.Show($"Rok za ocenjivanje smestaja je pet dana od kraja rezervacije. Proslo je {difference.TotalDays}!");
                return false;
            }
            return true;
        }

        public void Update()
        {
            FilteredData.Clear();
            FilteredData = _accommodationReservationService.FindSuitablePastReservations(User);
            AllGuestOneReservationsView.myDataGrid.ItemsSource = FilteredData;
        }

        internal void BackgroundShading()
        {
            AllGuestOneReservationsView.Overlay1.Visibility = Visibility.Visible;
        }

        internal void BackgroundUnshading()
        {
            AllGuestOneReservationsView.Overlay1.Visibility = Visibility.Collapsed;
        }

        internal bool RecommodtionIsPossible(AccommodationReservation selectedAccommodationReservation)
        {
            if (SelectedAccommodationReservation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smestaj za koji zelite da ostavite preporuku.");
                return false;
            }
            if (SelectedAccommodationReservation.RecommendedRenovation)
            {
                MessageBox.Show("Vec ste poslali preporuku za renoviranje.");
                return false;
            }
            TimeSpan difference = DateTime.Today - selectedAccommodationReservation.EndDate;
            if (difference.TotalDays >= 5)
            {
                MessageBox.Show($"Rok za ocenjivanje smestaja je pet dana od kraja rezervacije. Proslo je {difference.TotalDays}!");
                return false;
            }
            if (!SelectedAccommodationReservation.Graded)
            {
                MessageBox.Show("Morate da ocenite ovaj smestaj prvo.");
                return false;
            }
            return true;
        }

        public AccommodationGrade FindAccommodationGrade(AccommodationReservation selectedAccommodationReservation)
        {
            return _accommodationGradeService.FindGrade(selectedAccommodationReservation);
        }
    }
}
