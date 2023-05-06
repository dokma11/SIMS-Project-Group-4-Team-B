using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for GuestOneReviewsView.xaml
    /// </summary>
    public partial class GuestOneReviewsView : Page
    {
        GuestOneReviewsViewModel GuestOneReviewsViewModel;
        public GuestOneReviewsView(User guest)
        {
            InitializeComponent();
            GuestOneReviewsViewModel = new GuestOneReviewsViewModel(this, guest);
            DataContext = GuestOneReviewsViewModel;
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void ShowComment(object sender, ExecutedRoutedEventArgs e)
        {
            if (GuestOneReviewsViewModel.ShowComment())
            {
                NavigationService navigationService = NavigationService.GetNavigationService(this);

                if (navigationService.CanGoBack)
                {
                    navigationService.GoBack();
                }
            }
        }
    }
}
