using Sims2023.Controller;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for LiveTourTrackingView.xaml
    /// </summary>
    public partial class LiveTourTrackingView : Window
    {
        public Tour Tour { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }

        private KeyPointController _keyPointController;
        private UserController _userController;
        private TourReservationController _tourReservationController;
        public ObservableCollection<KeyPoint> KeyPointsToDisplay { get; set; }
        public ObservableCollection<KeyPoint> AllKeyPoints { get; set; }

        public int firstKeyPointId = -1;
        public int lastKeyPointId = -1;
        public int lastVisitedKeyPointId = -1;

        public List<User> MarkedGuests { get; set; }

        public LiveTourTrackingView(Tour tour, KeyPointController keyPointController, TourReservationController tourReservationController, UserController userController)
        {
            InitializeComponent();
            DataContext = this;

            Tour = tour;
            Tour.CurrentState = Tour.State.Started;

            _keyPointController = keyPointController;
            _tourReservationController = tourReservationController;
            _userController = userController;

            MarkedGuests = new List<User>();

            KeyPointsToDisplay = new ObservableCollection<KeyPoint>();
            AllKeyPoints = new ObservableCollection<KeyPoint>(_keyPointController.GetAllKeyPoints());
            foreach (var keyPoint in AllKeyPoints)
            {
                if (keyPoint.Tour.Id == Tour.Id)
                {
                    KeyPointsToDisplay.Add(keyPoint);
                }
            }

            firstKeyPointId = FindFirstKeyPoint();
            lastVisitedKeyPointId = firstKeyPointId;
            lastKeyPointId = FindLastKeyPoint();
        }

        private int FindFirstKeyPoint()
        {
            int counter = 0;
            foreach (var keyPoint in KeyPointsToDisplay)
            {
                if (counter == 0)
                {
                    firstKeyPointId = keyPoint.Id;
                    counter++;
                }
                else
                {
                    if (keyPoint.Id < firstKeyPointId)
                    {
                        firstKeyPointId = keyPoint.Id;
                    }
                }
            }
            MarkFirstKeyPoint();
            return firstKeyPointId;
        }

        private void MarkFirstKeyPoint()
        {
            foreach (var keyPoint in KeyPointsToDisplay)
            {
                if (keyPoint.Id == firstKeyPointId)
                {
                    keyPoint.CurrentState = KeyPoint.State.BeingVisited;
                }
            }
        }

        private void MarkLastVisitedKeyPoint()
        {
            foreach (var keyPoint in KeyPointsToDisplay)
            {
                if (keyPoint.Id == lastVisitedKeyPointId)
                {
                    keyPoint.CurrentState = KeyPoint.State.Visited;
                }
            }
        }

        private int FindLastKeyPoint()
        {
            foreach (var keyPoint in KeyPointsToDisplay)
            {
                if (keyPoint.Id > lastKeyPointId)
                {
                    lastKeyPointId = keyPoint.Id;
                }
            }
            return lastKeyPointId;
        }

        private void MarkLastKeyPoint()
        {
            foreach (var keyPoint in KeyPointsToDisplay)
            {
                if (keyPoint.Id == lastKeyPointId)
                {
                    keyPoint.CurrentState = KeyPoint.State.Visited;
                }
            }
        }

        private void MarkKeyPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.NotVisited && SelectedKeyPoint.Id == lastVisitedKeyPointId + 1) //the latter disables key point skipping
            {
                //mark previous key point as visited
                foreach (var keyPoint in KeyPointsToDisplay)
                {
                    if (keyPoint.Id == lastVisitedKeyPointId)
                    {
                        keyPoint.CurrentState = KeyPoint.State.Visited;
                    }
                }

                SelectedKeyPoint.CurrentState = KeyPoint.State.BeingVisited;
                lastVisitedKeyPointId = SelectedKeyPoint.Id;

                if (SelectedKeyPoint.Id == lastKeyPointId)
                {
                    Update();
                    Tour.CurrentState = Tour.State.Finished;
                    MarkLastKeyPoint();
                    ConfirmEnd();
                    Close();
                }

                Update();
            }
            else if (SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.BeingVisited)
            {
                MessageBox.Show("Ne mozete oznaciti tacku na kojoj se trenutno nalazite");
            }
            else if (SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.Visited)
            {
                MessageBox.Show("Ne mozete oznaciti tacku koju ste prosli");
            }
            else
            {
                MessageBox.Show("Izaberite kljucnu tacku koju zelite da oznacite");
            }
        }

        private void MarkGuestsPresentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.BeingVisited)
            {
                MarkGuestsPresentView markGuestsPresentView = new(SelectedKeyPoint, _tourReservationController, _userController, MarkedGuests);
                markGuestsPresentView.Closed += MarkGuestsPresentView_Closed;
                markGuestsPresentView.Show();
                _keyPointController.Save();
            }
            else
            {
                MessageBox.Show("Molimo odaberite kljucnu tacku za koju zelite da obelezite goste koji su se prikljucili");
            }
        }

        private void MarkGuestsPresentView_Closed(object sender, EventArgs e)
        {
            Update();
            int counter = 0;
            foreach (var tour in _tourReservationController.GetAllReservations())
            {
                if (tour.TourId == Tour.Id) counter++;
            }
            if (counter == MarkedGuests.Count)
            {
                markGuestsPresentButton.IsEnabled = false;
            }
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.CurrentState = Tour.State.Cancelled;
            MessageBoxResult result = ConfirmExit();
            if (result == MessageBoxResult.Yes)
            {
                Tour.CurrentState = Tour.State.Cancelled;
                MarkLastVisitedKeyPoint();
                Close();
            }
        }

        private static MessageBoxResult ConfirmExit()
        {
            string sMessageBoxText = $"Izlaskom cete prekinuti trenutnu turu\n";
            string sCaption = "Da li ste sigurni da zelite da izadjete?";

            MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;
            MessageBoxImage messageBoxImage = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, messageBoxButton, messageBoxImage);
            return result;
        }

        private static MessageBoxResult ConfirmEnd()
        {
            string sMessageBoxText = $"Vasa tura se uspesno zavrsila. Potvrdite zavrsetak pritiskom na OK\n";
            string sCaption = "Potvrda zavrsetka";

            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
            MessageBoxImage messageBoxImage = MessageBoxImage.Asterisk;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, messageBoxButton, messageBoxImage);
            return result;
        }

        public void Update()
        {
            UpdateKeyPointList();
        }

        public void UpdateKeyPointList()
        {
            KeyPointsToDisplay.Clear();
            AllKeyPoints.Clear();
            foreach (var keyPoint in _keyPointController.GetAllKeyPoints())
            {
                AllKeyPoints.Add(keyPoint);
                if (keyPoint.Tour.Id == Tour.Id)
                {
                    KeyPointsToDisplay.Add(keyPoint);
                }
            }
        }
    }
}
