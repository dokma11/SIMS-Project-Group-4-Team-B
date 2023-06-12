using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for WheneverWhereverOptionsView.xaml
    /// </summary>
    public partial class WheneverWhereverOptionsView : Page
    {
        public WheneverWhereverOptionsViewModel WheneverWhereverOptionsViewModel;

        public WheneverWhereverOptionsView(DateTime startDateSelected, DateTime endDateSelected, int stayLength, int numberOfGuests, User user, Frame mainFrame)
        {
            InitializeComponent();
            this.DataContext = new WheneverWhereverOptionsViewModel(this, user, stayLength, numberOfGuests, startDateSelected, endDateSelected, mainFrame);
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
        private void DataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ((WheneverWhereverOptionsViewModel)this.DataContext).SelectingFirstDataGrid(sender,e);
        }

        private void DataGrid2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ((WheneverWhereverOptionsViewModel)this.DataContext).SelectingSecondDataGrid(sender, e);
        }

    }
}
