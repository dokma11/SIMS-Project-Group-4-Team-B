using Microsoft.Win32;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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


namespace Sims2023.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerGradingView.xaml
    /// </summary>
    public partial class AccommodationAndOwnerGradingView : Window
    {
        private List<string> _addedPictures = new List<string>();
        public AccommodationReservation SelectedAccommodationReservation { get; set; }

        private AccommodationGradeController _accommodationGradeController;
        public ObservableCollection<AccommodationGrade> AccommodationGrades { get; set; }

        private AccommodationReservationController _accommodationReservationController;

        private AccommodationController _accommodationController;

        public User User { get; set; }
        public AccommodationAndOwnerGradingView(AccommodationReservation SelectedAccommodationReservationn, User guest1, AccommodationReservationController accommodationReservationController)
        {
            InitializeComponent();
            DataContext = this;

            User = guest1;

            _accommodationReservationController = accommodationReservationController;

            _accommodationGradeController = new AccommodationGradeController();
            AccommodationGrades = new ObservableCollection<AccommodationGrade>(_accommodationGradeController.GetAllAccommodationGrades());

            _accommodationController = new AccommodationController();

            _addedPictures = new List<string>();
            SelectedAccommodationReservation = SelectedAccommodationReservationn;
           // PicturesListView.ItemsSource = _addedPictures;

        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da ostavite ovu recenziju?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                AddCreatedGrade(SelectedAccommodationReservation);
                Close();
            }
            else
            {
                return;
            }
        }

        private void AddCreatedGrade(AccommodationReservation selectedAccommodationReservation)
        {
            AccommodationGrade accommodationGrade = CreateGrade(selectedAccommodationReservation);
            if(accommodationGrade != null)
            {
                _accommodationGradeController.Create(accommodationGrade);
                UpdateAccommodationReservation(selectedAccommodationReservation);
                UpdateDefaultStyleAccommodationImages(SelectedAccommodationReservation, _addedPictures);
                MessageBox.Show("Uspesno ste ocenili ovaj smestaj.");
            }

        }

        private void UpdateDefaultStyleAccommodationImages(AccommodationReservation selectedAccommodationReservation, List<string> addedPictures)
        {
            foreach(var image in addedPictures)
            {
                SelectedAccommodationReservation.Accommodation.Imageurls.Add(image);
            }
            _accommodationController.Update(SelectedAccommodationReservation.Accommodation);
        }

        private void UpdateAccommodationReservation(AccommodationReservation selectedAccommodationReservation)
        {

            SelectedAccommodationReservation.Graded = true;
            _accommodationReservationController.Update(SelectedAccommodationReservation);
        }

        private AccommodationGrade CreateGrade(AccommodationReservation selectedAccommodationReservation)
        {
            AccommodationGrade accommodationGrade = new AccommodationGrade();
            accommodationGrade.Guest = User;
            accommodationGrade.Accommodation = selectedAccommodationReservation.Accommodation;
            accommodationGrade.ValueForMoney = (int)valueForMoneyIntegerUpDown.Value;
            accommodationGrade.Cleanliness=(int)cleannessIntegerUpDown.Value;
            accommodationGrade.Owner=(int)ownerIntegerUpDown.Value;
            accommodationGrade.Comfort=(int)comfortIntegerUpDown.Value;
            accommodationGrade.Location=(int)locationIntegerUpDown.Value;
            accommodationGrade.Comment = textBox.Text;
            return accommodationGrade;

        }

        private void giveUp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    PicturesListView.Items.Add(new BitmapImage(new Uri(filename)));
                   _addedPictures.Add(new Uri(filename).AbsoluteUri);
                }
                myListBox.ItemsSource = _addedPictures;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string itemToRemove = (string)button.Tag;
            RemovePictureFromListBox(itemToRemove);
            RemovePictureFromListView(itemToRemove);
        }
        private void RemovePictureFromListBox(string itemToRemove)
        {
            _addedPictures.Remove(itemToRemove);
            myListBox.ItemsSource = null;
            myListBox.ItemsSource = _addedPictures;
        }
        private void RemovePictureFromListView(string itemToRemove)
        {
            BitmapImage selectedBitmapImage = FindDeletingPicture(itemToRemove);
            
            if (selectedBitmapImage != null)
            {
                PicturesListView.Items.Remove(selectedBitmapImage);
            }
        }
        private BitmapImage FindDeletingPicture(string itemToRemove)
        {
            foreach (BitmapImage bitmapImage in PicturesListView.Items)
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
