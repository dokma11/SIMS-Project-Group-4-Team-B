using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
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

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for WheneverWhereverOptionsView.xaml
    /// </summary>
    public partial class WheneverWhereverOptionsView : Page
    {
        private DateTime startDateSelected;
        private DateTime endDateSelected;
        private int stayLength;
        private int numberOfGuests;
        private User user;
        public WheneverWhereverOptionsViewModel WheneverWhereverOptionsViewModel;

        public WheneverWhereverOptionsView(DateTime startDateSelected, DateTime endDateSelected, int stayLength, int numberOfGuests, User user,Frame mainFrame)
        {
            InitializeComponent();
            this.DataContext = new WheneverWhereverOptionsViewModel(this, user, stayLength, numberOfGuests, startDateSelected, endDateSelected,mainFrame);
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void FindDates(object sender, ExecutedRoutedEventArgs e)
        {
            ((WheneverWhereverOptionsViewModel)this.DataContext).FindDates();
        }

        public void ConfirmReservation(object sender, ExecutedRoutedEventArgs e)
        {
            ((WheneverWhereverOptionsViewModel)this.DataContext).ConfirmReservation();
        }

        public void GoBack(object sender, ExecutedRoutedEventArgs e)
        {
            ((WheneverWhereverOptionsViewModel)this.DataContext).GoBack();
        }
        
        public void DetailedView(object sender, ExecutedRoutedEventArgs e)
        {
            ((WheneverWhereverOptionsViewModel)this.DataContext).DetailedView();
        }
    }
}
