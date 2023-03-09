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
            
            submitButton.IsEnabled = false;
            addMoreDatesButton.IsEnabled = false;
            addKeyPointsButton.IsEnabled = false;

            _tourController = tourController;
            _locationController = locationController;
        }
        private void SubmitButtonClicked(object sender, RoutedEventArgs e)
        {
            _locationController.Create(Location);
            _tourController.Create(Tour, dateTimeList, Location);
            _tourController.AddToursLocation(Tour, Location, dateTimeList);
            Close();
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddMoreDates(object sender, RoutedEventArgs e) 
        {
            string inputString = dateTimeTextBox.Text;
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
                Tour.guideLanguage = (Tour.Language)0;
            }
            else if(language == "English")
            {
                Tour.guideLanguage = (Tour.Language)1;
            }
            if (language == "German")
            {
                Tour.guideLanguage = (Tour.Language)2;
            }
            if (language == "French")
            {
                Tour.guideLanguage = (Tour.Language)3;
            }
            if (language == "Spanish")
            {
                Tour.guideLanguage = (Tour.Language)4;
            }
            if (language == "Italian")
            {
                Tour.guideLanguage = (Tour.Language)5;
            }
            if (language == "Chinese")
            {
                Tour.guideLanguage = (Tour.Language)6;
            }
            if (language == "Japanese")
            {
                Tour.guideLanguage = (Tour.Language)7;
            }
        }

        private void AddKeyPoints(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);
            keyPointTextBox.Clear();

        }

        private void keyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(keyPointTextBox.Text.Length == 0)
            {
                addKeyPointsButton.IsEnabled = false;
            }
            else
            {
                addKeyPointsButton.IsEnabled = true;
            }
        }

        private void dateTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dateTimeTextBox.Text.Length == 0)
            {
                addMoreDatesButton.IsEnabled = false;
            }
            else
            {
                addMoreDatesButton.IsEnabled = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
