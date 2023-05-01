using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class GuestReviewsView : Page
    {
        public GuestReviewsViewModel GuestReviewsViewModel;
        private RequestService _requestService;
        private TourService _tourService;
        private LocationService _locationService;
        private TourReviewService _tourReviewService;
        private KeyPointService _keyPointService;
        public User LoggedInGuide { get; set; }
        public GuestReviewsView(TourService tourService, TourReviewService tourReviewService, LocationService locationService, RequestService requestService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();

            _requestService = requestService;
            _tourService = tourService;
            _tourReviewService = tourReviewService;
            _locationService = locationService;
            _keyPointService = keyPointService;

            LoggedInGuide = loggedInGuide;

            GuestReviewsViewModel = new(tourService, tourReviewService, loggedInGuide);
            DataContext = GuestReviewsViewModel;
        }

        private void DisplayReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestReviewsViewModel.IsTourSelected())
            {
                GuestReviewsViewModel.UpdateReviewsList();
            }
            else
            {
                MessageBox.Show("Odaberite turu za koju želite da vidite recenzije");
            }
        }

        private void ReportReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestReviewsViewModel.IsReviewSelected() && GuestReviewsViewModel.IsReviewValid())
            {
                GuestReviewsViewModel.ReportReview();
                SuccessfulReportLabelEvent();
            }
            else
            {
                MessageBox.Show("Odaberite recenziju koju želite da prijavite");
            }
        }

        private void SuccessfulReportLabelEvent()
        {
            successfulReportLabel.Visibility = Visibility.Visible;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            successfulReportLabel.Visibility = Visibility.Hidden;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        private void DisplayCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestReviewsViewModel.IsReviewSelected())
            {
                MessageBox.Show(GuestReviewsViewModel.SelectedReview.Comment);
            }
            else
            {
                MessageBox.Show("Odaberite recenziju čiji komentar želite da vidite");
            }
        }

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
    }
}
