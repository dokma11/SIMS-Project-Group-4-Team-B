using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
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

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for NewAccommodationReservationReschedulingRequest.xaml
    /// </summary>
    public partial class NewAccommodationReservationReschedulingRequestView : Window, IObserver
    {
        NewAccommodationReservationReschedulingRequestViewModel NewAccommodationReservationReschedulingRequestViewModel;
        public NewAccommodationReservationReschedulingRequestView(User guest1)
        {
            InitializeComponent();
            NewAccommodationReservationReschedulingRequestViewModel = new NewAccommodationReservationReschedulingRequestViewModel(this, guest1);
            DataContext = NewAccommodationReservationReschedulingRequestViewModel;
        }

        private void makeRequest_Click(object sender, RoutedEventArgs e)
        {
            NewAccommodationReservationReschedulingRequestViewModel.makeRequest_Click(sender, e);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NewAccommodationReservationReschedulingRequestViewModel.back_Click(sender, e);
        }
        public void Update()
        {
            NewAccommodationReservationReschedulingRequestViewModel.Update();
        }
    }
}
