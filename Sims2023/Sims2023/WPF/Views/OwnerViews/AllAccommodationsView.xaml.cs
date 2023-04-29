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
    /// Interaction logic for AllAccommodationsView.xaml
    /// </summary>
    public partial class AllAccommodationsView : Page
    {
        public AllAccommodationsViewModel AllAccommodationsViewModel;
        public AllAccommodationsView(User owner)
        {
            InitializeComponent();
            AllAccommodationsViewModel = new AllAccommodationsViewModel(owner);
            DataContext = AllAccommodationsViewModel;
        }
    }
}
