using System;
using System.Collections.Generic;
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
using LiveCharts;
using LiveCharts.Wpf;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2TourRequestStatisticsView.xaml
    /// </summary>
    public partial class Guest2TourRequestStatisticsView :Window
    {
        public Guest2TourRequestStatisticsViewModel ViewModel { get; set; }
        public Guest2TourRequestStatisticsView(User user)
        {
            InitializeComponent();
            YearComboBox.Text = "All time";
            ViewModel = new Guest2TourRequestStatisticsViewModel(user,YearComboBox.Text); 
            DataContext=ViewModel;

            
        }

        


    }
}
