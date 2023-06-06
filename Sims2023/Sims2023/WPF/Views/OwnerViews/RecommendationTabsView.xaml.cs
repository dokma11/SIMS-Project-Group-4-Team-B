using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.OwnerViews
{
    public partial class RecommendationTabsView : Page
    {
        public User owner { get; set; }
        public RecommendationTabsView(User owner)
        {
            InitializeComponent();
            DataContext = this;
            owner = owner;
        }

        public void Popular_Click(object sender, RoutedEventArgs e)
        {
            PopularLocationsView popular = new PopularLocationsView(owner);
            FrameManager.Instance.MainFrame.Navigate(popular);
        }

        private void PlayVideo_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = TimeSpan.Zero;
            mediaElement.Play();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            mediaElement.Visibility = Visibility.Hidden;
        }
    }
}
