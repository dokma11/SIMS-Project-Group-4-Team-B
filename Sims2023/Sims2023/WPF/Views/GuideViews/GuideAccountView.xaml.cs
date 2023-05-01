using Sims2023.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for GuideAccountView.xaml
    /// </summary>
    public partial class GuideAccountView : Page
    {
        public User LoggedInGuide { get; set; }
        public GuideAccountView(User loggedInGuide)
        {
            InitializeComponent();

            LoggedInGuide = loggedInGuide;

            nameLabel.Content = loggedInGuide.Name;
            surnameLabel.Content = loggedInGuide.Surname;
            ageLabel.Content = loggedInGuide.Age;
            phoneNumberLabel.Content = loggedInGuide.PhoneNumber;
            emailLabel.Content = loggedInGuide.Email;
        }

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RequestsButton_Click(object sender, RoutedEventArgs e)
        {
            //RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide);
            //FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            //GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide);
            //FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            //nista
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
        }
    }
}
