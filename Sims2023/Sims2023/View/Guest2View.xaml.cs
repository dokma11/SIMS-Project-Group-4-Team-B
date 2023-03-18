using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window, IObserver
    {

        private TourController _controllerTour;

        private LocationController _controllerLocation;

        private TourReservationController _controllerTourReservation;
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Location> Locations { get; set; }
        public Tour SelectedTour { get; set; }
        public Tour EditedTour { get; set; }

        List<Tour> filteredData = new List<Tour>();

        
        

        

        public TourReservation TourReservation { get; set; }

        public Guest2View()
        {
            InitializeComponent();
            DataContext = this;

            _controllerTour = new TourController();
            _controllerTour.Subscribe(this);
            _controllerLocation = new LocationController();
            _controllerLocation.Subscribe(this);
            _controllerTourReservation = new TourReservationController();
            _controllerTourReservation.Subscribe(this);

            Tours = new ObservableCollection<Tour>(_controllerTour.GetAllTours());
            Locations = new ObservableCollection<Location>(_controllerLocation.GetAllLocations());

            SelectedTour = new Tour();
            EditedTour = new Tour();
            filteredData = new List<Tour>();
            
            AddLocationsToTour(Locations, Tours);

            //int toursMaxGuestNumber = Tours.Max(y => y.MaxGuestNumber);
           // maxGuestNumberSearchBox.Maximum = toursMaxGuestNumber;
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

        private void SearchTours_Click(object sender, RoutedEventArgs e)
        {
            
            filteredData.Clear();
            dataGridTours.ItemsSource = Tours;

            string citySearchTerm = citySearchBox.Text;
            string countrySearchTerm = countrySearchBox.Text;
            string lengthSearchTerm= lengthSearchBox.Text;
            string guideLanguageSearchTerm = guideLanguageSearchBox.Text.ToLower();
            int maxGuestNumberSearchTerm = (int)guestNumberBox.Value;
            




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
                //if (guestNumberBox.Value > 0)
                {
                    if (tour.MaxGuestNumber < maxGuestNumberSearchTerm)
                    {
                        maxGuestNumberCondition = false;
                    }
                }


                if (cityCondition && countryCondition && lengthCondition && guideLanguageCondition && maxGuestNumberCondition)
                {
                    filteredData.Add(tour);

                }

            }

            dataGridTours.ItemsSource = filteredData;

            

            
        }

        private void ReserveTour_Click(object sender, RoutedEventArgs e)
        {

            int reservedSpace = (int)guestNumberBox.Value;


            if (isNull(SelectedTour))
                return;
            if (isEmpty(userIdBox))
                return;

            if (SelectedTour.AvailableSpace >= reservedSpace)
            {
                EditedTour = SelectedTour;
                TourReservation tourReservation = new TourReservation();

                tourReservation.TourId = SelectedTour.Id;
                tourReservation.UserId = Convert.ToInt32(userIdBox.Text);
                tourReservation.GuestNumber = reservedSpace;
                _controllerTourReservation.Create(tourReservation);
                UpdateTours(reservedSpace);
                dataGridTours.ItemsSource = Tours;
            }
            else if (SelectedTour.AvailableSpace > 0)
            {
                DisplaySelectedTour();
            }
            else
            {
                
                DisplayAlternativeTours(reservedSpace);
            }

        }

        private bool isNull(Tour selectedTour)
        {
            if (selectedTour == null)
            {
                MessageBox.Show("Izaberite turu");
                return true;
            }

            return false;
        }


        

        private bool isEmpty(TextBox textBox)
        {
            int value;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Popuni potrebno polje");
                return true;
            }
            if (!int.TryParse(textBox.Text, out value))
            {
                MessageBox.Show("Unesite broj");
                return true;
            }

            return false;
        }

        private void UpdateTours(int reservedSpace)
        {
            foreach (Tour tour in Tours)
            {
                if (EditedTour == tour)
                {
                    EditedTour.AvailableSpace -= reservedSpace;
                    _controllerTour.Edit(EditedTour, tour);
                    break;
                }
            }
            MessageBox.Show("Uspesna rezervacija");
        }

        private void DisplayAlternativeTours(int reserveSpace)
        {
            filteredData.Clear();
            dataGridTours.ItemsSource = Tours;
            foreach (Tour tour in Tours)
            {
                if (tour.LocationId == SelectedTour.LocationId && tour.AvailableSpace >= reserveSpace)
                    filteredData.Add(tour);
            }

            dataGridTours.ItemsSource = filteredData;
            MessageBox.Show("Izabrana tura je popunjena al u ponudi imamo ove ture sa istom lokacijom");
        }

        private void DisplaySelectedTour()
        {
            filteredData.Clear();
            dataGridTours.ItemsSource = Tours;
            foreach (Tour tour in Tours)
            {
                if (tour == SelectedTour)
                    filteredData.Add(tour);
            }

            dataGridTours.ItemsSource = filteredData;
            MessageBox.Show("U ponudi je ostalo jos navedeni broj slobodnih mesta");
        }
        private void UpdateToursList()
        {
            Tours.Clear();
            foreach (var tour in _controllerTour.GetAllTours())
            {
                Tours.Add(tour);
            }
        }

        public void Update()
        {
            UpdateToursList();
        }
    }
}
