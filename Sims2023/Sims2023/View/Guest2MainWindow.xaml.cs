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
using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.View;

namespace Sims2023
{
    /// <summary>
    /// Interaction logic for Guest2MainWindow.xaml
    /// </summary>
    public partial class Guest2MainWindow : Window, IObserver
    {

        private TourController _controllerTour;//
        public ObservableCollection<Tour> Tours { get; set; }//

        public Tour SelectedTour { get; set; }//

        List<Tour> filteredData = new List<Tour>();

        private LocationController _controllerLocation;  
        public ObservableCollection<Location> Locations { get; set; }


        public Guest2MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _controllerTour = new TourController();//
            _controllerTour.Subscribe(this);//
            _controllerLocation=new LocationController();

            Tours = new ObservableCollection<Tour>(_controllerTour.GetAllTours());
            Locations = new ObservableCollection<Location>(_controllerLocation.GetAllLocations());
            AddLocationsToTour(Locations, Tours);
            List<Tour> filteredData = new List<Tour>();
        }


        private void AddLocationsToTour(ObservableCollection<Location> locations, ObservableCollection<Tour> tours)
        {
            
                foreach (var tour in tours)
                {
                    foreach (var location in locations)
                    {
                        if (tour.LocationId == location.Id)
                        {
                            tour.City = location.City;
                            tour.Country = location.Country;
                        }
                    }
                }
            
        }

        private void SearchTours(object sender, RoutedEventArgs e)
        {

            filteredData.Clear();
            DataGridTours.ItemsSource = Tours;

            string citySearchTerm = CitySearchBox.Text;
            string countrySearchTerm = CountrySearchBox.Text;
            string lengthSearchTerm= LengthSearchBox.Text;
            string guideLanguageSearchTerm = GuideLanguageSearchBox.Text.ToLower();
            string maxGuestNumberSearchTerm = MaxGuestSearchBox.Text;




            foreach (Tour tour in Tours)
            {
                
                bool cityCondition = true;
                bool countryCondition = true;
                bool lengthCondition = true;
                bool guideLanguageCondition = true;
                bool maxGuestNumberCondition = true;

                if (!string.IsNullOrEmpty(citySearchTerm))
                {
                    if (!tour.City.ToLower().Contains(citySearchTerm.ToLower()))
                    {
                        cityCondition = false;
                    }
                }

                if (!string.IsNullOrEmpty(countrySearchTerm))
                {
                    if (!tour.Country.ToLower().Contains(countrySearchTerm.ToLower()))
                    {
                        countryCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(lengthSearchTerm))
                {
                    if (!tour.Length.ToString().ToLower().Contains(lengthSearchTerm.ToLower()))
                    {
                        lengthCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(guideLanguageSearchTerm))
                {
                    if (!tour.GuideLanguage.ToString().ToLower().Contains(guideLanguageSearchTerm.ToLower()))
                    {
                        guideLanguageCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(maxGuestNumberSearchTerm))
                {
                    int maxGuestNumberSearchTermConverted=Convert.ToInt32(maxGuestNumberSearchTerm);
                    if (tour.MaxGuestNumber < maxGuestNumberSearchTermConverted)
                    {
                        maxGuestNumberCondition = false;
                    }
                }
                

                if (cityCondition && countryCondition && lengthCondition && guideLanguageCondition && maxGuestNumberCondition)
                {
                    filteredData.Add(tour);

                }

            }

            DataGridTours.ItemsSource = filteredData;

            

            
        }

        

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
