using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for CreateTourFromFrequentLanguage.xaml
    /// </summary>
    public partial class CreateTourFromFrequentLanguageView : Page
    {
        public CreateTourFromFrequentLanguageViewModel CreateTourFromFrequentLanguageViewModel;
        public CreateTourFromFrequentLanguageView(RequestsLanguage selectedLanguage, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService)
        {
            InitializeComponent();

            CreateTourFromFrequentLanguageViewModel = new(selectedLanguage, tourService, locationService, keyPointService, tourReviewService, requestService, tourReservationService, voucherService, userService, countriesAndCitiesService, loggedInGuide, tourNotificationService);
            DataContext = CreateTourFromFrequentLanguageViewModel;

            countryComboBox.ItemsSource = CreateTourFromFrequentLanguageViewModel.GetCitiesAndCountries();
            countryComboBox.DisplayMemberPath = "CountryName";
            countryComboBox.SelectedValuePath = "CountryName";

            foreach (var date in tourService.GetBusyDates(loggedInGuide))
            {
                requestDatePicker.BlackoutDates.Add(new CalendarDateRange(date, date.AddHours(1)));
            }

            requestDatePicker.DisplayDateStart = DateTime.Today.AddDays(1);

            TextBox[] textBoxes = { toursNameTextBox, keyPointTextBox, picturesTextBox, descriptionTextBox };

            foreach (TextBox textBox in textBoxes)
            {
                textBox.GotFocus += TextBox_GotFocus;
                textBox.LostFocus += TextBox_LostFocus;
                textBox.Text = textBox.Tag.ToString();
            }
        }

        private void IntegerUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string placeholderText = textBox.Tag.ToString();
            if (textBox.Text == placeholderText)
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string placeholderText = textBox.Tag.ToString();
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholderText;
            }
        }

        private void RequestDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateTourFromFrequentLanguageViewModel.NewTour.Start = (DateTime)requestDatePicker.SelectedDate;
            CreateTourFromFrequentLanguageViewModel.AddDatesToList(requestDatePicker.Text);
        }

        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the list of cities for the selected country
            var selectedCountry = (CountriesAndCities)countryComboBox.SelectedItem;
            var cities = new List<string> { selectedCountry.City1, selectedCountry.City2, selectedCountry.City3, selectedCountry.City4, selectedCountry.City5 };

            // Bind the city ComboBox to the list of cities
            cityComboBox.ItemsSource = cities;
        }
        /*
                private void ConfirmButton_Click(object sender, RoutedEventArgs e)
                {
                    if (keyPointsOutput.Items.Count > 1 && toursNameTextBox.Text != "" && toursNameTextBox.Text != "Unesite naziv"
                        && picturesTextBox.Text != "" && picturesTextBox.Text != "Unesite putanje slika"
                        && descriptionTextBox.Text != "" && descriptionTextBox.Text != "Unesite opis ture"
                        && maximumNumberOfGuests.Text != "" && duration.Text != "")
                    {
                        CreateTourFromFrequentLanguageViewModel.ConfirmCreation(countryComboBox.Text, cityComboBox.Text);
                        RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
                        FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
                    }
                    else
                    {
                        ValidationErrorLabelEvent();
                    }
                }

                public void ValidationErrorLabelEvent()
                {
                    validationLabel.Visibility = Visibility.Visible;
                    DispatcherTimer timer = new()
                    {
                        Interval = TimeSpan.FromSeconds(5)
                    };
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }

                private void Timer_Tick(object sender, EventArgs e)
                {
                    validationLabel.Visibility = Visibility.Hidden;
                    DispatcherTimer timer = (DispatcherTimer)sender;
                    timer.Stop();
                }
        */
        private void KeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addKeyPointsButton.IsEnabled = !string.IsNullOrEmpty(keyPointTextBox.Text);
            if (keyPointTextBox.Text == "Unesite ključne tačke")
            {
                addKeyPointsButton.IsEnabled = false;
            }
        }
    }
}
