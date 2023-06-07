﻿using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
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

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for UnopularLocationsView.xaml
    /// </summary>
    public partial class UnopularLocationsView : Page
    {
        public UnopularLocationsViewModel unopularLocationsViewModel;
        public UnopularLocationsView(User owenr)
        {

            unopularLocationsViewModel = new UnopularLocationsViewModel(owenr);
            DataContext = unopularLocationsViewModel;
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }

        private void DeleteAccommodations_Click(object sender, RoutedEventArgs e)
        {
            unopularLocationsViewModel.DeleteAccommodations_Click(sender, e);
        }
    }
}