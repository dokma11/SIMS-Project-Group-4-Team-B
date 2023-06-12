using System;
using System.Collections.Generic;
using System.Globalization;
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
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    MessageBox.Show("Uspešno kreiranje");
                }
                else
                {
                    MessageBox.Show("Creating successfully");
                }
                this.Close();
            }
            
        }

        private bool IsFilled()
        {
            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {

                if (string.IsNullOrWhiteSpace(countryComboBox.Text))
                {
                    MessageBox.Show("Molimo izaberite državu");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(cityComboBox.Text))
                {
                    MessageBox.Show("Molimo izaberite grad");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(languageComboBox.Text))
                {
                    MessageBox.Show("Molimo izaberite jezik");
                    return false;
                }
                if (!startDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Molimo izaberite početni datum");
                    return false;
                }
                if (!endDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Molimo izaberite krajnji datum");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(descriptionTextBox.Text))
                {
                    MessageBox.Show("Molimo upišite opis zahteva za turu");
                    return false;
                }
                return true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(countryComboBox.Text))
                {
                    MessageBox.Show("Choose country please");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(cityComboBox.Text))
                {
                    MessageBox.Show("Choose city please");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(languageComboBox.Text))
                {
                    MessageBox.Show("Choose language please");
                    return false;
                }
                if (!startDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Choose start date please");
                    return false;
                }
                if (!endDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Choose end date please");
                    return false;
                }
                if (string.IsNullOrWhiteSpace(descriptionTextBox.Text))
                {
                    MessageBox.Show("Write desription of tour request please");
                    return false;
                }
                return true;
            }
        }

        private bool IsStartBeforeEnd()
        {
            int result = DateTime.Compare(DateTime.Parse(startDatePicker.Text), DateTime.Parse(endDatePicker.Text));
            if (result <= 0)
                return true;
            else
            {
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    MessageBox.Show("Startni datum mora biti pre krajnjeg datuma");
                }
                else
                {
                    MessageBox.Show("Start date must be before end date");
                }
                return false;
            }
        }

    }
}
