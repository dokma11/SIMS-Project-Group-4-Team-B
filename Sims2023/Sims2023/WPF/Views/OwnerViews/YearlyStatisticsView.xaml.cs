using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for YearlyStatisticsView.xaml
    /// </summary>
    public partial class YearlyStatisticsView : Page
    {
        public YearlyStatisticsView(Accommodation Selected)
        {
            InitializeComponent();
            DataContext = new YearlyStatisticsViewModel(Selected);
        
        }
    }
}
