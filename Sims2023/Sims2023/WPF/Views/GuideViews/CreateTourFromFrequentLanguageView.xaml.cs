using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for CreateTourFromFrequentLanguage.xaml
    /// </summary>
    public partial class CreateTourFromFrequentLanguageView : Window
    {
        public CreateTourFromFrequentLanguageViewModel CreateTourFromFrequentLanguageViewModel;
        private bool addDatesButtonClicked;
        public CreateTourFromFrequentLanguageView(RequestsLanguage selectedLanguage, TourService tourService, LocationService locationService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();

            CreateTourFromFrequentLanguageViewModel = new(selectedLanguage, tourService, locationService, keyPointService, loggedInGuide);
            DataContext = CreateTourFromFrequentLanguageViewModel;

            addDatesButtonClicked = false;
            countryComboBox.ItemsSource = CreateTourFromFrequentLanguageViewModel.GetCitiesAndCountries();
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
                CreateTourFromFrequentLanguageViewModel.ConfirmCreation(countryComboBox.Text, cityComboBox.Text);
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

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);
            CreateTourFromFrequentLanguageViewModel.AddKeyPointsToList(inputText);
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
            CreateTourFromFrequentLanguageViewModel.AddDatesToList(dateTimeTextBox.Text);
        }
    }
}
