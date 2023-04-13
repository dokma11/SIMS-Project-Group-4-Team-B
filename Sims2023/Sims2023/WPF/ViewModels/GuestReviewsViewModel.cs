using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels
{
    public partial class GuestReviewsViewModel : IObserver
    {
        public Tour SelectedTour { get; set; }
        public List<Tour> AllTours { get; set; }
        public ObservableCollection<Tour> ToursToDisplay { get; set; }

        public TourReview SelectedReview { get; set; }
        public List<TourReview> AllReviews { get; set; }
        public ObservableCollection<TourReview> ReviewsToDisplay { get; set; }
        private TourReviewService _tourReviewService;
        private TourService _tourService;
        public List<KeyPoint> AllKeyPoints;
        public User LoggedInGuide;
        private KeyPointService _keyPointService;
        public GuestReviewsViewModel(TourService tourService, TourReviewService tourReviewController, KeyPointService keyPointService, User loggedInGuide)
        {
            AllTours = tourService.GetAll();
            LoggedInGuide = loggedInGuide;
            _tourService = tourService;
            ToursToDisplay = new ObservableCollection<Tour>(_tourService.GetFinishedTours(LoggedInGuide));

            _tourReviewService = tourReviewController;
            AllReviews = _tourReviewService.GetAllTourReviews();
            ReviewsToDisplay = new ObservableCollection<TourReview>();

            _keyPointService = keyPointService;
            AllKeyPoints = keyPointService.GetAll();
        }

        public List<TourReview> DisplayReviews(Tour selectedTour)
        {
            GetToursReviews(selectedTour);
            return _tourReviewService.GetReviewsByToursId(selectedTour.Id);
        }

        public void GetToursReviews(Tour selectedTour)
        {
            _keyPointService.GetKeyPointWhereGuestJoined(selectedTour);
        }

        public void Update()
        {
            ReviewsToDisplay.Clear();
            DisplayReviews(SelectedTour);
            _tourReviewService.Save();
        }
    }
}
