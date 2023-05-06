using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for ToursView.xaml
    /// </summary>
    public partial class ToursView : Page
    {
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private VoucherService _voucherService;
        private RequestService _requestService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        public ToursViewModel ToursViewModel;
        public User LoggedInGuide { get; set; }
        private bool addDatesButtonClicked;

        public ToursView(TourService tourService, TourReviewService tourReviewService, TourReservationService tourReservationService, KeyPointService keyPointService, LocationService locationService, VoucherService voucherService, UserService userService, User loggedInGuide, CountriesAndCitiesService countriesAndCitiesService, RequestService requestService)
        {
            InitializeComponent();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReservationService = tourReservationService;
            _tourReviewService = tourReviewService;
            _voucherService = voucherService;
            _userService = userService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _requestService = requestService;

            LoggedInGuide = loggedInGuide;

            ToursViewModel = new(_tourService, _voucherService, _tourReservationService, _userService, LoggedInGuide, _countriesAndCitiesService, _locationService, _keyPointService);
            DataContext = ToursViewModel;

            countryComboBox.ItemsSource = ToursViewModel.GetCitiesAndCountries();
            countryComboBox.DisplayMemberPath = "CountryName";
            countryComboBox.SelectedValuePath = "CountryName";

            addDatesButtonClicked = false;
        }

        private void TabControl_SelectionChanged(object sender, RoutedEventArgs e)
        {

            if (tabControl.SelectedIndex == 1)
            {
                /*
                toursNameTextBox.Text = string.Empty;
                maximumNumberOfGuests.Value = 1;
                duration.Value = 1;
                keyPointTextBox.Text = string.Empty;
                keyPointsOutput.Items.Clear();
                descriptionTextBox.Text = string.Empty;
                if(ToursViewModel.DateTimeList.Count > 0)
                {
                    ToursViewModel.DateTimeList.Clear();
                }
                if(ToursViewModel.KeyPointsList.Count > 0)
                {
                    ToursViewModel.KeyPointsList.Clear();
                }
                */
            }

        }

        //PREDSTOJECE TURE

        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToursViewModel.IsCreatedTourSelected())
            {
                startTourButton.IsEnabled = false;
                LiveTourTrackingView liveTourTrackingView = new(ToursViewModel.SelectedCreatedTour, _keyPointService, _tourReservationService, _userService, _tourService, _tourReviewService, _requestService, LoggedInGuide, _locationService, _voucherService, _countriesAndCitiesService);
                FrameManagerGuide.Instance.MainFrame.Navigate(liveTourTrackingView);
                ToursViewModel.Update();
            }
            else
            {
                MessageBox.Show("Odaberite turu koju zelite da zapocnete");
            }
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToursViewModel.IsCreatedTourSelected() && ToursViewModel.IsTourEligibleForCancellation())
            {
                string additionalComment = Microsoft.VisualBasic.Interaction.InputBox("Unesite razlog:", "Input String");
                if (!string.IsNullOrEmpty(additionalComment))
                {
                    ToursViewModel.CancelTour(additionalComment);
                    SuccessfulCancellationLabelEvent();
                }
            }
        }

        private void SuccessfulCancellationLabelEvent()
        {
            successfulCancellationLabel.Visibility = Visibility.Visible;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            successfulCancellationLabel.Visibility = Visibility.Hidden;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        //KREIRANJE TURE

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
                ToursViewModel.ConfirmCreation(countryComboBox.Text, cityComboBox.Text);
                ToursViewModel.Update();
                tabControl.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Popunite sva polja molim Vas");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
            //ovde je mozda problem sto kada klikne na odustani njemu se sacuva ono sto je pisao
        }

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string language = ((ComboBoxItem)cBox.SelectedItem).Content.ToString();
            ToursViewModel.SetToursLanguage(language);
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);
            ToursViewModel.AddKeyPointsToList(inputText);
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
            ToursViewModel.AddDatesToList(dateTimeTextBox.Text);
        }

        //STATISTIKA TURE

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }

        private void DisplayStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToursViewModel.IsFinishedTourSelected())
            {
                ChangeDisplay();
                ToursViewModel.DisplayStatistics();
            }
        }

        private void ChangeDisplay()
        {
            theMostVisitedTourLabel.Content = "Statistika izabrane ture";
            yearComboBox.Visibility = Visibility.Hidden;
            theMostVisitedTourDataGrid.Visibility = Visibility.Hidden;
            theMostVisitedTourStatisticsLabel.Visibility = Visibility.Hidden;
            displayTheMostVisitedTourButton.Visibility = Visibility.Visible;
            displayTheMostVisitedTourButton.Margin = new Thickness(270, -100, 0, 0);
            cartesianChart.Height = 400;
            cartesianChart.Margin = new Thickness(0, -60, 0, 100);
            cartesianChartLabel.Margin = new Thickness(40, -100, 0, 0);
            pieChart.Height = 400;
            pieChart.Margin = new Thickness(0, -60, 80, 100);
            pieChartLabel.Margin = new Thickness(40, -100, 0, 0);
        }

        private void YearComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string year = (cBox.SelectedItem.ToString());
            ToursViewModel.UpdateTheMostVisitedTour(LoggedInGuide, year);
        }

        private void DisplayTheMostVisitedTourButton(object sender, RoutedEventArgs e)
        {
            //vrati nazad sve
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
            toursView.tabControl.SelectedIndex = 2;
        }
    }
}
