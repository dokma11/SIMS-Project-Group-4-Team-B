using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
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

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for GuideView.xaml
    /// </summary>
    public partial class GuideView : Window, IObserver 
    {
        private TourController _tourController;
        private LocationController _locationController;
        private KeyPointController _keyPointController;
        public Tour Tour { get; set; }
        public Tour SelectedTour { get; set; }
        public ObservableCollection<Tour> ToursToShow { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }

        public GuideView()
        {
            InitializeComponent();
            DataContext = this;

            _locationController = new LocationController();
            _locationController.Subscribe(this);

            _keyPointController = new KeyPointController();
            _keyPointController.Subscribe(this);

            _tourController = new TourController();
            _tourController.Subscribe(this);

            ToursToShow = new ObservableCollection<Tour>();
            AllTours = new ObservableCollection<Tour>(_tourController.GetAllTours());
            foreach(Tour tour in AllTours)
            {
                if(tour.Start == DateTime.Today)
                {
                    ToursToShow.Add(tour);
                }
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourView createTourView = new(_tourController, _locationController, _keyPointController);
            createTourView.Show();  
        }

        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour != null && SelectedTour.CurrentState == Tour.State.Created)
            {
                startTourButton.IsEnabled = false;
                LiveTourTrackingView liveTourTrackingView = new(SelectedTour, _keyPointController);
                liveTourTrackingView.Closed += LiveTourTrackingView_Closed;
                liveTourTrackingView.Show();
                Update();
            }
            else if(SelectedTour != null && SelectedTour.CurrentState != Tour.State.Created)
            {
                MessageBox.Show("Ne mozete zapoceti turu koja je ranije zapoceta");
            }
            else
            {
                MessageBox.Show("Odaberite turu koju zelite da zapocnete");
            }
        }
        private void LiveTourTrackingView_Closed(object sender, EventArgs e)
        {
            startTourButton.IsEnabled = true;
            Update();
            _tourController.Save();
            _keyPointController.Save();
        }

        public void Update()
        {
            UpdateTourList();
        }

        public void UpdateTourList()
        {
            ToursToShow.Clear();
            AllTours.Clear();
            foreach(var tour in _tourController.GetAllTours()) 
            {
                AllTours.Add(tour);
                if(tour.Start == DateTime.Today)
                {
                    ToursToShow.Add(tour);
                }
            }
        }
    }
}
