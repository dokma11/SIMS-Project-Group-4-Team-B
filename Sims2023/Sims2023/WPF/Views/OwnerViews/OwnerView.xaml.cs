using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using Sims2023.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for OpenAccommodationRegistrationView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        public ObservableCollection<AccommodationCancellation> AccommodationCancellations { get; set; }
        public List<AccommodationReservation> Reservatons { get; set; }
        public List<AccommodationReservation> GradableGuests { get; set; }

        public OwnerViewModel ownerViewModel { get; set; }

        public User User { get; set; }
        public OwnerView(User owner)
        {

            InitializeComponent();
            DataContext = this;
            MenuButton.IsChecked = false;
            User = owner;
            ownerViewModel = new OwnerViewModel(User);

        }

        private void MenuButton_Checked(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 430;
            animation.Duration = TimeSpan.FromSeconds(0.2);
            MenuPanel.BeginAnimation(Grid.WidthProperty, animation);
        }

        private void MenuButton_Unchecked(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0;
            animation.Duration = TimeSpan.FromSeconds(0.2);
            MenuPanel.BeginAnimation(Grid.WidthProperty, animation);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ownerViewModel.Window_Loaded();
        }

        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            ownerViewModel.Grade_Click();
        }

        private void AddAccommodation_Click(object sender, RoutedEventArgs e)
        {
            MenuButton.IsChecked = false;
            ActionBarTextBlock.Text = RegistrationButton.Content.ToString();
            AccommodationRegistrationView registrationView = new AccommodationRegistrationView(User);
            MainFrame.Navigate(registrationView);
        }

        private void Grades_Given_From_Guests(object sender, RoutedEventArgs e)
        {
            ownerViewModel.Grades_Given_From_Guests();
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            ownerViewModel.Reservations_Click();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MenuButton.IsChecked = false;
            ActionBarTextBlock.Text = StartButton.Content.ToString();
            StartView start = new StartView(User);
            MainFrame.Navigate(start);
        }
    }
}