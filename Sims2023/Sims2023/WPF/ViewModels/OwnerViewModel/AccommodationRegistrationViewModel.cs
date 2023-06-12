using Microsoft.Win32;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.FileHandler;
using Sims2023.View;
using Sims2023.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class AccommodationRegistrationViewModel
    {
        private AccommodationService _accommodationService;
        private List<CountriesAndCities> _countries;
        private CountriesAndCitiesService _allCountriesService;

        private Accommodation Accommodation { get; set; }
        public User User { get; set; }
        public AccommodationRegistrationView _accommodationRegistrationView;
        private LocationService _locationService;
        public AccommodationRegistrationView View;

        public RelayCommand Register { get; set; }
        public RelayCommand AddPicture { get; set; }

        private List<string> _addedPictures = new List<string>();
        List<BitmapImage> imageList = new List<BitmapImage>();
        private List<BitmapImage> _permanentPictures = new List<BitmapImage>();

        public AccommodationRegistrationViewModel(AccommodationRegistrationView view, User owner)
        {
            Register = new RelayCommand(Executed_RegistrationCommand, CanExecute_RegistrationCommand);
            AddPicture = new RelayCommand(Executed_AddPictureCommand, CanExecute_AddPictureCommand);
            _accommodationService = new AccommodationService();
            User = owner;
            _locationService = new LocationService();
            View = view;
            Accommodation = new Accommodation();
            _addedPictures = new List<string>();
            imageList = new List<BitmapImage>();
            _allCountriesService = new CountriesAndCitiesService();
            _countries = _allCountriesService.GetAllLocations();

            View.countryComboBox.ItemsSource = _countries;
            View.countryComboBox.DisplayMemberPath = "CountryName";
            View.countryComboBox.SelectedValuePath = "CountryName";
        }


        public void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            var selectedCountry = (CountriesAndCities)View.countryComboBox.SelectedItem;
            var cities = new List<string> { selectedCountry.City1, selectedCountry.City2, selectedCountry.City3, selectedCountry.City4, selectedCountry.City5 };

            // Bind the city ComboBox to the list of cities
            View.cityComboBox.ItemsSource = cities;
 
        }

        public bool IsValid(string MaxGuests1, string mindays, string CancelDayss, string city, string country, string name, string type)
        {
            int clean;
            bool isCleanValid = int.TryParse(MaxGuests1, out clean);
            int RespectRules;
            bool isRulesValid = int.TryParse(mindays, out RespectRules);
            int Communication;
            bool isCommunicationValid = int.TryParse(CancelDayss, out Communication);

            List<Control> invalidControls = new List<Control>();

            // Reset background color of all fields
            View.textBox3.ClearValue(TextBox.BackgroundProperty);
            View.textBox4.ClearValue(TextBox.BackgroundProperty);
            View.textBox5.ClearValue(TextBox.BackgroundProperty);
            View.textBox.ClearValue(TextBox.BackgroundProperty);
            View.comboBox.ClearValue(ComboBox.BackgroundProperty);

            if (!isCleanValid)
            {
                invalidControls.Add(View.textBox3);
            }
            if (!isRulesValid)
            {
                invalidControls.Add(View.textBox4);
            }
            if (!isCommunicationValid)
            {
                invalidControls.Add(View.textBox5);
            }
            if (string.IsNullOrEmpty(city))
            {
                invalidControls.Add(View.countryComboBox);
            }
            if (string.IsNullOrEmpty(country))
            {
                invalidControls.Add(View.cityComboBox);
            }
            if (string.IsNullOrEmpty(name))
            {
                invalidControls.Add(View.textBox);
            }
            if (string.IsNullOrEmpty(type))
            {
                invalidControls.Add(View.comboBox);
            }

            if (invalidControls.Count > 0)
            {
                SetControlsBackground(invalidControls);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetControlsBackground(List<Control> controls)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
            foreach (Control control in controls)
            {
                control.Background = redBrush;
            }
        }
        private bool CanExecute_AddPictureCommand(object obj)
        {
            return true;
        }


        public void Executed_AddPictureCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                imageList.Clear(); // Clear the imageList collection before adding new images
                foreach (string filename in openFileDialog.FileNames)
                {
                    string relativePath = $"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/OwnerImages/{Path.GetFileName(filename)}";
                    Uri imageUri = new Uri(relativePath, UriKind.Absolute);
                    BitmapImage bitmapImage = new BitmapImage(imageUri);
                    imageList.Add(bitmapImage);
                    _addedPictures.Add(relativePath);
                    _permanentPictures.Add(bitmapImage);
                }
                View.PicturesListView.ItemsSource = null;
                View.PicturesListView.ItemsSource = _permanentPictures;
            }
        }

        private bool CanExecute_RegistrationCommand(object obj)
        {
            return true;
        }
        public void Executed_RegistrationCommand(object obj)
        {
            int Id = 0;
            string Name = View.textBox.Text;
            string Country = View.countryComboBox.Text;
            string Town = View.cityComboBox.Text;
            string Type = View.comboBox.Text;
            string MaxGuests1 = View.textBox3.Text;
            string mindays = View.textBox4.Text;
            string CancelDayss = View.textBox5.Text;

            if (IsValid(MaxGuests1, mindays, CancelDayss, Town, Country, Name, Type))
            {
                int maxguests = int.Parse(MaxGuests1);
                int mindayss = int.Parse(mindays);
                int canceldays = int.Parse(mindays);
                Location location = new Location(0, Town, Country);
                CreateLocation(location);

                Accommodation = new Accommodation(Id, Name, location, Type, maxguests, mindayss, canceldays, _addedPictures, User);
                CreateAccommodation(Accommodation);
                View.textBox.Text = string.Empty;
            //    View.cityComboBox.Text = string.Empty;
            //    View.countryComboBox.Text = string.Empty;
                View.comboBox.SelectedIndex = -1;
                View.textBox3.Text = string.Empty;
                View.textBox4.Text = string.Empty;
                View.textBox5.Text = string.Empty;
                _addedPictures.Clear();
                _permanentPictures.Clear();
                View.PicturesListView.ItemsSource = null;
                ToastNotificationService.ShowInformation("Uspiješna registracija smještaja");

                View.textBox.Text = string.Empty;
                View.comboBox.SelectedIndex = -1;
                View.textBox3.Text = string.Empty;
                View.textBox4.Text = string.Empty;
                View.textBox5.Text = string.Empty;
                _addedPictures.Clear();
                _permanentPictures.Clear();
                View.PicturesListView.ItemsSource = null;
            }
            else ToastNotificationService.ShowInformation("Niste unijeli sve podatke");
        }


        public void CreateLocation(Location location)
        {
            _locationService.Create(location);
        }

        public void CreateAccommodation(Accommodation accommodation)
        {
            _accommodationService.Create(accommodation);
        }
   }
}
