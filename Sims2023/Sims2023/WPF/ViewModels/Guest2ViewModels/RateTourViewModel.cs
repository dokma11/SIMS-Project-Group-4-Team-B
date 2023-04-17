using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest2Views;
using System.Collections.Generic;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class RateTourViewModel
    {
        public TourReviewService _tourReviewService;
        public KeyPointService _keyPointService;
        public User User { get; set; }
        public Tour Tour { get; set; }
        public RateTourView RateTourView { get; set; }
        public List<string> _picturesList;

        public RateTourViewModel(User user, Tour tour, RateTourView rateTourView)
        {
            _tourReviewService = new TourReviewService();
            _keyPointService = new KeyPointService();

            User = user;
            Tour = tour;
            RateTourView = rateTourView;
            

            _picturesList = new List<string>();
            KeyPoint = _keyPointService.GetWhereGuestJoined(Tour, User);
        }

        public void Send_Click()
        {
            TourReview = new TourReview(User, Tour, KeyPoint, (int)RateTourView.guidesKnowledgeBox.Value, (int)RateTourView.tourInterestBox.Value, (int)RateTourView.guidesLanguageCapabilityBox.Value, (string)RateTourView.CommentTextBox.Text);
            _tourReviewService.Create(TourReview);
            _tourReviewService.AddReviewsPictures(_picturesList, TourReview);
            MessageBox.Show("Hvala na recenziji");
            RateTourView.Close();

        }

        public void AddPicture_Click()
        {
            RateTourView.picturesOutput.Items.Add(RateTourView.pictureInputTextBox.Text);
            _picturesList.Add(RateTourView.pictureInputTextBox.Text);
            RateTourView.pictureInputTextBox.Clear();
        }
    }
}
