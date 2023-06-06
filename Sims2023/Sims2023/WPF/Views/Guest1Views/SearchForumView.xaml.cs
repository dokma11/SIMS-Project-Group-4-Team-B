using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for SearchForumView.xaml
    /// </summary>
    public partial class SearchForumView : Page
    {
        public SearchForumView(User user, Frame mainFrame)
        {
            InitializeComponent();
            this.DataContext = new SearchForumViewModel(user, mainFrame, this);
        }
        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void SearchForum(object sender, RoutedEventArgs e)
        {
            ((SearchForumViewModel)this.DataContext).SearchForum();
        }
        public void NewForum(object sender, RoutedEventArgs e)
        {
            ((SearchForumViewModel)this.DataContext).NewForum();
        }
        public void ShowForum(object sender, RoutedEventArgs e)
        {
            ((SearchForumViewModel)this.DataContext).ShowForum();
        }
        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((SearchForumViewModel)this.DataContext).CountryComboBox_SelectionChanged();
        }
    }
}
