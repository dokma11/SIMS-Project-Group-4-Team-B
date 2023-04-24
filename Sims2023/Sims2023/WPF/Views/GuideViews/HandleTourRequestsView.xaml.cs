using Sims2023.Application.Services;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for HandleTourRequestsView.xaml
    /// </summary>
    public partial class HandleTourRequestsView : Window
    {
        public HandleTourRequestsViewModel HandleTourRequestsViewModel;
        public HandleTourRequestsView(RequestService requestService)
        {
            InitializeComponent();

            HandleTourRequestsViewModel = new(requestService);
            DataContext = HandleTourRequestsViewModel;
        }

        public void FilterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
