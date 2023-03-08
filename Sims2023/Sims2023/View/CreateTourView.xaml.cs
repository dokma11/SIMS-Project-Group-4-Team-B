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
            if(dateTimeList.Count == 0) 
            {
                _tourController.Create(Tour, dateTimeList);
                _locationController.Create(Location);
                _tourController.AddToursLocation(Tour, Location);
            }
            else 
            {
                _locationController.Create(Location);
                _tourController.Create(Tour, dateTimeList);
                _tourController.AddToursLocation(Tour, Location);
            }

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
            int counter = 0;

            if (DateTime.TryParse(inputString, out dateTime))
            {
                foreach(var dateTimeInstance in dateTimeList)
                {
                    if(dateTimeInstance == dateTime) 
                    {
                        counter++;
                    }
                }
                if(counter == 0)
                {
                    dateTimeList.Add(dateTime);
                }
                counter = 0;
            }
            else
            {
                MessageBox.Show("Uneli ste nepravilan format datuma.");
            }
        }

        private void ComboBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            ComboBoxItem cbItem = (ComboBoxItem)cBox.SelectedItem;
            string language = (string)cbItem.Content;
            //The numbers represent the order in which the enum members are arranged, the first is Serbian (number 0), the second English (number 1) etc...
            if (language == "Serbian")
            {
                Tour.GuideLanguage = (Tour.Language)0;
            }
            else if(language == "English")
            {
                Tour.GuideLanguage = (Tour.Language)1;
            }
            if (language == "German")
            {
                Tour.GuideLanguage = (Tour.Language)2;
            }
            if (language == "French")
            {
                Tour.GuideLanguage = (Tour.Language)3;
            }
            if (language == "Spanish")
            {
                Tour.GuideLanguage = (Tour.Language)4;
            }
            if (language == "Italian")
            {
                Tour.GuideLanguage = (Tour.Language)5;
            }
            if (language == "Chinese")
            {
                Tour.GuideLanguage = (Tour.Language)6;
            }
            if (language == "Japanese")
            {
                Tour.GuideLanguage = (Tour.Language)7;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
