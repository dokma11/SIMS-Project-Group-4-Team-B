using Microsoft.VisualBasic;
using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Sims2023.View
{
    public partial class AccommodationListWindow : Window
    {
        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation selectedAccommodation { get; set; }


        private AccomodationLocationController _accommodationLocationController;
        public ObservableCollection<AccommodationLocation> AccommodationLocations { get; set; }

        List<Accommodation> filteredData = new List<Accommodation>();
        public AccommodationListWindow()
        {
            InitializeComponent();
            DataContext = this;
            _accommodationLocationController = new AccomodationLocationController();

            AccommodationLocations = new ObservableCollection<AccommodationLocation>(_accommodationLocationController.GetAllAccommodationLocations());

            _accommodationController = new AccommodationController();

            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllAccommodations());
            List<Accommodation> filteredData = new List<Accommodation>();

            AddLocationToAccommodation(AccommodationLocations, Accommodations);
        }

        private void AddLocationToAccommodation(ObservableCollection<AccommodationLocation> accommodationLocations, ObservableCollection<Accommodation> accommodations)
        {
            foreach(var accommodation in accommodations)
            {
                foreach(var location in accommodationLocations)
                {
                    if(accommodation.locationId==location.id)
                    {
                        accommodation.city = location.city;
                        accommodation.country = location.country;
                    }
                }
            }
        }

        private void searchAccommodationClick(object sender, RoutedEventArgs e)
        {
            filteredData.Clear();
            myDataGrid.ItemsSource = Accommodations;

            string nameSearchTerm = nameSearchBox.Text;
            string citySearchTerm = citySearchBox.Text;
            string countrySearchTerm = countrySearchBox.Text;
            string typeSearchTerm = typeComboBox.Text;
            int maxGuests = (int)numberOfGuests.Value;
            int minDays = (int)numberOfDays.Value;


            foreach (Accommodation accommodation in Accommodations)
            {
                bool nameCondition = true;
                bool cityCondition = true;
                bool countryCondition = true;
                bool typeCondition = true;
                bool maxGuestsCondition = true;
                bool minDaysCondition = true;

                if (!string.IsNullOrEmpty(nameSearchTerm))
                {
                    if (!accommodation.name.ToLower().Contains(nameSearchTerm.ToLower()))
                    {
                        nameCondition = false;
                    }
                }

                if (!string.IsNullOrEmpty(citySearchTerm))
                {
                    if (!accommodation.city.ToLower().Contains(citySearchTerm.ToLower()))
                    {
                        cityCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(countrySearchTerm))
                {
                    if (!accommodation.country.ToLower().Contains(countrySearchTerm.ToLower()))
                    {
                        countryCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(typeSearchTerm))
                {
                    if (!accommodation.type.ToLower().Contains(typeSearchTerm.ToLower()))
                    {
                        typeCondition = false;
                    }
                }
                if (numberOfGuests.Value > 0)
                {
                    if (accommodation.maxGuests < maxGuests)
                    {
                        maxGuestsCondition = false;
                    }
                }
                if (numberOfDays.Value > 0)
                {
                    if (accommodation.minDays < minDays)
                    {
                        minDaysCondition = false;
                    }
                }

                if (nameCondition && cityCondition && countryCondition && typeCondition && maxGuestsCondition && minDaysCondition)
                {
                    filteredData.Add(accommodation);

                }

            }

            myDataGrid.ItemsSource = filteredData;

        }

        private void giveUpSearchClick(object sender, RoutedEventArgs e)
        {
            filteredData.Clear();
            myDataGrid.ItemsSource = Accommodations;
        }

        private void buttonReservation_Click(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an accommodation from the list.");
                return;
            }
            Accommodation selectedAccommodation = (Accommodation)myDataGrid.SelectedItem;

            AccommodationReservationWindow accommodationReservationWindow = new AccommodationReservationWindow(selectedAccommodation);
            accommodationReservationWindow.Show();
        }
    }
}
