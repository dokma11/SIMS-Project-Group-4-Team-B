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
    public class RateTourViewModel
    {
        public TourReviewService _tourReviewService;
        public KeyPointService _keyPointService;
        public User User { get; set; }
        public Tour Tour { get; set; }
        public RateTourView RateTourView { get; set; }
        public List<string> _picturesList;

        public RateTourViewModel(User user,Tour tour,RateTourView rateTourView)
        {
            _tourReviewService = new TourReviewService();
            _keyPointService = new KeyPointService();

            User = user;
            Tour = tour;
            RateTourView = rateTourView;
            

            _picturesList = new List<string>(); 

            
        }

        public void Send_Click()
        {
            
            TourReview TourReview = new TourReview(User, Tour, _keyPointService.GetById(0), (int)RateTourView.guidesKnowledgeBox.Value, (int)RateTourView.tourInterestBox.Value, (int)RateTourView.guidesLanguageCapabilityBox.Value, (string)RateTourView.CommentTextBox.Text);
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
