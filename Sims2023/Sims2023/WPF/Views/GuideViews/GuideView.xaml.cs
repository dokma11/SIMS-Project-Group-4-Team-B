using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for GuideView.xaml
    /// </summary>
    public partial class GuideView : Window
    {
        public User LoggedInGuide { get; set; }
        public GuideView(User loggedInGuide)
        {
            InitializeComponent();

            LoggedInGuide = loggedInGuide;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FrameManagerGuide.Instance.MainFrame = MainFrameGuide;
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }
    }
}
