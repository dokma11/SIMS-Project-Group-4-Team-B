using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for GuestReviewsView.xaml
    /// </summary>
    public partial class GuestReviewsView : Window
    {
        //public Review UserReview = SelectedReview { get; set;}
        //public ObservableCollection<Review> AllReviews { get; set; }
        //public ObservableCollection<Review> ReviewsToDisplay { get; set; }

        public GuestReviewsView()
        {
            InitializeComponent();
            DataContext = this;
        }


    }
}
