using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for CreateTourFromFrequentLocation.xaml
    /// </summary>
    public partial class CreateTourFromFrequentLocationView : Window
    {
        public CreateTourFromFrequentLocationViewModel CreateTourFromFrequentLocationViewModel;
        private bool addDatesButtonClicked;
        public CreateTourFromFrequentLocationView(Location selectedLocation, TourService tourService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();

            CreateTourFromFrequentLocationViewModel = new(selectedLocation, tourService, keyPointService, loggedInGuide);
            DataContext = CreateTourFromFrequentLocationViewModel;

            addDatesButtonClicked = false;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (addDatesButtonClicked && keyPointsOutput.Items.Count > 1)
            {
                CreateTourFromFrequentLocationViewModel.ConfirmCreation();
                Close();
            }
            else
            {
                MessageBox.Show("Popunite sva polja molim Vas");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string language = ((ComboBoxItem)cBox.SelectedItem).Content.ToString();
            CreateTourFromFrequentLocationViewModel.SetToursLanguage(language);
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);
            CreateTourFromFrequentLocationViewModel.AddKeyPointsToList(inputText);
            keyPointTextBox.Clear();
        }

        private void KeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addKeyPointsButton.IsEnabled = !string.IsNullOrEmpty(keyPointTextBox.Text);
        }

        private void DateTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addDatesButton.IsEnabled = !string.IsNullOrEmpty(dateTimeTextBox.Text);
        }

        private void AddDatesButton_Click(object sender, RoutedEventArgs e)
        {
            addDatesButtonClicked = true;
            CreateTourFromFrequentLocationViewModel.AddDatesToList(dateTimeTextBox.Text);
        }
    }
}
