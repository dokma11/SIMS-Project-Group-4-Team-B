using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for CreateTourFromRequestView.xaml
    /// </summary>
    public partial class CreateTourFromRequestView : Page
    {
        public CreateTourFromRequestViewModel CreateTourFromRequestViewModel;
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private VoucherService _voucherService;
        private RequestService _requestService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private TourNotificationService _tourNotificationService;
        public User LoggedInGuide { get; set; }
        public CreateTourFromRequestView(TourRequest selectedTourRequest, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService)
        {
            InitializeComponent();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;
            _tourReservationService = tourReservationService;
            _voucherService = voucherService;
            _userService = userService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _tourNotificationService = tourNotificationService;

            LoggedInGuide = loggedInGuide;

            CreateTourFromRequestViewModel = new(selectedTourRequest, loggedInGuide, tourService, keyPointService, requestService, tourNotificationService, tourReservationService);
            DataContext = CreateTourFromRequestViewModel;

            foreach (var date in _tourService.GetBusyDates(LoggedInGuide))
            {
                requestDatePicker.BlackoutDates.Add(new CalendarDateRange(date, date.AddHours(1)));
            }

            requestDatePicker.DisplayDateStart = CreateTourFromRequestViewModel.SelectedRequest.Start;

            requestDatePicker.BlackoutDates.Add(new CalendarDateRange(CreateTourFromRequestViewModel.SelectedRequest.End.AddDays(1), DateTime.MaxValue));

            TextBox[] textBoxes = { toursNameTextBox, keyPointTextBox, picturesTextBox};

            foreach (TextBox textBox in textBoxes)
            {
                textBox.GotFocus += TextBox_GotFocus;
                textBox.LostFocus += TextBox_LostFocus;
                textBox.Text = textBox.Tag.ToString();
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
            CreateTourFromRequestViewModel.Tour.Start = (DateTime)requestDatePicker.SelectedDate;
            CreateTourFromRequestViewModel.AddDatesToList(requestDatePicker.Text);
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);
            CreateTourFromRequestViewModel.AddKeyPointsToList(inputText);
            keyPointTextBox.Clear();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (keyPointsOutput.Items.Count > 1 && toursNameTextBox.Text != "" && toursNameTextBox.Text != "Unesite naziv"
                && keyPointTextBox.Text != "" && keyPointTextBox.Text != "Unesite ključne tačke" && picturesTextBox.Text != "" && picturesTextBox.Text != "Unesite putanje slika")
            {
                CreateTourFromRequestViewModel.ConfirmCreation();
                RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
                FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
            }
            else
            {
                MessageBox.Show("Popunite sva polja molim Vas");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromRequestViewModel.CancelCreation();
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void KeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addKeyPointsButton.IsEnabled = !string.IsNullOrEmpty(keyPointTextBox.Text);
        }

        //TOOLBAR

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
    }
}
