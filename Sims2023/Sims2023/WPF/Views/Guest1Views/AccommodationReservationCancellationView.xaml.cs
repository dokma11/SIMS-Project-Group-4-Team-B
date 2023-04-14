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
using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.Domain.Models;
using Sims2023.Application.Services;
using Sims2023.WPF.ViewModels.Guest1ViewModel;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationReservationCancellationView.xaml
    /// </summary>
    public partial class AccommodationReservationCancellationView : Window, IObserver
    {
        AccommodationReservationCancellationViewModel AccommodationReservationCancellationViewModel;
        public AccommodationReservationCancellationView(User guest1)
        {
            InitializeComponent();
            AccommodationReservationCancellationViewModel = new AccommodationReservationCancellationViewModel(this, guest1);
            DataContext = AccommodationReservationCancellationViewModel;
        }

        private void cancellation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationReservationCancellationViewModel.cancellation_Click(sender, e);
        }

        public void Update()
        {
            AccommodationReservationCancellationViewModel.Update();
        }
    }
}
