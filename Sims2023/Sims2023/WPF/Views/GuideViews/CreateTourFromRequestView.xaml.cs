using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for CreateTourFromRequestView.xaml
    /// </summary>
    public partial class CreateTourFromRequestView : Window
    {
        public CreateTourFromRequestViewModel CreateTourFromRequestViewModel;
        bool addDatesButtonClicked;
        public CreateTourFromRequestView(Request selectedTourRequest, User loggedInGuide, TourService tourService, KeyPointService keyPointService)
        {
            InitializeComponent();

            CreateTourFromRequestViewModel = new(selectedTourRequest, loggedInGuide, tourService, keyPointService);
            DataContext = CreateTourFromRequestViewModel;

            addDatesButtonClicked = false;  
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);
            CreateTourFromRequestViewModel.AddKeyPointsToList(inputText);
            keyPointTextBox.Clear();
        }

        private void AddDatesButton_Click(object sender, RoutedEventArgs e)
        {
            addDatesButtonClicked = true;
            CreateTourFromRequestViewModel.AddDatesToList(dateTimeTextBox.Text);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (addDatesButtonClicked && keyPointsOutput.Items.Count > 1)
            {
                CreateTourFromRequestViewModel.ConfirmCreation();
                Close();
            }
            else
            {
                MessageBox.Show("Popunite sva polja molim Vas");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void KeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addKeyPointsButton.IsEnabled = !string.IsNullOrEmpty(keyPointTextBox.Text);
        }

        private void DateTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addDatesButton.IsEnabled = !string.IsNullOrEmpty(dateTimeTextBox.Text);
        }
    }
}
