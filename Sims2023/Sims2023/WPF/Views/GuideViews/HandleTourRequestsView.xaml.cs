using Sims2023.Application.Services;
using Sims2023.Domain.Models;
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
        public User LoggedInGuide { get; set; }
        private TourService _tourService;
        private KeyPointService _keyPointService;
        public HandleTourRequestsView(RequestService requestService, User loggedInGuide, TourService tourService, KeyPointService keyPointService)
        {
            InitializeComponent();

            HandleTourRequestsViewModel = new(requestService);
            DataContext = HandleTourRequestsViewModel;

            LoggedInGuide = loggedInGuide;
            _tourService = tourService;
            _keyPointService = keyPointService;
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
            //CreateTourFromRequestView createTourFromRequestView = new(HandleTourRequestsViewModel.SelectedRequest, LoggedInGuide, _tourService, _keyPointService);
            //createTourFromRequestView.Show();
            HandleTourRequestsViewModel.AcceptRequest();
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            HandleTourRequestsViewModel.DeclineRequest();
        }
    }
}
