using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationReschedulingView.xaml
    /// </summary>
    public partial class AccommodationReservationReschedulingView : Window, IObserver
    {
        AccommodationReservationReschedulingViewModel AccommodationReservationReschedulingViewModel;

        public AccommodationReservationReschedulingView(User guest1)
        {
            InitializeComponent();
            AccommodationReservationReschedulingViewModel = new AccommodationReservationReschedulingViewModel(this,guest1);
            DataContext = AccommodationReservationReschedulingViewModel;
        }

        private void newRequest_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.newRequest_Click(sender, e);
        }

        private void report_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.report_Click(sender, e);
        }

        private void comment_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationReschedulingViewModel.comment_Click(sender, e);
        }

        public void Update()
        {
            AccommodationReservationReschedulingViewModel.Update();
        }
    }
}
