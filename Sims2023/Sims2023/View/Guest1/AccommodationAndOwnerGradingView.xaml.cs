using Microsoft.Win32;
using Sims2023.Model;
using System;
using System.Collections.Generic;
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
        public AccommodationAndOwnerGradingView(AccommodationReservation SelectedAccommodationReservationn)
        {
            InitializeComponent();
            DataContext = this;

            List<string> _addedPictures = new List<string>();
            AccommodationReservation SelectedAccommodationReservation = SelectedAccommodationReservationn;
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void giveUp_Click(object sender, RoutedEventArgs e)
        {

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
