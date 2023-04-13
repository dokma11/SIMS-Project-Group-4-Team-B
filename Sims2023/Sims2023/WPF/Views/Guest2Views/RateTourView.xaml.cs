using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sims2023.Domain.Models;
using Sims2023.Application.Services;
using Sims2023.Observer;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for RateTourView.xaml
    /// </summary>
    public partial class RateTourView : Window,IObserver
    {
        public User User { get; set; }
        public Tour Tour { get; set; }

        public KeyPoint KeyPoint { get; set; }

        public TourReviewService _tourReviewService;
        public KeyPointService _keyPointService;
        public TourReview TourReview { get; set; }
        public RateTourView(User user,Tour tour)
        {
            InitializeComponent();
            DataContext = this;

            Tour = tour;
            User = user;
             
            //TourReview = new TourReview();

            _tourReviewService = new TourReviewService();
            _tourReviewService.Subscribe(this);

            _keyPointService = new KeyPointService();
            _keyPointService.Subscribe(this);

            KeyPoint = _keyPointService.GetById(0);//default and then guide will check at which KeyPoint has joined 
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
             TourReview = new TourReview(User, Tour,KeyPoint, (int)guidesKnowledgeBox.Value, (int)tourInterestBox.Value, (int)guidesLanguageCapabilityBox.Value,(string)CommentTextBox.Text);
            _tourReviewService.Create(TourReview);
            MessageBox.Show("Hvala na recenziji");
            Close();
            
        }

        public void Update()
        {
            
        }
    }
}
