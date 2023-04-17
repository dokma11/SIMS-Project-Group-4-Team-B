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
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for GuestLiveTrackingTourView.xaml
    /// </summary>
    public partial class GuestLiveTrackingTourView : Window
    {
        public GuestLiveTrackingTourViewModel GuestLiveTrackingTourViewModel { get; set; }
        public GuestLiveTrackingTourView(Tour tour)
        {
            InitializeComponent();
            GuestLiveTrackingTourViewModel = new GuestLiveTrackingTourViewModel(tour,this);
            DataContext = tour;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GuestLiveTrackingTourViewModel.Cancel_Click();
        }
    }
}
