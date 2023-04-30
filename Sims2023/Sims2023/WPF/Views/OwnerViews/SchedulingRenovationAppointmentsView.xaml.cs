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
    /// Interaction logic for SchedulingRenovationAppointmentsView.xaml
    /// </summary>
    public partial class SchedulingRenovationAppointmentsView : Page
    {
        public SchedulingRenovationAppointmentsViewModel schedulingRenovationAppointmentsViewModel;
        public string welcomeString { get; set; }
        public SchedulingRenovationAppointmentsView(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            schedulingRenovationAppointmentsViewModel = new SchedulingRenovationAppointmentsViewModel(this, selectedAccommodation);
            DataContext = schedulingRenovationAppointmentsViewModel;
            welcomeString = selectedAccommodation.Name;
        }

        private void FindDates(object sender, RoutedEventArgs e)
        {
            schedulingRenovationAppointmentsViewModel.FindDates();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }

    }
}
