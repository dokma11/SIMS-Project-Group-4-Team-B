using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
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
    /// Interaction logic for LeaveCommentView.xaml
    /// </summary>
    public partial class LeaveCommentView : Page
    {
        public Forum SelectedForum { get; set; }
        public string welcomeString { get; set; }

        public ForumCommentService _forumCommentService;
        public ForumService _forumService;
        public User Owner { get; set; }
        public ObservableCollection<ForumComment> ForumComment { get; set; }

        public LeaveCommentView(Forum selectedForum, User user, ObservableCollection<ForumComment> forumComment)
        {
            InitializeComponent();
            DataContext = this;
            SelectedForum = selectedForum;
            Owner = user;
            ForumComment = new ObservableCollection<ForumComment>(forumComment);
            _forumService = new ForumService();
            _forumCommentService = new ForumCommentService();
            welcomeString = "Ostavite komentar na temu:\n    " + selectedForum.Theme;
            ForumComment = forumComment;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }

        private void Comment_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MyTextBox.Text))
            {
                string comment = MyTextBox.Text;
                ForumComment newComment = new ForumComment();
                newComment.Forum = SelectedForum;
                newComment.User = Owner;
                newComment.Comment = comment;
                newComment.Special = true;
                _forumCommentService.Create(newComment);
                SelectedForum.CountOwnerComments++;
                _forumService.Update(SelectedForum);
                ToastNotificationService.ShowInformation("Uspiješno ostavljen komentar");
                ForumComment.Add(newComment);
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                navigationService?.GoBack();
            }
            else ToastNotificationService.ShowInformation("Ne možete ostaviti prazan komentar");
        }
    }
}
