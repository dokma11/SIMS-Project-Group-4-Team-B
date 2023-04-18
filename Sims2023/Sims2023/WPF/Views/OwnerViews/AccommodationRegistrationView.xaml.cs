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
    public partial class AccommodationRegistrationView : Window
    {

        public AccommodationRegistrationViewModel AccommodationRegistrationViewModel;
        public AccommodationRegistrationView(AccommodationService accommodationCtrl1, User owner)
        {
            InitializeComponent();
            AccommodationRegistrationViewModel = new AccommodationRegistrationViewModel(this, accommodationCtrl1, owner);
            DataContext = AccommodationRegistrationViewModel;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRegistrationViewModel.SaveButton_Click(sender, e);
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRegistrationViewModel.Registration_Click(sender, e);
        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AccommodationRegistrationViewModel.countryComboBox_SelectionChanged(sender, e);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}