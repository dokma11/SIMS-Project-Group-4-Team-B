using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class TourListViewModel:IObserver
    {
        private TourService _tourService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public TourListView TourListView { get; set; }
        public List<Tour> FilteredData { get; set; }
        public Tour SelectedTour { get; set; }
        public User User { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public TourListViewModel(TourListView tourListView,User user)
        {
            TourListView = tourListView;
            FilteredData = new List<Tour>();
            _tourService = new TourService();
            _countriesAndCitiesService = new CountriesAndCitiesService();
            Tours = new ObservableCollection<Tour>(_tourService.GetCreated());

            SelectedTour = null;
            User = user;
        }
        public void SearchTours_Click()
        {
            string citySearchTerm = TourListView.citySearchBox.Text.ToLower();
            string countrySearchTerm = TourListView.countrySearchBox.Text.ToLower();
            string lengthSearchTerm = TourListView.lengthSearchBox.Text.ToLower();
            string guideLanguageSearchTerm = TourListView.guideLanguageSearchBox.Text.ToLower();
            int maxGuestNumberSearchTerm = (int)TourListView.guestNumberBox.Value;

            FilteredData = _tourService.GetFiltered(citySearchTerm, countrySearchTerm, lengthSearchTerm, guideLanguageSearchTerm, maxGuestNumberSearchTerm);
            TourListView.dataGridTours.ItemsSource = FilteredData;
        }

        public List<CountriesAndCities> GetCitiesAndCountries()
        {
            return _countriesAndCitiesService.GetAllLocations();
        }


        /* public void ReserveTour_Click()
         {
             int reservedSpace = (int)TourListView.guestNumberBox.Value;

             if (IsNull(SelectedTour))
                 return;

             if (SelectedTour.AvailableSpace >= reservedSpace)
             {
                 TourReservation tourReservation = new TourReservation(SelectedTour, User, reservedSpace);
                 _tourReservationService.Create(tourReservation);
                 _tourService.UpdateAvailableSpace(reservedSpace, SelectedTour);

                 Update();
                 MessageBox.Show("Uspesna rezervacija");
                 CheckVouchers(tourReservation);

                 ShowVoucherListView();

             }
             else if (SelectedTour.AvailableSpace > 0)
             {
                 DisplaySelectedTour();
             }
             else
             {
                 DisplayAlternativeTours(reservedSpace, SelectedTour);
             }
         }*/

        public bool IsNull(Tour selectedTour)
        {
            if (selectedTour == null)
            {
                MessageBox.Show("Izaberite turu");
                return true;
            }

            return false;
        }




        public void DisplaySelectedTour()
        {
            FilteredData.Clear();
            FilteredData.Add(SelectedTour);
            TourListView.dataGridTours.ItemsSource = FilteredData;

            MessageBox.Show($"U ponudi je ostalo još {SelectedTour.AvailableSpace} slobodnih mesta.");
        }

        public void DisplayAlternativeTours(int reservedSpace, Tour selectedTour)
        {
            TourListView.dataGridTours.ItemsSource = _tourService.GetAlternatives(reservedSpace, selectedTour);
            MessageBox.Show("Nema slobodnih mesta, ali imamo na istoj lokaciji u ponudi:");
        }

        /*public void ShowVoucherListView()
        {
            var voucherListView = new VoucherListView(User);
            voucherListView.Show();
        }

        public void CheckVouchers(TourReservation tourReservation)
        {
            if (_tourReservationService.CheckVouchers(tourReservation))
            {
                Voucher Voucher = new Voucher(Voucher.VoucherType.FiveReservations, User, SelectedTour);
                _voucherService.Create(Voucher);
            }
        }*/

        public void SeeDetails_Click()
        {
            if (IsNull(SelectedTour))
                return;
            else
            {
                TourDetailedView TourDetailedView = new TourDetailedView(SelectedTour);
                TourDetailedView.Show();
            }
        }

        
        public void Update()
        {
            TourListView.dataGridTours.ItemsSource = new ObservableCollection<Tour>(_tourService.GetCreated());
        }
    }
}
