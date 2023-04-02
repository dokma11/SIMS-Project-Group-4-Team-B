using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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

        private TourController _tourController;
        public TourReview SelectedReview { get; set; }
        public List<TourReview> AllReviews { get; set; }
        public ObservableCollection<TourReview> ReviewsToDisplay { get; set; }

        private TourReviewController _tourReviewController;

        private KeyPointController _keyPointController;

        public GuestReviewsView(TourController tourController, TourReviewController tourReviewController, KeyPointController keyPointController)
        {
            InitializeComponent();
            DataContext = this;

            _tourController = tourController;
            AllTours = _tourController.GetAllTours();
            ToursToDisplay = new ObservableCollection<Tour>();

            _tourReviewController = tourReviewController;
            AllReviews = _tourReviewController.GetAllTourReviews();
            ReviewsToDisplay = new ObservableCollection<TourReview>();

            _keyPointController = keyPointController;

            DisplayTours();

        }

        public void DisplayTours()
        {
            foreach (var tour in AllTours)
            {
                if (tour.CurrentState == Tour.State.Finished)
                {
                    ToursToDisplay.Add(tour);
                    GetJoinedKeyPoints(tour);
                }
            }
        }

        private void ReportReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReview != null)
            {
                SelectedReview.IsValid = false;
                Update();
            }
            else
            {
                MessageBox.Show("Molimo Vas odaberite recenziju koju zelite da prijavite");
            }
        }

        private void DisplayReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                Update();
            }
            else
            {
                MessageBox.Show("Molimo Vas odaberite turu ");
            }
        }

        private void DisplayReviews(Tour selectedTour)
        {
            foreach (var review in AllReviews)
            {
                if (selectedTour.Id == review.Tour.Id)
                {
                    ReviewsToDisplay.Add(review);
                }
            }
        }

        private void GetJoinedKeyPoints(Tour selectedTour)
        {
            foreach(var review in AllReviews)
            {
                foreach(var keyPoint in _keyPointController.GetAllKeyPoints())
                {
                    if(keyPoint.ToursId == selectedTour.Id)
                    {
                        if (keyPoint.ShowedGuestsIds.Contains(review.Guest.Id))
                        {
                            review.KeyPointJoined = keyPoint;
                        }
                    }
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
                MessageBox.Show("Molimo Vas odaberite recenziju ");
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
