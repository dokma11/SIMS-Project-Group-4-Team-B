using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CreateTourView.xaml
    /// </summary>
    public partial class CreateTourView : Window
    {
        private TourController _controller;
        public Tour Tour { get; set; }
        public CreateTourView(TourController controller)
        {
            InitializeComponent();
            DataContext = this;
            Tour = new Tour();

            _controller = controller;
        }
        private void SubmitButtonClicked(object sender, RoutedEventArgs e)
        {
            _controller.Create(Tour);
            Close();
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
