using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.View;
using Sims2023.WPF.ViewModels.Guest1ViewModel;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for AllGuestOneReservationsView.xaml
    /// </summary>
    public partial class AllGuestOneReservationsView : Window, IObserver
    {
        AllGuestOneReservationsViewModel AllGuestOneReservationsViewModel;
        public AllGuestOneReservationsView(User guest1)
        {
            InitializeComponent();
            AllGuestOneReservationsViewModel = new AllGuestOneReservationsViewModel(this, guest1);
            DataContext = AllGuestOneReservationsViewModel;
        }

        private void grading_Click(object sender, RoutedEventArgs e)
        {
            AllGuestOneReservationsViewModel.grading_Click(sender ,e);
        }

        public void Update()
        {
            AllGuestOneReservationsViewModel.Update();
        }

        private void renovation_Click(object sender, RoutedEventArgs e)
        {
            AllGuestOneReservationsViewModel.renovation_Click(sender ,e);   
        }
    }
}
