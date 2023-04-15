using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
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

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationDetailedView.xaml
    /// </summary>
    public partial class AccommodationDetailedView : Window
    {
        AccommodationDetailedViewModel AccommodationDetailedViewModel;
        public AccommodationDetailedView(User guest1, Accommodation selectedAccommodation)
        {
            InitializeComponent();
            AccommodationDetailedViewModel = new AccommodationDetailedViewModel(this, guest1,selectedAccommodation);
            DataContext = AccommodationDetailedViewModel;
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDetailedViewModel.ReservationButton_Click(sender, e);
        }

        private void ButtonDateCancelation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDetailedViewModel.ButtonDateCancelation_Click(sender, e);
        }
    }
}
