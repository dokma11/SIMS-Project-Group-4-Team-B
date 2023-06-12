using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest2Views;
using System.Globalization;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class TourListViewModel:IObserver,INotifyPropertyChanged
    {
        private TourService _tourService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public TourListView TourListView { get; set; }
        public List<Tour> FilteredData { get; set; }
        public Tour SelectedTour { get; set; }
        public User User { get; set; }
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours {
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
        public TourListViewModel(TourListView tourListView,User user)
        {
            TourListView = tourListView;
            FilteredData = new List<Tour>();
            _tourService = new TourService();
            _countriesAndCitiesService = new CountriesAndCitiesService();
            Tours = new ObservableCollection<Tour>(_tourService.GetCreated());
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

            FilteredData = _tourService.GetFiltered(citySearchTerm, countrySearchTerm, lengthSearchTerm, guideLanguageSearchTerm, maxGuestNumberSearchTerm);
            TourListView.dataGridTours.ItemsSource = FilteredData;
        }

        public List<CountriesAndCities> GetCitiesAndCountries()
        {
            return _countriesAndCitiesService.GetAllLocations();
        }


        
        public bool IsNull(Tour selectedTour)
        {
            
            
            if (selectedTour == null)
            {
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    MessageBox.Show("Izaberite turu");
                    return true;
                }
                else
                {
                    MessageBox.Show("Choose the tour");
                    return true;
                }
            }

            return false;
        }




        public void DisplaySelectedTour()
        {
            FilteredData.Clear();
            FilteredData.Add(SelectedTour);
            TourListView.dataGridTours.ItemsSource = FilteredData;
            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {
                MessageBox.Show($"U ponudi je ostalo još {SelectedTour.AvailableSpace} slobodnih mesta.");
            }
            else
            {
                MessageBox.Show($"There are still  vacancies left {SelectedTour.AvailableSpace}  vacancies left");
            }
        }

        public void DisplayAlternativeTours(int reservedSpace, Tour selectedTour)
        {
            TourListView.dataGridTours.ItemsSource = _tourService.GetAlternatives(reservedSpace, selectedTour);
            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {
                MessageBox.Show("Nema slobodnih mesta, ali imamo na istoj lokaciji u ponudi:");
            }
            else
            {
                MessageBox.Show("There are no vacancies, but we have in the same location on offer:");
            }
            
        }

        

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
