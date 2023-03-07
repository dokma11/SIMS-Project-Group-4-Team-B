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
        private TourController _controller;
        public Tour Tour { get; set; }
        public Tour SelectedTour { get; set; }
        public ObservableCollection<Tour> tours { get; set; }
        public GuideView()
        {
            InitializeComponent();
            DataContext = this;

            _controller = new TourController();
            _controller.Subscribe(this);
            tours = new ObservableCollection<Tour>(_controller.GetAllTours());
        }

        private void CreateButtonClicked(object sender, RoutedEventArgs e)
        {
            CreateTourView createTourView = new CreateTourView(_controller);
            createTourView.Show();
        }

        public void Update()
        {
            UpdateTourList();
        }

        public void UpdateTourList()
        {
            tours.Clear();
            foreach(var tour in _controller.GetAllTours()) 
            {
                tours.Add(tour);
            }
        }
    }
}
