using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class CreateTourView
    {
        public CreateTourViewModel CreateTourViewModel;
        private bool addDatesButtonClicked;
        public CreateTourView(TourService tourService, LocationService locationService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();

            addDatesButton.IsEnabled = false;
            addKeyPointsButton.IsEnabled = false;
            addDatesButtonClicked = false;

            CreateTourViewModel = new(tourService, locationService, keyPointService, loggedInGuide);
            DataContext = CreateTourViewModel;

            countryComboBox.ItemsSource = CreateTourViewModel.GetCitiesAndCountries();
            countryComboBox.DisplayMemberPath = "CountryName";
            countryComboBox.SelectedValuePath = "CountryName";
        }

        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the list of cities for the selected country
            var selectedCountry = (CountriesAndCities)countryComboBox.SelectedItem;
            var cities = new List<string> { selectedCountry.City1, selectedCountry.City2, selectedCountry.City3, selectedCountry.City4, selectedCountry.City5 };

            // Bind the city ComboBox to the list of cities
            cityComboBox.ItemsSource = cities;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (addDatesButtonClicked && keyPointsOutput.Items.Count > 1)
            {
                CreateTourViewModel.ConfirmCreation(countryComboBox.Text, cityComboBox.Text);
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
            CreateTourViewModel.AddKeyPointsToList(inputText);
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
            CreateTourViewModel.AddDatesToList(dateTimeTextBox.Text);
        }
    }
}
