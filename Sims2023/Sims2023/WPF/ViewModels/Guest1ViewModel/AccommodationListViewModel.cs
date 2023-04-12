using Microsoft.VisualBasic;
using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
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

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public partial class AccommodationListViewModel
    {
        private AccommodationListView AccommodationListView;

        private AccommodationService _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public UserService _userController;
        public Accommodation SelectedAccommodation { get; set; }

        public User User { get; set; }

        private AccomodationLocationController _accommodationLocationController;
        public ObservableCollection<AccommodationLocation> AccommodationLocations { get; set; }

        public List<Accommodation> FilteredData = new List<Accommodation>();

        public AccommodationListViewModel(AccommodationListView accommodationListView, User guest1)
        {
            AccommodationListView = accommodationListView; 
            _userController = new UserService();

            User = guest1;
            _accommodationLocationController = new AccomodationLocationController();
            AccommodationLocations = new ObservableCollection<AccommodationLocation>(_accommodationLocationController.GetAllAccommodationLocations());

            _accommodationController = new AccommodationService();

            _userController.MarkSuperOwner();
            _accommodationController = new AccommodationService();
            //   _accommodationController.Update();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllAccommodations());
            List<Accommodation> FilteredData = new List<Accommodation>();
        }

        public void SearchAccommodation_Click(object sender, RoutedEventArgs e)
        {
            FilteredData.Clear();
            AccommodationListView.myDataGrid.ItemsSource = Accommodations;

            string nameSearchTerm = AccommodationListView.nameSearchBox.Text;
            string citySearchTerm = AccommodationListView.citySearchBox.Text;
            string countrySearchTerm = AccommodationListView.countrySearchBox.Text;
            string typeSearchTerm = AccommodationListView.typeComboBox.Text;
            int maxGuests = (int)AccommodationListView.numberOfGuests.Value;
            int minDays = (int)AccommodationListView.numberOfDays.Value;

            CheckSetConditions(nameSearchTerm, citySearchTerm, countrySearchTerm, typeSearchTerm, maxGuests, minDays);

            AccommodationListView.myDataGrid.ItemsSource = FilteredData;

        }
        public void CheckSetConditions(string nameSearchTerm, string citySearchTerm, string countrySearchTerm, string typeSearchTerm, int maxGuests, int minDays)
        {
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
                    if (!accommodation.Name.ToLower().Contains(nameSearchTerm.ToLower()))
                    {
                        nameCondition = false;
                    }
                }

                if (!string.IsNullOrEmpty(citySearchTerm))
                {
                    if (!accommodation.Location.City.ToLower().Contains(citySearchTerm.ToLower()))
                    {
                        cityCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(countrySearchTerm))
                {
                    if (!accommodation.Location.Country.ToLower().Contains(countrySearchTerm.ToLower()))
                    {
                        countryCondition = false;
                    }
                }
                if (!string.IsNullOrEmpty(typeSearchTerm))
                {
                    if (!accommodation.Type.ToLower().Contains(typeSearchTerm.ToLower()))
                    {
                        typeCondition = false;
                    }
                }
                if (AccommodationListView.numberOfGuests.Value > 0)
                {
                    if (accommodation.MaxGuests < maxGuests)
                    {
                        maxGuestsCondition = false;
                    }
                }
                if (AccommodationListView.numberOfDays.Value > 0)
                {
                    if (accommodation.MinDays > minDays)
                    {
                        minDaysCondition = false;
                    }
                }

                if (nameCondition && cityCondition && countryCondition && typeCondition && maxGuestsCondition && minDaysCondition)
                {
                    FilteredData.Add(accommodation);

                }

            }
        }
        public void GiveUpSearch_Click(object sender, RoutedEventArgs e)
        {
            FilteredData.Clear();
            AccommodationListView.myDataGrid.ItemsSource = Accommodations;
        }

        public void ButtonReservation_Click(object sender, RoutedEventArgs e)
        {
            SelectedAccommodation = (Accommodation)AccommodationListView.myDataGrid.SelectedItem;
            
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Molimo Vas selektujte smestaj koji zelite da rezervisete.");
                return;
            }
            AccommodationReservationDateView accommodationReservationDateView = new AccommodationReservationDateView(-1,SelectedAccommodation,User);
            accommodationReservationDateView.Show();
        }

        public void Back_Click(object sender, RoutedEventArgs e)
        {
            AccommodationListView.Close();
        }
    }
}
