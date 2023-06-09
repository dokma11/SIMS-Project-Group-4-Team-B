using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
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
using ToastNotifications.Position;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for CommentsView.xaml
    /// </summary>
    public partial class CommentsView : Page
    {
        public ForumService _forumService;
        public ForumCommentService _commentService;

        public User owner { get; set; }
        public Forum SelectedForum { get; set; }
        public string welcomeString { get; set; }
        public ObservableCollection<ForumComment> ForumComment { get; set; }
        public CommentsView(User user, Forum selectedForum)
        {
            InitializeComponent();
            DataContext= this;
            welcomeString = "Tema: " + selectedForum.Theme;
            SelectedForum = selectedForum;
            owner = user;
            _forumService = new ForumService();
            _commentService = new ForumCommentService();
            ForumComment = new ObservableCollection<ForumComment>();
            ForumComment.Add(new ForumComment { Id = 100, Forum = selectedForum, Comment = selectedForum.MainComment, Special = false, User = selectedForum.User });
            var filteredComments = _commentService.FilterComments(ForumComment, selectedForum);
            var commentsToAdd = new List<ForumComment>(filteredComments);

            foreach (var comment in commentsToAdd)
            {
                if (!ForumComment.Contains(comment)) 
                {
                    ForumComment.Add(comment);
                }
            }

            

        }

        private void Comment_Click(object sender, RoutedEventArgs e)
        {
            LeaveCommentView comments = new LeaveCommentView(SelectedForum, owner, ForumComment);
            FrameManager.Instance.MainFrame.Navigate(comments);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }
    }
}
