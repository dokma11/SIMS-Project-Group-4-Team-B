using Sims2023.Controller;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for CreateTourView.xaml
    /// </summary>
    public partial class CreateTourView : Window
    {
        private TourController _tourController;
        private LocationController _locationController;
        private List<DateTime> dateTimeList;
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public CreateTourView(TourController tourController, LocationController locationController)
        {
            InitializeComponent();
            DataContext = this;
            Tour = new Tour();
            Location = new Location();
            dateTimeList = new List<DateTime>();

            _tourController = tourController;
            _locationController = locationController;
        }
        private void SubmitButtonClicked(object sender, RoutedEventArgs e)
        {
            _tourController.Create(Tour);
            _locationController.Create(Location);
            _tourController.AddToursLocation(Tour, Location);
            Close();
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddMoreDates(object sender, RoutedEventArgs e) 
        {
            string inputString = startTextBox.Text;
            DateTime dateTime;

            if (DateTime.TryParse(inputString, out dateTime))
            {
                dateTimeList.Add(dateTime);
            }
            else
            {
                MessageBox.Show("Invalid DateTime format.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
