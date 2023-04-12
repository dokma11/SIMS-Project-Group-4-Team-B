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

        public TourReviewService _tourReviewService;
        public TourReview TourReview;
        public RateTourView(User user,Tour tour)
        {
            InitializeComponent();
            DataContext = this;

            Tour = tour;
            User = user;

            _tourReviewService = new TourReviewService();
            _tourReviewService.Subscribe(this);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
             TourReview = new TourReview(User, Tour.Guide, Tour, (int)guidesKnowledgeBox.Value, (int)tourInterestBox.Value, (int)guidesLanguageCapabilityBox.Value,(string)CommentTextBox.Text);
            _tourReviewService.Create(TourReview);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
