using Microsoft.VisualBasic;
using Sims2023.Application.Services;
using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.View;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Sims2023.WPF.ViewModels.Guest1ViewModel;

namespace Sims2023.WPF.Views.Guest1Views
{
    public partial class AccommodationListView : Window
    {
        public AccommodationListViewModel AccommodationListViewModel;
        public AccommodationListView(User guest1)
        {
            InitializeComponent();
            AccommodationListViewModel = new AccommodationListViewModel(this,guest1);
            DataContext = AccommodationListViewModel;
        }

        private void SearchAccommodation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationListViewModel.SearchAccommodation_Click(sender, e);
        }

        private void GiveUpSearch_Click(object sender, RoutedEventArgs e)
        {
            AccommodationListViewModel.GiveUpSearch_Click(sender, e);
        }

        private void ButtonReservation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationListViewModel.ButtonReservation_Click(sender, e);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AccommodationListViewModel.Back_Click(sender, e);
        }
    }
}
