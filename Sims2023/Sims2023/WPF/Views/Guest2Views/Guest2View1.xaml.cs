﻿using System;
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
    /// Interaction logic for Guest2View1.xaml
    /// </summary>
    public partial class Guest2View1 : Window
    {
        public Guest2View1(User user)
        {
            InitializeComponent();
            this.DataContext = new Guest2ViewModel1(this.frame.NavigationService,user);
        }
    }
}