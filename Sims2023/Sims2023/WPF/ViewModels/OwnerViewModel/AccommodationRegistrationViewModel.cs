using Microsoft.Win32;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class AccommodationRegistrationViewModel
    {
        private AccommodationService _accommodationService;

        private Accommodation Accommodation { get; set; }
        public User User { get; set; }
        public AccommodationRegistrationView _accommodationRegistrationView; 
        private LocationService _locationService;
        public AccommodationRegistrationView View;
 
        private List<string> _addedPictures = new List<string>();
        List<BitmapImage> imageList = new List<BitmapImage>();
        private List<BitmapImage> _permanentPictures = new List<BitmapImage>();

        public AccommodationRegistrationViewModel(AccommodationRegistrationView view,AccommodationService accommodationCtrl1, User owner)
        {
            _accommodationService = accommodationCtrl1;
            User = owner;
            _locationService = new LocationService();
            View = view;
            Accommodation = new Accommodation();
            _addedPictures = new List<string>();
            imageList = new List<BitmapImage>();
        }

        public bool IsValid(string MaxGuests1, string mindays, string CancelDayss, string city, string country, string name, string type)
        {
            int clean;
            bool isCleanValid = int.TryParse(MaxGuests1, out clean);
            int RespectRules;
            bool isRulesValid = int.TryParse(mindays, out RespectRules);
            int Communication;
            bool isCommunicationValid = int.TryParse(CancelDayss, out Communication);

            if (!isCleanValid) return false;
            else if (!isRulesValid) return false;
            else if (!isCommunicationValid) return false;
            else if (string.IsNullOrEmpty(city)) return false;
            else if (string.IsNullOrEmpty(country)) return false;
            else if (string.IsNullOrEmpty(name)) return false;
            else if (string.IsNullOrEmpty(type)) return false;

            else return true;
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
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

        public void Registration_Click(object sender, RoutedEventArgs e)
        {
            int Id = 0;
            string Name = View.textBox.Text;
            string Town = View.textBox1.Text;
            string Country = View.textBox2.Text;
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
                MessageBox.Show("uspijsna registracija smjestaja");
                View.Close();
            }
            else MessageBox.Show("Niste dobro popunili sve podatke");
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
