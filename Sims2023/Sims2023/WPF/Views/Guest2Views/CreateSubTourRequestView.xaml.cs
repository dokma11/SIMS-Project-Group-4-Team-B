﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for CreateSubTourRequestView.xaml
    /// </summary>
    public partial class CreateSubTourRequestView : Window
    {
        public User User { get; set; }
        public CreateSubTourRequestViewModel CreateSubTourRequestViewModel { get; set; }
        public CreateSubTourRequestView(User user,ComplexTourRequest complexTourRequest)
        {
            InitializeComponent();
            CreateSubTourRequestViewModel = new CreateSubTourRequestViewModel(complexTourRequest,user);
            User = user;



            countryComboBox.ItemsSource = CreateSubTourRequestViewModel.GetCitiesAndCountries();
            countryComboBox.DisplayMemberPath = "CountryName";
            countryComboBox.SelectedValuePath = "CountryName";

            startDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(2)));
            endDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(2)));
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            endDatePicker.DisplayDateStart = startDatePicker.SelectedDate;
        }

        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the list of cities for the selected country
            var selectedCountry = (CountriesAndCities)countryComboBox.SelectedItem;
            var cities = new List<string> { selectedCountry.City1, selectedCountry.City2, selectedCountry.City3, selectedCountry.City4, selectedCountry.City5 };

            // Bind the city ComboBox to the list of cities
            cityComboBox.ItemsSource = cities;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFilled() && IsStartBeforeEnd())
            {
                CreateSubTourRequestViewModel.ConfirmCreation(countryComboBox.Text, cityComboBox.Text, DateTime.Parse(startDatePicker.Text), DateTime.Parse(endDatePicker.Text), descriptionTextBox.Text, User, (RequestsLanguage)Enum.Parse(typeof(RequestsLanguage), languageComboBox.Text), int.Parse(guestNumberBox.Text));
                MessageBox.Show("Uspesno kreiranje");
                this.Close();
            }
            else
            {
                MessageBox.Show("Molim Vas popunite sva polja");
            }
        }

        private bool IsFilled()
        {
            return !string.IsNullOrWhiteSpace(countryComboBox.Text) &&
                   !string.IsNullOrWhiteSpace(cityComboBox.Text) &&
                   !string.IsNullOrWhiteSpace(languageComboBox.Text) &&
                    startDatePicker.SelectedDate.HasValue &&
                    endDatePicker.SelectedDate.HasValue &&
                   !string.IsNullOrWhiteSpace(descriptionTextBox.Text);
        }

        private bool IsStartBeforeEnd()
        {
            int result = DateTime.Compare(DateTime.Parse(startDatePicker.Text), DateTime.Parse(endDatePicker.Text));
            if (result <= 0)
                return true;
            else
            {
                MessageBox.Show("Startni datum mora biti manji od krajnjeg datuma");
                return false;
            }
        }

    }
}
