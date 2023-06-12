using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;

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
