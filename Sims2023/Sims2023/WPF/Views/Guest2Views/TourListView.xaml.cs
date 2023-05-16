using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourListView.xaml
    /// </summary>
    public partial class TourListView : Page
    {
        public User User { get; set; }
        public TourListViewModel TourListViewModel { get; set; }
        public Tour SelectedTour { get; set; }
        public TourListView(User user)
        {
            InitializeComponent();
            User = user;
            TourListViewModel = new TourListViewModel(this,user);
            DataContext=TourListViewModel;
            dataGridTours.ItemsSource = TourListViewModel.Tours;
            dataGridTours.SelectedItem = TourListViewModel.SelectedTour;

            countrySearchBox.ItemsSource = TourListViewModel.GetCitiesAndCountries();
            countrySearchBox.DisplayMemberPath = "CountryName";
            countrySearchBox.SelectedValuePath = "CountryName";

        }

        private void SearchTours_Click(object sender, RoutedEventArgs e)
        {
           TourListViewModel.SearchTours_Click();
        }

        private void SeeMore_Click(object sender, RoutedEventArgs e)
        {
            TourListViewModel.SeeDetails_Click();
        }

        public void CountrySearchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the list of cities for the selected country
            var selectedCountry = (CountriesAndCities)countrySearchBox.SelectedItem;
            var cities = new List<string> { selectedCountry.City1, selectedCountry.City2, selectedCountry.City3, selectedCountry.City4, selectedCountry.City5 };

            // Bind the city ComboBox to the list of cities
            citySearchBox.ItemsSource = cities;
        }
    }
}
