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
using System.Windows.Shapes;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2SubToursRequestListView.xaml
    /// </summary>
    public partial class Guest2SubToursRequestListView : Window
    {
        public User User { get; set; }
        public ComplexTourRequest ComplexTourRequest { get; set; }
        public Guest2SubToursRequestListViewModel Guest2SubToursRequestListViewModel { get; set; }
        public Guest2SubToursRequestListView(ComplexTourRequest complexTourRequest,User user)
        {
            InitializeComponent();
            User=user;
            ComplexTourRequest=complexTourRequest;
            Guest2SubToursRequestListViewModel = new Guest2SubToursRequestListViewModel(complexTourRequest,user);
            DataContext = Guest2SubToursRequestListViewModel;

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewSubtour_Click(object sender, RoutedEventArgs e)
        {
            CreateSubTourRequestView createSubTourRequestView = new CreateSubTourRequestView(User,ComplexTourRequest);
            createSubTourRequestView.Show();

        }
    }
}
