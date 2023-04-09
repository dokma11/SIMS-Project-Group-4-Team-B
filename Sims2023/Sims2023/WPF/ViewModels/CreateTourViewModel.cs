using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels
{
    public partial class CreateTourViewModel : INotifyPropertyChanged
    {
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public KeyPoint KeyPoint { get; set; }

        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;

        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;
        public User LoggedInGuide { get; set; }
        //private bool dateTimeButtonClicked = false;
        public CreateTourViewModel(TourService tourService, LocationService locationService, KeyPointService keyPointService, User loggedInGuide)
        {
            InitializeComponent();
            DataContext = this;

            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();

            _dateTimeList = new List<DateTime>();
            _keyPointsList = new List<string>();

            //submitButton.IsEnabled = true;
            //addMoreDatesButton.IsEnabled = false;
            addKeyPointsButton.IsEnabled = false;

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;

            LoggedInGuide = loggedInGuide;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (Tour.IsValid /*&& dateTimeButtonClicked */&& keyPointsOutput.Items.Count > 1)
            {
                int newToursNumber = _dateTimeList.Count;
                _locationService.Create(Location);
                _tourService.Create(Tour, _dateTimeList, Location, LoggedInGuide);
                int firstToursId = Tour.Id - newToursNumber + 1;
                _tourService.AddToursLocation(Tour, Location, newToursNumber);
                _keyPointService.Create(KeyPoint, _keyPointsList, firstToursId, newToursNumber);
                _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
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
        /*
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
        */
        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string language = ((ComboBoxItem)cBox.SelectedItem).Content.ToString();

            switch (language)
            {
                case "Serbian":
                    Tour.GuideLanguage = Tour.Language.Serbian;
                    break;
                case "English":
                    Tour.GuideLanguage = Tour.Language.English;
                    break;
                case "German":
                    Tour.GuideLanguage = Tour.Language.German;
                    break;
                case "French":
                    Tour.GuideLanguage = Tour.Language.French;
                    break;
                case "Spanish":
                    Tour.GuideLanguage = Tour.Language.Spanish;
                    break;
                case "Italian":
                    Tour.GuideLanguage = Tour.Language.Italian;
                    break;
                case "Chinese":
                    Tour.GuideLanguage = Tour.Language.Chinese;
                    break;
                case "Japanese":
                    Tour.GuideLanguage = Tour.Language.Japanese;
                    break;
            }
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
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
        /*
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
        */
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
