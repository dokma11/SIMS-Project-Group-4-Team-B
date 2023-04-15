using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class RateTourViewModel:IObserver
    {
        public KeyPoint KeyPoint { get; set; }

        public TourReviewService _tourReviewService;
        public KeyPointService _keyPointService;
        public TourReview TourReview { get; set; }
        public User User { get; set; }
        public Tour Tour { get; set; }
        public RateTourView RateTourView { get; set; }

        public RateTourViewModel(User user,Tour tour,RateTourView rateTourView)
        {
            _tourReviewService = new TourReviewService();
            _keyPointService = new KeyPointService();

            User = user;
            Tour = tour;
            RateTourView = rateTourView;
            TourReview = new TourReview();

            KeyPoint = _keyPointService.GetById(0);//default and then guide will check at which KeyPoint has joined 
        }

        public void Send_Click()
        {
            
            TourReview = new TourReview(User, Tour, KeyPoint, (int)RateTourView.guidesKnowledgeBox.Value, (int)RateTourView.tourInterestBox.Value, (int)RateTourView.guidesLanguageCapabilityBox.Value, (string)RateTourView.CommentTextBox.Text);
            _tourReviewService.Create(TourReview);
            _tourReviewService.AddReviewsPictures(RateTourView._picturesList, TourReview);
            MessageBox.Show("Hvala na recenziji");
            RateTourView.Close();

        }

        public void AddPicture_Click()
        {

        }

        public void Update()
        {
            
        }
    }
}
