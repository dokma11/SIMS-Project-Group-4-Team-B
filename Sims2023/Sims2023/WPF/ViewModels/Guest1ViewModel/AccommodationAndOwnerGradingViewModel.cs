using Microsoft.Win32;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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

        private AccommodationGradeService _accommodationGradeService;
        public ObservableCollection<AccommodationGrade> AccommodationGrades { get; set; }

        private AccommodationReservationService _accommodationReservationService;
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationService _accommodationService;

        List<BitmapImage> imageList = new List<BitmapImage>();

        public User User { get; set; }

        public AccommodationAndOwnerGradingViewModel(AccommodationAndOwnerGradingView accommodationAndOwnerGradingView, AccommodationReservation selectedAccommodationReservation, User guest1, AccommodationReservationService accommodationReservationController, AccommodationGradeService accommodationGradeService)
        {
            _accommodationGradeService = accommodationGradeService;
            _accommodationAndOwnerGradingView = accommodationAndOwnerGradingView;

            User = guest1;

            _accommodationReservationService = accommodationReservationController;
            AccommodationGrades = new ObservableCollection<AccommodationGrade>(_accommodationGradeService.GetAllAccommodationGrades());

            _accommodationService = new AccommodationService();

            _addedPictures = new List<string>();
            imageList = new List<BitmapImage>();
            SelectedAccommodationReservation = selectedAccommodationReservation;
        }

        public void AddCreatedGrade(AccommodationGrade accommodationGrade)
        {
            accommodationGrade = CreateGrade(SelectedAccommodationReservation);
            if (accommodationGrade != null)
            {
                _accommodationGradeService.Create(accommodationGrade);
                UpdateAccommodationReservation(SelectedAccommodationReservation);
                UpdateAccommodationImages(SelectedAccommodationReservation, _addedPictures);
                MessageBox.Show("Uspesno ste ocenili ovaj smestaj.");
            }

        }

        public void UpdateAccommodationImages(AccommodationReservation selectedAccommodationReservation, List<string> addedPictures)
        {
            foreach (var image in addedPictures)
            {
                selectedAccommodationReservation.Accommodation.Imageurls.Add(image);
            }
            _accommodationService.Update(SelectedAccommodationReservation.Accommodation);
        }

        public void UpdateAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {
            selectedAccommodationReservation.Graded = true;
            _accommodationReservationService.Update(SelectedAccommodationReservation);
        }


        public AccommodationGrade CreateGrade(AccommodationReservation selectedAccommodationReservation)
        {
            AccommodationGrade accommodationGrade = new AccommodationGrade();
            accommodationGrade.Guest = User;
            accommodationGrade.Accommodation = selectedAccommodationReservation.Accommodation;
            accommodationGrade.ValueForMoney = (int)_accommodationAndOwnerGradingView.valueForMoneyIntegerUpDown.Value;
            accommodationGrade.Cleanliness = (int)_accommodationAndOwnerGradingView.cleannessIntegerUpDown.Value;
            accommodationGrade.Owner = (int)_accommodationAndOwnerGradingView.ownerIntegerUpDown.Value;
            accommodationGrade.Comfort = (int)_accommodationAndOwnerGradingView.comfortIntegerUpDown.Value;
            accommodationGrade.Location = (int)_accommodationAndOwnerGradingView.locationIntegerUpDown.Value;
            accommodationGrade.Comment = _accommodationAndOwnerGradingView.textBox.Text;
            accommodationGrade.CurrentAccommodationState = "Korisnik nije uneo podatke o trenutnom stanju smestaja";
            accommodationGrade.RenovationUrgency = "Korisnik nije uneo podatke o hitnosti renoviranja";
            accommodationGrade.ReservationStartDate = selectedAccommodationReservation.StartDate;
            return accommodationGrade;
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
