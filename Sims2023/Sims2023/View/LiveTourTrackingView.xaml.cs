using Sims2023.Controller;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<KeyPoint> KeyPointsToShow { get; set; }
        public ObservableCollection<KeyPoint> AllKeyPoints { get; set; }

        public int firstKeyPointId = -1;
        
        public int lastKeyPointId = -1;

        public int lastVisitedKeyPointId = -1;
        public LiveTourTrackingView(Tour tour, KeyPointController keyPointController)
        {
            InitializeComponent();
            DataContext = this;

            Tour = tour;
            Tour.CurrentState = Tour.State.Started;

            _keyPointController = keyPointController;

            KeyPointsToShow = new ObservableCollection<KeyPoint>();
            AllKeyPoints = new ObservableCollection<KeyPoint>(_keyPointController.GetAllKeyPoints());
            foreach(var keyPoint in AllKeyPoints)
            {
                if(keyPoint.ToursId == Tour.Id)
                {
                    KeyPointsToShow.Add(keyPoint);
                }
            }
            //Finding the start of the tour (the first KeyPoint)
            int counter = 0;
            foreach(var keyPoint in KeyPointsToShow)
            {
                if(counter==0)
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

            foreach(var keyPoint in KeyPointsToShow)
            {
                if(keyPoint.Id == firstKeyPointId)
                {
                    keyPoint.CurrentState = KeyPoint.State.BeingVisited;
                    lastVisitedKeyPointId = keyPoint.Id;
                }
            }
            //Finding the end of the tour (the last KeyPoint)
            foreach (var keyPoint in KeyPointsToShow)
            {
                if (keyPoint.Id > lastKeyPointId)
                {
                    lastKeyPointId = keyPoint.Id;
                }
            }
        }

        private void MarkKeyPointButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.NotVisited && SelectedKeyPoint.Id == lastVisitedKeyPointId+1)
            {
                foreach (var keyPoint in KeyPointsToShow)
                {
                    if (keyPoint.CurrentState == KeyPoint.State.BeingVisited)
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
                    ConfirmEnd();
                    Close();
                }

                Update();
            }
            else if(SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.BeingVisited)
            {
                MessageBox.Show("Ne mozete oznaciti tacku na kojoj se trenutno nalazite");
            }
            else if(SelectedKeyPoint != null && SelectedKeyPoint.CurrentState == KeyPoint.State.Visited)
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
                MarkGuestsPresentView markGuestsPresentView = new(SelectedKeyPoint);
                markGuestsPresentView.Show();
            }
            else
            {
                MessageBox.Show("Molimo odaberite kljucnu tacku za koju zelite da obelezite goste koji su se prikljucili");
            }
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.CurrentState = Tour.State.Cancelled;
            MessageBoxResult result = ConfirmExit();
            if (result == MessageBoxResult.Yes)
            {
                Tour.CurrentState = Tour.State.Cancelled;
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
            KeyPointsToShow.Clear();
            AllKeyPoints.Clear();
            foreach(var keyPoint in _keyPointController.GetAllKeyPoints())
            {
                AllKeyPoints.Add(keyPoint);
                if(keyPoint.ToursId == Tour.Id)
                {
                    KeyPointsToShow.Add(keyPoint);
                }
            }
        }
    }
}
