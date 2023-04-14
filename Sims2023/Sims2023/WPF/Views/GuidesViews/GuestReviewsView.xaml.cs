using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Sims2023.WPF.Views.GuidesViews
{
    public partial class GuestReviewsView : Window
    {
        public GuestReviewsViewModel GuestReviewsViewModel;
        public GuestReviewsView(TourService tourService, TourReviewService tourReviewService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();

            GuestReviewsViewModel = new(tourService, tourReviewService, keyPointService, loggedInGuide);
            DataContext = GuestReviewsViewModel;
        }

        private void ReportReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestReviewsViewModel.SelectedReview != null && GuestReviewsViewModel.SelectedReview.IsValid)
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

        private void DisplayReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestReviewsViewModel.SelectedTour != null)
            {
                GuestReviewsViewModel.DisplayReviews();
            }
            else
            {
                MessageBox.Show("Odaberite turu za koju želite da vidite recenzije");
            }
        }

        private void DisplayCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (GuestReviewsViewModel.SelectedReview != null)
            {
                MessageBox.Show(GuestReviewsViewModel.SelectedReview.Comment);
            }
            else
            {
                MessageBox.Show("Odaberite recenziju čiji komentar želite da vidite");
            }
        }
    }
}
