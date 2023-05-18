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
            ViewModel = new Guest2TourRequestStatisticsViewModel(user);
            DataContext = ViewModel;

            //languageComboBox.ItemsSource = ViewModel.GetLanguages();
            //locationComboBox.ItemsSource = ViewModel.GetLocations();
            languageYearComboBox.ItemsSource = ViewModel.GetYears();
            locationYearComboBox.ItemsSource = ViewModel.GetYears();
            yearComboBox.ItemsSource= ViewModel.GetYears();

            


        }

        private void LocationComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (locationYearComboBox.SelectedItem != null)
            {
                ViewModel.DisplayLocationStatistics( locationYearComboBox.SelectedItem.ToString());
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (languageYearComboBox.SelectedItem != null)
            {
                ViewModel.DisplayLanguageStatistics( languageYearComboBox.SelectedItem.ToString());
            }
        }

        private void YearComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (yearComboBox.SelectedItem != null)
            {
                ViewModel.DisplayTourRequestStatistics(yearComboBox.SelectedItem.ToString());
            }
        }




    }
}
