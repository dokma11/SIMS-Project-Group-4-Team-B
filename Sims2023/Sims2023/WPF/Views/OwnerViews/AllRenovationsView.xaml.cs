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
    /// Interaction logic for AllRenovationsView.xaml
    /// </summary>
    public partial class AllRenovationsView : Page
    {
        public AllRenovationsViewModel allRenovationsViewModel; 
        public AllRenovationsView(User user)
        {
            InitializeComponent();
            allRenovationsViewModel = new AllRenovationsViewModel(user);
            DataContext = allRenovationsViewModel;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            allRenovationsViewModel.Delete_Click();
        }

    }
}
