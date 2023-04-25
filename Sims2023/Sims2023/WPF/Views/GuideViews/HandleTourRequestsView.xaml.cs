using Sims2023.Application.Services;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for HandleTourRequestsView.xaml
    /// </summary>
    public partial class HandleTourRequestsView : Window
    {
        public HandleTourRequestsViewModel HandleTourRequestsViewModel;
        public HandleTourRequestsView(RequestService requestService)
        {
            InitializeComponent();

            HandleTourRequestsViewModel = new(requestService);
            DataContext = HandleTourRequestsViewModel;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string locationSearchTerm = locationTextBox.Text;
            string guestNumberSearchTerm = guestNumberTextBox.Text;
            string languageSearchTerm = languageTextBox.Text;
            string dateStartSearchTerm = dateStartTextBox.Text;
            string dateEndSearchTerm = dateEndTextBox.Text;

            requestDataGrid.ItemsSource = HandleTourRequestsViewModel.FilterRequests(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm);
        }
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            HandleTourRequestsViewModel.AcceptRequest();
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            HandleTourRequestsViewModel.DeclineRequest();
        }
    }
}
