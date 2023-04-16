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
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourDetailedView.xaml
    /// </summary>
    public partial class TourDetailedView : Window
    {
        public TourDetailedViewModel TourDetailedViewModel { get; set; }
        public TourDetailedView(User user,Tour tour)
        {
            InitializeComponent();
            TourDetailedViewModel = new TourDetailedViewModel(this, user, tour);
            DataContext = TourDetailedViewModel;
        }

        

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            TourDetailedViewModel.Cancel_Click();
        }

        private void NextPicture_Click(object sender, RoutedEventArgs e)
        {
            TourDetailedViewModel.NextPicture_Click();
        }

        private void PreviousPicture_Click(object sender, RoutedEventArgs e)
        {
            TourDetailedViewModel.PreviousPicture_Click();
        }
    }
}
