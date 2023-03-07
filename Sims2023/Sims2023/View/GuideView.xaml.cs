using Sims2023.Model;
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
    public partial class GuideView : Window
    {
        ObservableCollection<Tour> tours = new ObservableCollection<Tour>();
        public Tour SelectedTour { get; set; }
        public GuideView()
        {
            InitializeComponent();
            DataContext = this;

            //tours = new ObservableCollection<Tour>(_tourController.GetAllTours());
        }

        private void CreateButtonClicked(object sender, RoutedEventArgs e)
        {
            //CreateTourView createTourView = new CreateTourView();
            //createTourView.Show();
        }
    }
}
