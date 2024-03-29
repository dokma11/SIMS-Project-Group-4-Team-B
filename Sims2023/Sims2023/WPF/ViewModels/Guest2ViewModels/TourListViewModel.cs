﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest2Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class TourListViewModel : IObserver, INotifyPropertyChanged
    {
        private TourService _tourService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public TourListView TourListView { get; set; }
        public List<Tour> FilteredData { get; set; }
        public Tour SelectedTour { get; set; }
        public User User { get; set; }
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                if (_tours != value)
                {
                    _tours = value;
                    OnPropertyChanged("Tours");
                }
            }
        }
        public TourListViewModel(TourListView tourListView, User user)
        {
            TourListView = tourListView;
            FilteredData = new List<Tour>();
            _tourService = new TourService();
            _countriesAndCitiesService = new CountriesAndCitiesService();
            Tours = new ObservableCollection<Tour>(_tourService.GetCreated().Where(t => t.Guide.SuperGuide == true)
                                                                .Concat(_tourService.GetCreated().Where(t => !t.Guide.SuperGuide)));
            foreach (Tour tour in Tours)
            {
                tour.PropertyChanged += Tour_PropertyChanged;
            }
            SelectedTour = null;
            User = user;
        }
        public void SearchTours_Click()
        {
            string citySearchTerm = TourListView.citySearchBox.Text.ToLower();
            string countrySearchTerm = TourListView.countrySearchBox.Text.ToLower();
            string lengthSearchTerm = TourListView.lengthSearchBox.Text;
            string guideLanguageSearchTerm = TourListView.guideLanguageSearchBox.Text.ToLower();
            int maxGuestNumberSearchTerm = (int)TourListView.guestNumberBox.Value;

            FilteredData = _tourService.GetFiltered(citySearchTerm, countrySearchTerm, lengthSearchTerm, guideLanguageSearchTerm, maxGuestNumberSearchTerm)
                                       .OrderByDescending(t => t.Guide.SuperGuide).ToList();

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
            int reservedSpace = (int)TourListView.guestNumberBox.Value;
            if (IsNull(SelectedTour))
                return;

            if (SelectedTour.AvailableSpace >= reservedSpace)
            {
                TourDetailedView TourDetailedView = new TourDetailedView(SelectedTour, (int)TourListView.guestNumberBox.Value, User);
                TourDetailedView.Show();
            }
            else if (SelectedTour.AvailableSpace > 0)
            {
                DisplaySelectedTour();
            }
            else
            {
                DisplayAlternativeTours(reservedSpace, SelectedTour);
            }
        }


        public void Update()
        {
            TourListView.dataGridTours.ItemsSource = new ObservableCollection<Tour>(_tourService.GetCreated());
        }

        private void Tour_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Refresh the list of tours when the AvailableSpace property of a tour changes
            if (e.PropertyName == "AvailableSpace")
            {
                OnPropertyChanged("Tours");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
