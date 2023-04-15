using Sims2023.Application.Services;
using Sims2023.Controller;
using System.Windows;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using Microsoft.Win32;
using Sims2023.View.Guest1;
using System.Reflection;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;
using Path = System.IO.Path;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for addAccommodationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window
    {
        private Accommodation Accommodation { get; set; }
        User user { get; set; }
        public AccommodationRegistrationViewModel AccommodationRegistrationViewModel;
        private List<string> _addedPictures = new List<string>();
        List<BitmapImage> imageList = new List<BitmapImage>();
        private List<BitmapImage> _permanentPictures = new List<BitmapImage>();

        public AccommodationRegistrationView(AccommodationService accommodationCtrl1, User owner)
        {
            InitializeComponent();
            DataContext = this;
            AccommodationRegistrationViewModel = new AccommodationRegistrationViewModel(accommodationCtrl1, owner);
            Accommodation = new Accommodation();
            user = owner;
            _addedPictures = new List<string>();
            imageList = new List<BitmapImage>();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            int Id = 0;
            string Name = textBox.Text;
            string Town = textBox1.Text;
            string Country = textBox2.Text;
            string Type = comboBox.Text;
            string MaxGuests1 = textBox3.Text;
            string mindays = textBox4.Text;
            string CancelDayss = textBox5.Text;

            if (IsValid(MaxGuests1,mindays,CancelDayss,Town,Country,Name,Type))
            {
                int maxguests = int.Parse(MaxGuests1);
                int mindayss = int.Parse(mindays);
                int canceldays = int.Parse(mindays);
                Location location = new Location(0, Town, Country);
                AccommodationRegistrationViewModel.CreateLocation(location);

                Accommodation = new Accommodation(Id, Name, location, Type, maxguests, mindayss, canceldays, _addedPictures, user);
                AccommodationRegistrationViewModel.CreateAccommodation(Accommodation);
                MessageBox.Show("uspijsna registracija smjestaja");
                Close();
            }
            else MessageBox.Show("Niste dobro popunili sve podatke");
        }
       
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
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
               PicturesListView.ItemsSource = null;
               PicturesListView.ItemsSource = _permanentPictures;
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
    }
}