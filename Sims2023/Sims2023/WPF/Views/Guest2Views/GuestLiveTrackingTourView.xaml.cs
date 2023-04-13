using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Sims2023.Application.Services;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for GuestLiveTrackingTourView.xaml
    /// </summary>
    public partial class GuestLiveTrackingTourView : Window
    {
        public Tour Tour;
        
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }


        private LocationService _locationService;
        private KeyPointService _keyPointService;
        public GuestLiveTrackingTourView(Tour tour)
        {
            InitializeComponent();
            DataContext = tour;

            _locationService=new LocationService();
            _keyPointService=new KeyPointService();
            

            Tour = tour;
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            AddLocationsToTour(Locations);
            KeyPoints = new ObservableCollection<KeyPoint>(_keyPointService.GetAll());
            foreach (var keyPoint in KeyPoints)
            {
                if (keyPoint.Tour.Id == Tour.Id && keyPoint.CurrentState==KeyPoint.State.BeingVisited)
                {
                    keyPointTextBlock.Text = keyPoint.Name;   
                }
            }

        }

        private void AddLocationsToTour(ObservableCollection<Location> locations)
        {

            
                foreach (var location in locations)
                {
                    if (Tour.LocationId == location.Id)
                    {
                        Tour.City = location.City;
                        Tour.Country = location.Country;
                    }
                }
            

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
