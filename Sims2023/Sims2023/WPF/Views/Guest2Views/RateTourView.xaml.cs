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
using System.Windows.Shapes;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
   
    public partial class RateTourView : Window,IObserver
    {
        public User User { get; set; }
        public Tour Tour { get; set; }
        RateTourViewModel RateTourViewModel { get; set; }

        
        public RateTourView(User user,Tour tour)
        {
            InitializeComponent();
           
            Tour = tour;
            User = user;
            RateTourViewModel= new RateTourViewModel(user,tour,this);

            

            DataContext = RateTourViewModel;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            RateTourViewModel.Send_Click();
        }

        private void AddPicture_Click(object sender,RoutedEventArgs e)
        {
            RateTourViewModel.AddPicture_Click();
        }

        public void Update()
        {
            RateTourViewModel.Update();
        }
    }
}
