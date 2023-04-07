﻿using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for GuestReviewsView.xaml
    /// </summary>
    public partial class GuestReviewsView : Window, IObserver
    {
        public Tour SelectedTour { get; set; }
        public List<Tour> AllTours { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }

        public TourReview SelectedReview { get; set; }
        public List<TourReview> AllReviews { get; set; }
        public ObservableCollection<TourReview> ReviewsToDisplay { get; set; }
        private TourReviewController _tourReviewController;

        public List<KeyPoint> AllKeyPoints;

        public GuestReviewsView(TourController tourController, TourReviewController tourReviewController, KeyPointController keyPointController)
        {
            InitializeComponent();
            DataContext = this;

            AllTours = tourController.GetAllTours();
            ToursToDisplay = new ObservableCollection<Tour>();

            _tourReviewController = tourReviewController;
            AllReviews = _tourReviewController.GetAllTourReviews();
            ReviewsToDisplay = new ObservableCollection<TourReview>();

            AllKeyPoints = keyPointController.GetAllKeyPoints();

            DisplayTours();
        }

        public void DisplayTours()
        {
            foreach (var tour in AllTours)
            {
                if (tour.CurrentState == Tour.State.Finished)
                {
                    ToursToDisplay.Add(tour);
                    GetToursReviews(tour);
                }
            }
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
            SuccessfulReportLabel.Visibility = Visibility.Visible;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SuccessfulReportLabel.Visibility = Visibility.Hidden;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        private void DisplayReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                Update();
            }
            else
            {
                MessageBox.Show("Molimo Vas odaberite turu za koju želite da vidite recenzije");
            }
        }

        private void DisplayReviews(Tour selectedTour)
        {
            foreach (var tourReview in AllReviews)
            {
                if (selectedTour.Id == tourReview.Tour.Id)
                {
                    ReviewsToDisplay.Add(tourReview);
                }
            }
        }

        private void GetToursReviews(Tour selectedTour)
        {
            foreach (var tourReview in AllReviews)
            {
                GetJoinedKeyPoints(selectedTour, tourReview);
            }
        }

        private void GetJoinedKeyPoints(Tour selectedTour, TourReview tourReview)
        {
            foreach (var keyPoint in AllKeyPoints)
            {
                if (keyPoint.Tour.Id == selectedTour.Id && keyPoint.ShowedGuestsIds.Contains(tourReview.Guest.Id))
                {
                    tourReview.KeyPointJoined = keyPoint;
                }
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
            DisplayReviews(SelectedTour);
            _tourReviewController.Save();
        }
    }
}