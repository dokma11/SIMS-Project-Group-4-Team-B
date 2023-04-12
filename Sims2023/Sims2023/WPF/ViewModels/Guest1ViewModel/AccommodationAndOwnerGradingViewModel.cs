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
using Sims2023.Model;
using Sims2023.WPF.Views.Guest1Views;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerGradingView.xaml
    /// </summary>
    public partial class AccommodationAndOwnerGradingViewModel
    {
        private AccommodationAndOwnerGradingView _accommodationAndOwnerGradingView;

        private List<string> _addedPictures = new List<string>();
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationGradeController _accommodationGradeController;
        public ObservableCollection<AccommodationGrade> AccommodationGrades { get; set; }

        private AccommodationReservationService _accommodationReservationController;

        private AccommodationService _accommodationController;

        public User User { get; set; }
        public AccommodationAndOwnerGradingViewModel(AccommodationAndOwnerGradingView accommodationAndOwnerGradingView, AccommodationReservation SelectedAccommodationReservationn, User guest1, AccommodationReservationService accommodationReservationController)
        {
            _accommodationAndOwnerGradingView = accommodationAndOwnerGradingView;

            User = guest1;

            _accommodationReservationController = accommodationReservationController;

            _accommodationGradeController = new AccommodationGradeController();
            AccommodationGrades = new ObservableCollection<AccommodationGrade>(_accommodationGradeController.GetAllAccommodationGrades());

            _accommodationController = new AccommodationService();

            _addedPictures = new List<string>();
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
            }
            _accommodationController.Update(SelectedAccommodationReservation.Accommodation);
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
                    _accommodationAndOwnerGradingView.PicturesListView.Items.Add(new BitmapImage(new Uri(filename)));
                   _addedPictures.Add(new Uri(filename).AbsoluteUri);
                }
                _accommodationAndOwnerGradingView.myListBox.ItemsSource = _addedPictures;
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
                _accommodationAndOwnerGradingView.PicturesListView.Items.Remove(selectedBitmapImage);
            }
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
            return null;
        }

    }
}
