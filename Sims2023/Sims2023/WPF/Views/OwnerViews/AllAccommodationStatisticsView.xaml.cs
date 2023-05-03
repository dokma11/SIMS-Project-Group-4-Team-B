using Sims2023.Domain.Models;
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
    /// Interaction logic for AllAccommodationStatisticsView.xaml
    /// </summary>
    public partial class AllAccommodationStatisticsView : Page
    {
        public AllAccommodationsViewModel AllAccommodationsViewModel;
        public AllAccommodationStatisticsView(User user)
        {
            AllAccommodationsViewModel = new AllAccommodationsViewModel(user);
            InitializeComponent();
            DataContext = AllAccommodationsViewModel;
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            AllAccommodationsViewModel.Statistics_Click();
        }
    }
}
