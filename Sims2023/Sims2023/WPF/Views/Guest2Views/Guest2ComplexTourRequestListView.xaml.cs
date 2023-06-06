using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2ComplexTourRequestListView.xaml
    /// </summary>
    public partial class Guest2ComplexTourRequestListView : Page
    {
        public User User { get; set; }
        public ComplexTourRequest SelectedComplexTourRequest { get; set; }
        public Guest2ComplexTourRequestListViewModel Guest2ComplexTourRequestListViewModel { get; set; }
        public Guest2ComplexTourRequestListView(User user)
        {
            InitializeComponent();
            Guest2ComplexTourRequestListViewModel = new Guest2ComplexTourRequestListViewModel(user);

            DataContext = Guest2ComplexTourRequestListViewModel;
            dataGridGuestComplexTourRequests.ItemsSource = Guest2ComplexTourRequestListViewModel.ComplexTourRequests;
            User = user;
        }

        private void NewRequest_Click(object sender, RoutedEventArgs e)
        {
             CreateComplexTourRequestView createComplexTourRequestView = new CreateComplexTourRequestView(User);
             createComplexTourRequestView.Closed += CreateComplexTourRequestView_Closed;

             createComplexTourRequestView.Show();
        }

        private void CreateComplexTourRequestView_Closed(object sender, EventArgs e)
        {
            // Refresh the data grid
            Guest2ComplexTourRequestListViewModel = new Guest2ComplexTourRequestListViewModel(User);

            DataContext = Guest2ComplexTourRequestListViewModel;
            dataGridGuestComplexTourRequests.ItemsSource = Guest2ComplexTourRequestListViewModel.ComplexTourRequests;

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (Guest2ComplexTourRequestListViewModel.RateTour_Click())
            {
                SelectedComplexTourRequest = Guest2ComplexTourRequestListViewModel.SelectedComplexTourRequest;
                // Guest2SubToursRequestListView guest2SubToursRequestListView = new Guest2SubToursRequestListView(SelectedComplexTourRequest);
                //guest2SubToursRequestListView.Show();
                MessageBox.Show(SelectedComplexTourRequest.ToString());
                MessageBox.Show($"Name: {SelectedComplexTourRequest.Name}\n");
            }
            else
            {
                MessageBox.Show("Izaberite turu");
            }
        }
    }
}

