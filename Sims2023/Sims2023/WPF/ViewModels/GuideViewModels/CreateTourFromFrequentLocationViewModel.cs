﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Threading;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class CreateTourFromFrequentLocationViewModel : INotifyPropertyChanged
    {
        public Tour NewTour { get; set; }
        public KeyPoint NewKeyPoint { get; set; }
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private TourReviewService _tourReviewService;
        private RequestService _requestService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        private UserService _userService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private TourNotificationService _tourNotificationService;
        private ComplexTourRequestService _complexTourRequestService;
        private SubTourRequestService _subTourRequestService;

        public Location SelectedLocation { get; set; }
        private List<DateTime> _dateTimeList;
        private List<string> _keyPointsList;
        public ObservableCollection<string> ListBoxItems { get; set; }
        private string _keyPointTextBoxText;
        public string KeyPointTextBoxText
        {
            get { return _keyPointTextBoxText; }
            set
            {
                if (_keyPointTextBoxText != value)
                {
                    _keyPointTextBoxText = value;
                    OnPropertyChanged(nameof(KeyPointTextBoxText));
                }
            }
        }
        public User LoggedInGuide { get; set; }

        private DateTime _selectedDate = DateTime.Today.AddDays(1);
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));

                    if (!_tourService.GetBusyDates(LoggedInGuide).Contains(value))
                    {
                        _dateTimeList.Add(value);
                        NewTour.Start = value;
                    }
                }
            }
        }

        private bool _isLabelVisible;
        public bool IsLabelVisible
        {
            get { return _isLabelVisible; }
            set
            {
                if (_isLabelVisible != value)
                {
                    _isLabelVisible = value;
                    OnPropertyChanged(nameof(IsLabelVisible));
                }
            }
        }

        private Brush _nameTextBoxBorderBrush;
        public Brush NameTextBoxBorderBrush
        {
            get { return _nameTextBoxBorderBrush; }
            set
            {
                _nameTextBoxBorderBrush = value;
                OnPropertyChanged(nameof(NameTextBoxBorderBrush));
            }
        }

        private Brush _keyPointTextBoxBorderBrush;
        public Brush KeyPointTextBoxBorderBrush
        {
            get { return _keyPointTextBoxBorderBrush; }
            set
            {
                _keyPointTextBoxBorderBrush = value;
                OnPropertyChanged(nameof(KeyPointTextBoxBorderBrush));
            }
        }

        private Brush _picturesTextBoxBorderBrush;
        public Brush PicturesTextBoxBorderBrush
        {
            get { return _picturesTextBoxBorderBrush; }
            set
            {
                _picturesTextBoxBorderBrush = value;
                OnPropertyChanged(nameof(PicturesTextBoxBorderBrush));
            }
        }

        private Brush _listBoxBorderBrush;
        public Brush ListBoxBorderBrush
        {
            get { return _listBoxBorderBrush; }
            set
            {
                _listBoxBorderBrush = value;
                OnPropertyChanged(nameof(ListBoxBorderBrush));
            }
        }

        private Brush _datePickerBorderBrush;
        public Brush DatePickerBorderBrush
        {
            get { return _datePickerBorderBrush; }
            set
            {
                _datePickerBorderBrush = value;
                OnPropertyChanged(nameof(DatePickerBorderBrush));
            }
        }

        private Brush _descriptionBorderBrush;
        public Brush DescriptionBorderBrush
        {
            get { return _descriptionBorderBrush; }
            set
            {
                _descriptionBorderBrush = value;
                OnPropertyChanged(nameof(DescriptionBorderBrush));
            }
        }

        public DateTime DisplayDateStart { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand CreateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand HomePageNavigationCommand { get; set; }
        public RelayCommand ToursPageNavigationCommand { get; set; }
        public RelayCommand ReviewsPageNavigationCommand { get; set; }
        public RelayCommand AccountPageNavigationCommand { get; set; }

        public CreateTourFromFrequentLocationViewModel(Location selectedLocation, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            CreateCommand = new RelayCommand(Executed_CreateCommand, CanExecute_CreateCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand, CanExecute_CancelCommand);
            AddItemCommand = new RelayCommand(Executed_AddItemCommand, CanExecute_AddItemCommand);
            HomePageNavigationCommand = new RelayCommand(Executed_HomePageNavigationCommand, CanExecute_HomePageNavigationCommand);
            ToursPageNavigationCommand = new RelayCommand(Executed_ToursPageNavigationCommand, CanExecute_ToursPageNavigationCommand);
            ReviewsPageNavigationCommand = new RelayCommand(Executed_ReviewsPageNavigationCommand, CanExecute_ReviewsPageNavigationCommand);
            AccountPageNavigationCommand = new RelayCommand(Executed_AccountPageNavigationCommand, CanExecute_AccountPageNavigationCommand);

            SelectedLocation = selectedLocation;

            NewTour = new();
            NewKeyPoint = new();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;
            _tourReservationService = tourReservationService;
            _voucherService = voucherService;
            _userService = userService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _tourNotificationService = tourNotificationService;
            _countriesAndCitiesService = new CountriesAndCitiesService();
            _complexTourRequestService = complexTourRequestService;
            _subTourRequestService = subTourRequestService;

            _dateTimeList = new List<DateTime>();
            _keyPointsList = new List<string>();
            ListBoxItems = new();

            LoggedInGuide = loggedInGuide;

            DisplayDateStart = DateTime.Today.AddDays(1);

            NameTextBoxBorderBrush = Brushes.Gray;
            KeyPointTextBoxBorderBrush = Brushes.Gray;
            ListBoxBorderBrush = Brushes.Gray;
            PicturesTextBoxBorderBrush = Brushes.Gray;
            DatePickerBorderBrush = Brushes.Gray;
            DescriptionBorderBrush = Brushes.Gray;
        }

        public void Executed_CreateCommand(object obj)
        {
            if (NewTour.Name != "" && NewTour.MaxGuestNumber > 0 && NewTour.Length > 0 && NewTour.ConcatenatedPictures != ""
                && _dateTimeList.Count > 0 && ListBoxItems.Count > 1 && NewTour.Description != ""
                && !_tourService.GetBusyDates(LoggedInGuide).Contains(NewTour.Start))
            {
                NewTour.Location = SelectedLocation;
                NewTour.GuideLanguage = (ToursLanguage)NewTour.GuideLanguage;
                _locationService.Create(SelectedLocation);
                _tourService.Create(NewTour, _dateTimeList, SelectedLocation, LoggedInGuide);
                int firstToursId = NewTour.Id - _dateTimeList.Count + 1;
                _tourService.AddToursLocation(NewTour, SelectedLocation, _dateTimeList.Count);
                _keyPointService.Create(NewKeyPoint, _keyPointsList, firstToursId, _dateTimeList.Count);
                _tourService.AddToursKeyPoints(_keyPointsList, firstToursId);
                NotifyGuests();

                RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
                FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
            }
            else
            {
                ValidationErrorLabelEvent();
            }
        }

        public void NotifyGuests()
        {
            foreach (var req in _requestService.GetByLocation(SelectedLocation))
            {
                TourNotification tourNotification = new(NewTour, req.Guest, NotificationType.MatchedTourRequestsLocation);
                _tourNotificationService.Create(tourNotification);
            }
        }

        public bool CanExecute_CreateCommand(object obj)
        {
            return true;
        }

        public void ValidationErrorLabelEvent()
        {
            IsLabelVisible = true;

            if (NewTour.Name == null)
            {
                NameTextBoxBorderBrush = Brushes.Red;
            }

            if (ListBoxItems.Count <= 1)
            {
                KeyPointTextBoxBorderBrush = Brushes.Red;
                ListBoxBorderBrush = Brushes.Red;
            }

            if (NewTour.ConcatenatedPictures == null)
            {
                PicturesTextBoxBorderBrush = Brushes.Red;
            }

            if (SelectedDate == DateTime.MinValue || _tourService.GetBusyDates(LoggedInGuide).Contains(NewTour.Start))
            {
                DatePickerBorderBrush = Brushes.Red;
            }

            if (NewTour.Description == null)
            {
                DescriptionBorderBrush = Brushes.Red;
            }

            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            IsLabelVisible = false;

            NameTextBoxBorderBrush = Brushes.Gray;
            KeyPointTextBoxBorderBrush = Brushes.Gray;
            ListBoxBorderBrush = Brushes.Gray;
            PicturesTextBoxBorderBrush = Brushes.Gray;
            DatePickerBorderBrush = Brushes.Gray;
            DescriptionBorderBrush = Brushes.Gray;

            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        public void Executed_CancelCommand(object obj)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        public bool CanExecute_CancelCommand(object obj)
        {
            return true;
        }

        private void Executed_AddItemCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(KeyPointTextBoxText) && !ListBoxItems.Contains(KeyPointTextBoxText))
            {
                ListBoxItems.Add(KeyPointTextBoxText);
                _keyPointsList.Add(KeyPointTextBoxText);
                KeyPointTextBoxText = "";
            }
        }

        private bool CanExecute_AddItemCommand(object obj)
        {
            return true;
        }

        //TOOLBAR

        private void Executed_HomePageNavigationCommand(object obj)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private bool CanExecute_HomePageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_ToursPageNavigationCommand(object obj)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private bool CanExecute_ToursPageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_ReviewsPageNavigationCommand(object obj)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private bool CanExecute_ReviewsPageNavigationCommand(object obj)
        {
            return true;
        }

        private void Executed_AccountPageNavigationCommand(object obj)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }

        private bool CanExecute_AccountPageNavigationCommand(object obj)
        {
            return true;
        }
    }
}
