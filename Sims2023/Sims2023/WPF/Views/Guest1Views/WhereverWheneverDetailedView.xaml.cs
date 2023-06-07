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
    /// Interaction logic for WhereverWheneverDetailedView.xaml
    /// </summary>
    public partial class WhereverWheneverDetailedView : Page
    {
        public WhereverWheneverDetailedView(Accommodation selectedAccommodation, int daysNumber, int guestsNumber, User user, DateTime startDateSelected, DateTime endDateSelected, Frame mainFrame)
        {
            InitializeComponent();
            this.DataContext = new WhereverWheneverDetailedViewModel(selectedAccommodation, daysNumber, guestsNumber, user, startDateSelected, endDateSelected, mainFrame,this);
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void MakeReservation(object sender, ExecutedRoutedEventArgs e)
        {
            ((WhereverWheneverDetailedViewModel)this.DataContext).MakeReservation();
        }

        public void GoBack(object sender, ExecutedRoutedEventArgs e)
        {
            ((WhereverWheneverDetailedViewModel)this.DataContext).GoBack();
        }
        
}
}
