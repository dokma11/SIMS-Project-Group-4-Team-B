using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for addAccommodationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Page
    {

        public AccommodationRegistrationViewModel AccommodationRegistrationViewModel;
        public AccommodationRegistrationView( User owner)
        {
            InitializeComponent();
            AccommodationRegistrationViewModel = new AccommodationRegistrationViewModel(this, owner);
            DataContext = AccommodationRegistrationViewModel;

        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AccommodationRegistrationViewModel.countryComboBox_SelectionChanged(sender, e);
        }


    }
}