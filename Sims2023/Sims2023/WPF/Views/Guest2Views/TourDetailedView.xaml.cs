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
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourDetailedView.xaml
    /// </summary>
    public partial class TourDetailedView : Window
    {
        public TourDetailedViewModel TourDetailedViewModel { get; set; }
        public int GuestNumber { get; set; }
        public TourDetailedView(Tour tour, int guestNumber,User user)
        {
            InitializeComponent();
            TourDetailedViewModel = new TourDetailedViewModel(this, tour,user);
            GuestNumber = guestNumber;
            DataContext = TourDetailedViewModel;
            guestNumberTextBlock.Text=guestNumber.ToString();
        }

        

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            TourDetailedViewModel.Cancel_Click();
        }

        private void NextPicture_Click(object sender, RoutedEventArgs e)
        {
            TourDetailedViewModel.NextPicture_Click();
        }

        private void PreviousPicture_Click(object sender, RoutedEventArgs e)
        {
            TourDetailedViewModel.PreviousPicture_Click();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            double clickPosition = e.GetPosition(image).X;
            double leftSidePosition = image.ActualWidth * 0.25;
            double rightSidePosition = image.ActualWidth * 0.75;

            if (clickPosition < leftSidePosition)
            {
                TourDetailedViewModel.PreviousPicture_Click();
            }
            else if (clickPosition > rightSidePosition)
            {
                TourDetailedViewModel.NextPicture_Click();
            }
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Image image = (Image)sender;
            double mousePosition = e.GetPosition(image).X;
            double leftSidePosition = image.ActualWidth * 0.25;
            double rightSidePosition = image.ActualWidth * 0.75;

            if (mousePosition < leftSidePosition)
            {
                LeftArrowImage.Visibility = Visibility.Visible;
                RightArrowImage.Visibility = Visibility.Collapsed;
                Canvas.SetLeft(LeftArrowImage, mousePosition);
                Canvas.SetTop(LeftArrowImage, e.GetPosition(image).Y);
            }
            else if (mousePosition > rightSidePosition)
            {
                RightArrowImage.Visibility = Visibility.Visible;
                LeftArrowImage.Visibility = Visibility.Collapsed;
                Canvas.SetLeft(RightArrowImage, mousePosition - RightArrowImage.Width);
                Canvas.SetTop(RightArrowImage, e.GetPosition(image).Y);
            }
            else
            {
                LeftArrowImage.Visibility = Visibility.Collapsed;
                RightArrowImage.Visibility = Visibility.Collapsed;
            }
        }


        private void NextImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
                TourDetailedViewModel.NextPicture_Click();
            
        }

        private void PreviousImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TourDetailedViewModel.PreviousPicture_Click();

        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            TourDetailedViewModel.ReserveTour_Click();
        }
    }
}
