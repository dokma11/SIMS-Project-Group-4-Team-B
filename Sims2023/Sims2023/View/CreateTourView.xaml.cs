﻿using Sims2023.Controller;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for CreateTourView.xaml
    /// </summary>
    public partial class CreateTourView : Window
    {
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public KeyPoint KeyPoint { get; set; }

        private TourController _tourController;
        private LocationController _locationController;
        private KeyPointController _keyPointController;

        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;

        private bool dateTimeButtonClicked = false;
        public CreateTourView(TourController tourController, LocationController locationController, KeyPointController keyPointController)
        {
            InitializeComponent();
            DataContext = this;

            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();

            _dateTimeList = new List<DateTime>();
            _keyPointsList = new List<string>();

            submitButton.IsEnabled = true;
            addMoreDatesButton.IsEnabled = false;
            addKeyPointsButton.IsEnabled = false;

            _tourController = tourController;
            _locationController = locationController;
            _keyPointController = keyPointController;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (Tour.IsValid && dateTimeButtonClicked && keyPointsOutput.Items.Count > 1)
            {
                int newToursNumber = _dateTimeList.Count;
                _locationController.Create(Location);
                _tourController.Create(Tour, _dateTimeList, Location);
                int firstToursId = Tour.Id - newToursNumber + 1;
                _tourController.AddToursLocation(Tour, Location, newToursNumber);
                _keyPointController.Create(KeyPoint, _keyPointsList, firstToursId, newToursNumber);
                _tourController.AddToursKeyPoints(_keyPointsList, firstToursId);
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

        private void AddMoreDates(object sender, RoutedEventArgs e)
        {
            string inputString = dateTimeTextBox.Text;
            dateTimeButtonClicked = true;

            if (DateTime.TryParse(inputString, out DateTime dateTime))
            {
                if (!CheckIfDateExists(dateTime))
                {
                    _dateTimeList.Add(dateTime);
                }
            }
            else
            {
                MessageBox.Show("Uneli ste nepravilan format datuma.");
            }
        }

        private bool CheckIfDateExists(DateTime dateTime)
        {
            foreach (var dateTimeInstance in _dateTimeList)
            {
                if (dateTimeInstance == dateTime)
                {
                    return true;
                }
            }
            return false;
        }

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            ComboBoxItem cbItem = (ComboBoxItem)cBox.SelectedItem;
            string language = (string)cbItem.Content;
            //The numbers represent the order in which the enum members are arranged, the first is Serbian (number 0), the second English (number 1) etc...
            if (language == "Serbian")
            {
                Tour.GuideLanguage = (Tour.Language)0;
            }
            else if (language == "English")
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

        private void AddKeyPoints(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            keyPointsOutput.Items.Add(inputText);

            if (_keyPointsList.Count == 0)
            {
                _keyPointsList.Add(inputText);
            }
            else
            {
                if (!CheckIfKeyPointExists(inputText))
                {
                    _keyPointsList.Add(inputText);
                }
            }
            keyPointTextBox.Clear();
        }

        private bool CheckIfKeyPointExists(string inputText)
        {
            foreach (var keyPoint in _keyPointsList)
            {
                if (inputText == keyPoint)
                {
                    return true;
                }
            }
            return false;
        }

        private void KeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (keyPointTextBox.Text.Length == 0)
            {
                addKeyPointsButton.IsEnabled = false;
            }
            else
            {
                addKeyPointsButton.IsEnabled = true;
            }
        }

        private void DateTimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
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
        //Prevent the user from entering any character other than a number
        private void MaxGuestNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+$").IsMatch(e.Text);
        }

        private void LengthTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+$").IsMatch(e.Text);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
