using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class CreateTourView
    {
        public CreateTourViewModel CreateTourViewModel;
        private bool addDateButtonClicked;
        public CreateTourView(TourService tourService, LocationService locationService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();

            addDateButton.IsEnabled = false;
            addKeyPointsButton.IsEnabled = false;
            addDateButtonClicked = false;

            CreateTourViewModel = new(tourService, locationService, keyPointService, loggedInGuide);
            DataContext = CreateTourViewModel;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (addDateButtonClicked && keyPointsOutput.Items.Count > 1)
            {
                CreateTourViewModel.ConfirmCreation();
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
            CreateTourViewModel.SetToursLanguage(language);
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);
            CreateTourViewModel.AddKeyPoints(inputText);
            keyPointTextBox.Clear();
        }

        private void KeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addKeyPointsButton.IsEnabled = !string.IsNullOrEmpty(keyPointTextBox.Text);
        }

        private void DateTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addDateButton.IsEnabled = !string.IsNullOrEmpty(dateTimeTextBox.Text);
        }

        private void AddDateButton_Click(object sender, RoutedEventArgs e)
        {
            addDateButtonClicked = true;
            CreateTourViewModel.AddDates(dateTimeTextBox.Text);
        }
    }
}
