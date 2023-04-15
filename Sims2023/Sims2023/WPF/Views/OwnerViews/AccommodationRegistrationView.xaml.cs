using Sims2023.Application.Services;
using Sims2023.Controller;
using System.Windows;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using Microsoft.Win32;
using System.Reflection;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;
using Path = System.IO.Path;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for addAccommodationView.xaml
    /// </summary>
    public partial class AccommodationRegistrationView : Window
    {

        public AccommodationRegistrationViewModel AccommodationRegistrationViewModel;
        public AccommodationRegistrationView(AccommodationService accommodationCtrl1, User owner)
        {
            InitializeComponent();
            AccommodationRegistrationViewModel = new AccommodationRegistrationViewModel(this, accommodationCtrl1, owner);
            DataContext = AccommodationRegistrationViewModel;
           
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRegistrationViewModel.SaveButton_Click(sender, e);
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRegistrationViewModel.Registration_Click(sender, e);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
 
    }
}