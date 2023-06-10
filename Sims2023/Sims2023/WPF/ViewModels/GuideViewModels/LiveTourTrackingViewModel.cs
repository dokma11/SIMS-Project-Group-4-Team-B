using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class LiveTourTrackingViewModel : INotifyPropertyChanged
    {
        public Tour Tour { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourService _tourService;
        private TourReviewService _tourReviewService;
        private RequestService _requestService;
        private LocationService _locationService;
        private VoucherService _voucherService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private TourNotificationService _tourNotificationService;
        private ComplexTourRequestService _complexTourRequestService;
        private SubTourRequestService _subTourRequestService;

        public User LoggedInGuide { get; set; }
        public ObservableCollection<KeyPoint> KeyPointsToDisplay { get; set; }

        public User SelectedUser { get; set; }
        public int firstKeyPointId = -1;
        public int lastKeyPointId = -1;
        public int lastVisitedKeyPointId = -1;
        public bool LastKeyPointVisited;
        public List<User> MarkedGuests { get; set; }
        public ObservableCollection<User> GuestsToDisplay { get; set; }

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

        private string _labelContent;
        public string LabelContent
        {
            get { return _labelContent; }
            set
            {
                _labelContent = value;
                OnPropertyChanged(nameof(LabelContent));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand MarkKeyPointCommand { get; set; }
        public RelayCommand MarkGuestPresentCommand { get; set; }
        public RelayCommand CancelTourCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand HomePageNavigationCommand { get; set; }
        public RelayCommand AccountPageNavigationCommand { get; set; }
        public RelayCommand RequestsPageNavigationCommand { get; set; }
        public RelayCommand ReviewsPageNavigationCommand { get; set; }

        public LiveTourTrackingViewModel(Tour tour, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            MarkKeyPointCommand = new RelayCommand(Executed_MarkKeyPointCommand, CanExecute_MarkKeyPointCommand);
            MarkGuestPresentCommand = new RelayCommand(Executed_MarkGuestPresentCommand, CanExecute_MarkGuestPresentCommand);
            CancelTourCommand = new RelayCommand(Executed_CancelTourCommand, CanExecute_CancelTourCommand);
            GoBackCommand = new RelayCommand(Executed_GoBackCommand, CanExecute_GoBackCommand);
            HomePageNavigationCommand = new RelayCommand(Executed_HomePageNavigationCommand, CanExecute_HomePageNavigationCommand);
            RequestsPageNavigationCommand = new RelayCommand(Executed_RequestsPageNavigationCommand, CanExecute_RequestsPageNavigationCommand);
            ReviewsPageNavigationCommand = new RelayCommand(Executed_ReviewsPageNavigationCommand, CanExecute_ReviewsPageNavigationCommand);
            AccountPageNavigationCommand = new RelayCommand(Executed_AccountPageNavigationCommand, CanExecute_AccountPageNavigationCommand);

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
            _complexTourRequestService = complexTourRequestService;
            _subTourRequestService = subTourRequestService;

            LoggedInGuide = loggedInGuide;
            SelectedUser = new();

            Tour = tour;
            _tourService.UpdateState(Tour, ToursState.Started);

            MarkedGuests = new List<User>();
            KeyPointsToDisplay = new ObservableCollection<KeyPoint>(_keyPointService.GetByToursId(Tour.Id));

            firstKeyPointId = FindAndMarkFirstKeyPoint();
            SelectedKeyPoint = _keyPointService.GetById(firstKeyPointId);
            GuestsToDisplay = new ObservableCollection<User>(_tourReservationService.GetGuestsWithReservations(SelectedKeyPoint, MarkedGuests));

            lastVisitedKeyPointId = firstKeyPointId;
            lastKeyPointId = FindLastKeyPoint();
            LastKeyPointVisited = false;

            IsLabelVisible = false;
        }

        private int FindAndMarkFirstKeyPoint()
        {
            KeyPoint firstKeyPoint = new()
            {
                Id = int.MaxValue
            };

            firstKeyPoint = KeyPointsToDisplay.MinBy(keyPoint => keyPoint.Id);

            _keyPointService.ChangeKeyPointsState(firstKeyPoint, KeyPointsState.BeingVisited);

            return firstKeyPoint.Id;
        }

        private void MarkLastVisitedKeyPoint()
        {
            var keyPoint = KeyPointsToDisplay.FirstOrDefault(k => k.Id == lastVisitedKeyPointId);
            if (keyPoint != null)
            {
                _keyPointService.ChangeKeyPointsState(keyPoint, KeyPointsState.Visited);
            }
        }

        private int FindLastKeyPoint()
        {
            lastKeyPointId = KeyPointsToDisplay.Max(keyPoint => keyPoint.Id);
            return lastKeyPointId;
        }

        private void MarkLastKeyPoint()
        {
            KeyPointsToDisplay.Single(keyPoint => keyPoint.Id == lastKeyPointId).CurrentState = KeyPointsState.Visited;
        }

        public void AddMarkedGuests(User guest)
        {
            _keyPointService.AddGuestsId(SelectedKeyPoint, guest.Id);
            MarkedGuests.Add(guest);
            ShouldConfirmParticipation(guest);
            GuestsToDisplay.Remove(guest);
            _keyPointService.Save();
        }

        private void ShouldConfirmParticipation(User user)
        {
            foreach (var tourReservation in _tourReservationService.GetAll())
            {
                if (tourReservation.User.Id == user.Id && tourReservation.Tour.Id == SelectedKeyPoint.Tour.Id)
                {
                    tourReservation.ShouldConfirmParticipation = true;
                    _tourReservationService.Save();
                }
            }
        }

        public void UpdateKeyPointList()
        {
            KeyPointsToDisplay.Clear();
            foreach (var keyPoint in _keyPointService.GetAll())
            {
                if (keyPoint.Tour.Id == Tour.Id)
                {
                    KeyPointsToDisplay.Add(keyPoint);
                }
            }
            _keyPointService.Save();
        }

        private void Executed_MarkKeyPointCommand(object obj)
        {
            //mark previous key point as visited
            _keyPointService.ChangeKeyPointsState(KeyPointsToDisplay.First(k => k.Id == lastVisitedKeyPointId), KeyPointsState.Visited);

            _keyPointService.ChangeKeyPointsState(SelectedKeyPoint, KeyPointsState.BeingVisited);
            lastVisitedKeyPointId = SelectedKeyPoint.Id;

            if (SelectedKeyPoint.Id == lastKeyPointId)
            {
                UpdateKeyPointList();
                _tourService.UpdateState(Tour, ToursState.Finished);
                MarkLastKeyPoint();
                LastKeyPointVisited = true;
                SuccessfulLastKeyPointMarkingLabelEvent();
            }
            else
            {
                SuccessfulKeyPointMarkingLabelEvent();
            }

            UpdateKeyPointList();
        }

        private bool CanExecute_MarkKeyPointCommand(object obj)
        {
            return SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPointsState.NotVisited && SelectedKeyPoint.Id == lastVisitedKeyPointId + 1;
        }

        //Moram isto pokusati da dodam one uslove tipa, ne mozes na prethodnu ne mozes ovo ono smaranje...

        private void SuccessfulKeyPointMarkingLabelEvent()
        {
            LabelContent = "Uspešno ste označili trenutnu ključnu tačku!";
            IsLabelVisible = true;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void SuccessfulLastKeyPointMarkingLabelEvent()
        {
            LabelContent = "Uspešno ste završili turu!";
            IsLabelVisible = true;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick_Last_KeyPoint;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            IsLabelVisible = false;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        private void Timer_Tick_Last_KeyPoint(object sender, EventArgs e)
        {
            IsLabelVisible = false;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private void Executed_MarkGuestPresentCommand(object obj)
        {
            AddMarkedGuests(SelectedUser);
            UpdateKeyPointList();
            SuccessfulGuestMarkingLabelEvent();
        }

        private void SuccessfulGuestMarkingLabelEvent()
        {
            LabelContent = "Uspešno ste dodali sve izabrane goste na turu!";
            IsLabelVisible = true;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private bool CanExecute_MarkGuestPresentCommand(object obj)
        {
            return SelectedUser != null && SelectedKeyPoint != null && SelectedUser.Id != 0;
        }

        private void Executed_CancelTourCommand(object obj)
        {
            _tourService.UpdateState(Tour, ToursState.Interrupted);
            MarkLastVisitedKeyPoint();
            _keyPointService.Save();
            SuccessfulCancellationLabelEvent();
        }

        private bool CanExecute_CancelTourCommand(object obj)
        {
            return true;
        }

        private void SuccessfulCancellationLabelEvent()
        {
            LabelContent = "Uspešno ste prekinuli turu!";
            IsLabelVisible = true;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick_Cancel;
            timer.Start();
        }

        private void Timer_Tick_Cancel(object sender, EventArgs e)
        {
            IsLabelVisible = false;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private void Executed_GoBackCommand(object obj)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private bool CanExecute_GoBackCommand(object obj)
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

        private void Executed_RequestsPageNavigationCommand(object obj)
        {
            RequestsView requestsView = new(_requestService, _tourService, _locationService, _keyPointService, _tourReviewService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(requestsView);
        }

        private bool CanExecute_RequestsPageNavigationCommand(object obj)
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
