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
        public User Owner { get; set; }
        public RecommendationTabsView(User owner)
        {
            InitializeComponent();
            DataContext = this;
            Owner = owner;
        }

        public void Popular_Click(object sender, RoutedEventArgs e)
        {
            PopularLocationsView popular = new PopularLocationsView(Owner);
            FrameManager.Instance.MainFrame.Navigate(popular);
        }

        public void Unpopular_Click(object sender, RoutedEventArgs e)
        {
            UnopularLocationsView unpopular = new UnopularLocationsView(Owner);
            FrameManager.Instance.MainFrame.Navigate(unpopular);
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
