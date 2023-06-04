using Sims2023.WPF.ViewModels.OwnerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for RecommendationTabsView.xaml
    /// </summary>
    public partial class RecommendationTabsView : Page
    {
        
        public RecommendationTabsView()
        {
            InitializeComponent();
            DataContext= this;
        }

        public void Popular_Click(object sender, RoutedEventArgs e)
        {
            PopularLocationsView popular = new PopularLocationsView();
            FrameManager.Instance.MainFrame.Navigate(popular);
        }
    }
}
