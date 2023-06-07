using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MakeNewForumView.xaml
    /// </summary>
    public partial class MakeNewForumView : Page
    {
        public MakeNewForumView(Location location, ObservableCollection<Forum> filteredForums, ForumService forumService, Frame mainFrame, User user)
        {
            InitializeComponent();
            this.DataContext = new MakeNewForumViewModel(location, filteredForums, forumService, mainFrame, user,this);
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void NewForum(object sender, RoutedEventArgs e)
        {
            ((MakeNewForumViewModel)this.DataContext).NewForum();
        }
        public void GoBack(object sender, RoutedEventArgs e)
        {
            ((MakeNewForumViewModel)this.DataContext).GoBack();
        }
    }
}
