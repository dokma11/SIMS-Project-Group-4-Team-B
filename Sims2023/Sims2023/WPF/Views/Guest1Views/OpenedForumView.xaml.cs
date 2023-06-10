using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest1ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for OpenedForumView.xaml
    /// </summary>
    public partial class OpenedForumView : Page
    {
        public OpenedForumView(ForumService forumService, Frame mainFrame, User user, Forum selectedForum, ForumCommentService forumCommentService)
        {
            InitializeComponent();
            this.DataContext = new OpenedForumViewModel(user, mainFrame, selectedForum, this, forumService, forumCommentService);
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
