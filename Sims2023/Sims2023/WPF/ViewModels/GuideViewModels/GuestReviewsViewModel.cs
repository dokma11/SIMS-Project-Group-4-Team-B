﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class GuestReviewsViewModel
    {
        public Tour SelectedTour { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }
        public TourReview SelectedReview { get; set; }
        public ObservableCollection<TourReview> ReviewsToDisplay { get; set; }
        private TourService _tourService;
        private TourReviewService _tourReviewService;
        public User LoggedInGuide;
        public GuestReviewsViewModel(TourService tourService, TourReviewService tourReviewService, User loggedInGuide)
        {
            LoggedInGuide = loggedInGuide;

            _tourReviewService = tourReviewService;
            _tourService = tourService;

            ToursToDisplay = new ObservableCollection<Tour>(_tourService.GetFinishedTours(loggedInGuide));
            ReviewsToDisplay = new ObservableCollection<TourReview>();

            GetAttendedGuestsNumber();
        }

        public void GetAttendedGuestsNumber()
        {
            _tourService.GetAttendedGuestsNumber(LoggedInGuide);
        }

        public void DisplayReviews()
        {
            ReviewsToDisplay.Clear();
            foreach (var tourReview in _tourReviewService.GetByToursId(SelectedTour.Id))
            {
                ReviewsToDisplay.Add(tourReview);
            }
            _tourReviewService.Save();
        }

        public void ReportReview()
        {
            _tourReviewService.Report(SelectedReview);
            DisplayReviews();
        }

        public bool IsReviewSelected()
        {
            return SelectedReview != null;
        }

        public bool IsTourSelected()
        {
            return SelectedTour != null;
        }

        public bool IsReviewValid()
        {
            return SelectedReview.IsValid;
        }
    }
}
