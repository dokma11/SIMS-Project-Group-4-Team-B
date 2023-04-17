using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;

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
            AllGuestOneReservationsViewModel.grading_Click(sender, e);
        }

        public void Update()
        {
            AllGuestOneReservationsViewModel.Update();
        }

        private void renovation_Click(object sender, RoutedEventArgs e)
        {
            AllGuestOneReservationsViewModel.renovation_Click(sender, e);
        }
    }
}
