﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for AllGuestsView.xaml
    /// </summary>
    public partial class AllGuestsView : Window
    {

        public AllGuestsViewModel allGuestsViewModel;


        public AllGuestsView(User use, List<AccommodationReservation> reserv)
        {
            InitializeComponent();
            allGuestsViewModel = new AllGuestsViewModel(this,use, reserv);
            DataContext = allGuestsViewModel;       
        }
        private void Grade_Click(object sender, EventArgs e)
        {
         
            if (allGuestsViewModel.SelectedGuest != null)
            {
                var gradeView = new Guest1GradeView(allGuestsViewModel.SelectedGuest, allGuestsViewModel.Reservatons);
                gradeView.Show();
            }
        }
    }
}
