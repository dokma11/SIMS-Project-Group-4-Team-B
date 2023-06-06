using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for OpenedForumView.xaml
    /// </summary>
    public partial class OpenedForumView : Page
    {
        public OpenedForumView(ForumService forumService, Frame mainFrame, User user, Forum selectedForum)
        {
            InitializeComponent();
            this.DataContext = new OpenedForumViewModel(user,mainFrame,selectedForum,this,forumService);
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void AddComment(object sender, RoutedEventArgs e)
        {
            ((OpenedForumViewModel)this.DataContext).AddComment();
        }
        public void ShutDown(object sender, RoutedEventArgs e)
        {
            ((OpenedForumViewModel)this.DataContext).ShutDown();
        }
        public void GoBack(object sender, RoutedEventArgs e)
        {
            ((OpenedForumViewModel)this.DataContext).GoBack();
        }
    }
}
