using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace Sims2023.WPF.Views.GuidesViews
{
    public partial class GuestReviewsView : Window
    {
        public Tour SelectedTour { get; set; }
        public List<Tour> AllTours { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }

        public TourReview SelectedReview { get; set; }
        public List<TourReview> AllReviews { get; set; }
        public ObservableCollection<TourReview> ReviewsToDisplay { get; set; }
        private TourReviewService _tourReviewController;
        public List<KeyPoint> AllKeyPoints;
        public GuestReviewsViewModel GuestReviewsViewModel;
        public GuestReviewsView(TourService tourService, TourReviewService tourReviewService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();
            DataContext = this;

            AllTours = tourService.GetAll();
            GuestReviewsViewModel = new(tourService, tourReviewService, keyPointService, loggedInGuide);
            ToursToDisplay = new ObservableCollection<Tour>(GuestReviewsViewModel.ToursToDisplay);

            _tourReviewController = tourReviewService;
            AllReviews = _tourReviewController.GetAllTourReviews();
            ReviewsToDisplay = new ObservableCollection<TourReview>();

            AllKeyPoints = keyPointService.GetAll();
        }

        private void ReportReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReview != null && SelectedReview.IsValid)
            {
                SelectedReview.IsValid = false;
                Update();
                SuccessfulReportLabelEvent();
            }
            else
            {
                MessageBox.Show("Molimo Vas odaberite recenziju koju želite da prijavite");
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

        private void DisplayReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                ReviewsToDisplay.Clear();
                foreach (var tourReview in GuestReviewsViewModel.DisplayReviews(SelectedTour))
                {
                    ReviewsToDisplay.Add(tourReview);
                }
            }
            else
            {
                MessageBox.Show("Molimo Vas odaberite turu za koju želite da vidite recenzije");
            }
        }

        private void DisplayCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReview != null)
            {
                MessageBox.Show(SelectedReview.Comment);
            }
            else
            {
                MessageBox.Show("Molimo Vas odaberite recenziju čiji komentar želite da vidite");
            }
        }

        public void Update()
        {
            ReviewsToDisplay.Clear();
            foreach (var tourReview in GuestReviewsViewModel.DisplayReviews(SelectedTour))
            {
                ReviewsToDisplay.Add(tourReview);
            };
        }
    }
}
