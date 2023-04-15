using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Sims2023.WPF.Views.Guest1Views;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerGradingView.xaml
    /// </summary>
    public partial class AccommodationAndOwnerGradingViewModel
    {
        private AccommodationAndOwnerGradingView _accommodationAndOwnerGradingView;

        private List<string> _addedPictures = new List<string>();

        private AccommodationGradeService _accommodationGradeController;
        public ObservableCollection<AccommodationGrade> AccommodationGrades { get; set; }

        private AccommodationReservationService _accommodationReservationController;
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationService _accommodationService;

        List<BitmapImage> imageList = new List<BitmapImage>();
        public User User { get; set; }
        public AccommodationAndOwnerGradingViewModel(AccommodationAndOwnerGradingView accommodationAndOwnerGradingView, AccommodationReservation SelectedAccommodationReservationn, User guest1, AccommodationReservationService accommodationReservationController)
        {
            _accommodationAndOwnerGradingView = accommodationAndOwnerGradingView;

            User = guest1;

            _accommodationReservationController = accommodationReservationController;

            _accommodationGradeController = new AccommodationGradeService();
            AccommodationGrades = new ObservableCollection<AccommodationGrade>(_accommodationGradeController.GetAllAccommodationGrades());

            _accommodationService = new AccommodationService();

            _addedPictures = new List<string>();
            imageList = new List<BitmapImage>();
            SelectedAccommodationReservation = SelectedAccommodationReservationn;

        }

        public void accept_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da ostavite ovu recenziju?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                AddCreatedGrade();
                _accommodationAndOwnerGradingView.Close();
            }
            else
            {
                return;
            }
        }

        public void AddCreatedGrade()
        {
            AccommodationGrade accommodationGrade = CreateGrade(SelectedAccommodationReservation);
            if(accommodationGrade != null)
            {
                _accommodationGradeController.Create(accommodationGrade);
                UpdateAccommodationReservation(SelectedAccommodationReservation);
                UpdateAccommodationImages(SelectedAccommodationReservation, _addedPictures);
                MessageBox.Show("Uspesno ste ocenili ovaj smestaj.");
            }

        }

        public void UpdateAccommodationImages(AccommodationReservation selectedAccommodationReservation, List<string> addedPictures)
        {
            foreach(var image in addedPictures)
            {
                selectedAccommodationReservation.Accommodation.Imageurls.Add(image);
                MessageBox.Show($"dodata slika {image}.");
            }
            _accommodationService.Update(SelectedAccommodationReservation.Accommodation);
            MessageBox.Show("Smestaj updejtovan");
        }

        public void UpdateAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            selectedAccommodationReservation.Graded = true;
            _accommodationReservationController.Update(SelectedAccommodationReservation);
        }

        
        public AccommodationGrade CreateGrade(AccommodationReservation selectedAccommodationReservation)
        {
            AccommodationGrade accommodationGrade = new AccommodationGrade();
            accommodationGrade.Guest = User;
            accommodationGrade.Accommodation = selectedAccommodationReservation.Accommodation;
            accommodationGrade.ValueForMoney = (int)_accommodationAndOwnerGradingView.valueForMoneyIntegerUpDown.Value;
            accommodationGrade.Cleanliness=(int)_accommodationAndOwnerGradingView.cleannessIntegerUpDown.Value;
            accommodationGrade.Owner=(int)_accommodationAndOwnerGradingView.ownerIntegerUpDown.Value;
            accommodationGrade.Comfort=(int)_accommodationAndOwnerGradingView.comfortIntegerUpDown.Value;
            accommodationGrade.Location=(int)_accommodationAndOwnerGradingView.locationIntegerUpDown.Value;
            accommodationGrade.Comment = _accommodationAndOwnerGradingView.textBox.Text;
            return accommodationGrade;

        }
        
        public void giveUp_Click(object sender, RoutedEventArgs e)
        {
            _accommodationAndOwnerGradingView.Close();
        }
        
        public void addPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    string relativePath = $"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/Guest1Images/{Path.GetFileName(filename)}";
                    Uri imageUri = new Uri(relativePath, UriKind.Absolute);
                    BitmapImage bitmapImage = new BitmapImage(imageUri);
                    imageList.Add(bitmapImage);
                    _addedPictures.Add(relativePath);
                }
                _accommodationAndOwnerGradingView.myListBox.ItemsSource = null;
                _accommodationAndOwnerGradingView.myListBox.ItemsSource = _addedPictures;

                _accommodationAndOwnerGradingView.PicturesListView.ItemsSource = null;
                _accommodationAndOwnerGradingView.PicturesListView.ItemsSource = imageList;
            }
        }

        public void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string itemToRemove = (string)button.Tag;
            RemovePictureFromListBox(itemToRemove);
            RemovePictureFromListView(itemToRemove);
        }

        public void RemovePictureFromListBox(string itemToRemove)
        {
            _addedPictures.Remove(itemToRemove);
            _accommodationAndOwnerGradingView.myListBox.ItemsSource = null;
            _accommodationAndOwnerGradingView.myListBox.ItemsSource = _addedPictures;
        }

        public void RemovePictureFromListView(string itemToRemove)
        {
            BitmapImage selectedBitmapImage = FindDeletingPicture(itemToRemove);
            
            if (selectedBitmapImage != null)
            {
                imageList.Remove(selectedBitmapImage);

            }

            _accommodationAndOwnerGradingView.PicturesListView.ItemsSource = null;
            _accommodationAndOwnerGradingView.PicturesListView.ItemsSource = imageList;
        }

        public BitmapImage FindDeletingPicture(string itemToRemove)
        {

            foreach (BitmapImage bitmapImage in _accommodationAndOwnerGradingView.PicturesListView.Items)
            {
                if (bitmapImage.UriSource.AbsoluteUri == itemToRemove)
                {
                    return bitmapImage;
                }
            }
            MessageBox.Show("nije pronadena slika");
            return null;
        }

    }
}
