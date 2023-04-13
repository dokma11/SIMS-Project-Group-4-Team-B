using Microsoft.Win32;
using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Model.DAO;
using Sims2023.Observer;
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
using Sims2023.WPF.ViewModels;
using Sims2023.WPF.ViewModels.Guest1ViewModel;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerGradingView.xaml
    /// </summary>
    public partial class AccommodationAndOwnerGradingView : Window
    {
        public AccommodationAndOwnerGradingViewModel AccommodationAndOwnerGradingViewModel;
        public AccommodationAndOwnerGradingView(AccommodationReservation SelectedAccommodationReservationn, User guest1, AccommodationReservationService accommodationReservationController)
        {
            InitializeComponent();
            AccommodationAndOwnerGradingViewModel = new AccommodationAndOwnerGradingViewModel(this,SelectedAccommodationReservationn,guest1,accommodationReservationController);
            DataContext = AccommodationAndOwnerGradingViewModel;

        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            AccommodationAndOwnerGradingViewModel.accept_Click(sender, e);
        }

        public void giveUp_Click(object sender, RoutedEventArgs e)
        {
            AccommodationAndOwnerGradingViewModel.giveUp_Click(sender, e);
        }

        public void addPicture_Click(object sender, RoutedEventArgs e)
        {
            AccommodationAndOwnerGradingViewModel.addPicture_Click(sender, e);
        }

        public void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationAndOwnerGradingViewModel.RemoveButton_Click(sender, e);
        }

    }
}
