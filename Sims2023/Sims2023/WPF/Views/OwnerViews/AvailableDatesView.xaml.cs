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
    /// Interaction logic for AvailableDatesView.xaml
    /// </summary>
    public partial class AvailableDatesView : Page
    {
        private AvailableDatesViewModel AvailableDatesViewModel;
        public AvailableDatesView(Accommodation selectedAccommodation, List<AccommodationStay> stayss)
        {
            InitializeComponent();
            AvailableDatesViewModel = new AvailableDatesViewModel(selectedAccommodation,stayss);
            DataContext = AvailableDatesViewModel;
        }
        
        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            AvailableDatesViewModel.Schedule_Click();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }
    }
}
