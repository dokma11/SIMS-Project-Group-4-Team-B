using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
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
