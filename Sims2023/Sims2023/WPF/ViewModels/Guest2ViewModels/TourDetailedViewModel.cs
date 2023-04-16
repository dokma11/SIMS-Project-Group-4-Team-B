using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class TourDetailedViewModel
    {
        TourDetailedView TourDetailedView;
        public User User { get; set; }
        public Tour Tour { get; set; }

        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Location> Locations { get; set; }

        private TourService _tourService;
        private LocationService _locationService;
        public int currentIndex;

        

        
        public TourDetailedViewModel(TourDetailedView tourDetailedView,User user,Tour tour)
        {
            TourDetailedView = tourDetailedView;
            User = user;
            Tour = tour;
            _tourService = new TourService();
            _locationService = new LocationService();
            Tours = new ObservableCollection<Tour>(_tourService.GetAvailable());
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            
            currentIndex = 0;
           
            FillTextBoxes();
        }

        public void FillTextBoxes()
        {
            TourDetailedView.nameTextBlock.Text = Tour.Name;
            TourDetailedView.cityTextBlock.Text = Tour.Location.City;
            TourDetailedView.countryTextBlock.Text = Tour.Location.Country;
            TourDetailedView.languageTextBlock.Text = Tour.GuideLanguage.ToString();
            TourDetailedView.hoursTextBlock.Text=Tour.Length.ToString();
            TourDetailedView.startTimeTextBlock.Text = Tour.Start.ToString();
            TourDetailedView.descriptionTextBlock.Text=Tour.Description;
            TourDetailedView.tourImage.Source = new BitmapImage(_tourService.GetPictureUri(Tour,currentIndex));
            
           
        }

        public void NextPicture_Click()
        {
            
            if (currentIndex < Tour.Pictures.Count - 1)
            {
                currentIndex++;
            }
            TourDetailedView.tourImage.Source = new BitmapImage(_tourService.GetPictureUri(Tour, currentIndex));
              

        }
        public void PreviousPicture_Click()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            TourDetailedView.tourImage.Source = new BitmapImage(_tourService.GetPictureUri(Tour, currentIndex));
        }
       

        public void Cancel_Click()
        {
            TourDetailedView.Close();
        }
    }
}
