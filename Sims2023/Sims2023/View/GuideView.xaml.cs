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
        public ObservableCollection<Tour> tours { get; set; }
        public GuideView()
        {
            InitializeComponent();
            DataContext = this;

            _tourController = new TourController();
            _tourController.Subscribe(this);
            tours = new ObservableCollection<Tour>(_tourController.GetAllTours());

            _locationController = new LocationController();
            _locationController.Subscribe(this);

            _keyPointController = new KeyPointController();
            _keyPointController.Subscribe(this);
        }

        private void CreateButtonClicked(object sender, RoutedEventArgs e)
        {
            CreateTourView createTourView = new CreateTourView(_tourController, _locationController, _keyPointController);
            createTourView.Show();  
        }

        public void Update()
        {
            UpdateTourList();
        }

        public void UpdateTourList()
        {
            tours.Clear();
            foreach(var tour in _tourController.GetAllTours()) 
            {
                tours.Add(tour);
            }
        }
    }
}
