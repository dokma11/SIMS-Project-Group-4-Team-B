using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Lifetime;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class ForumCommentViewModel
    {
        private User User;
        private ForumCommentView ForumCommentView;
        private Forum SelectedForum;
        private ForumCommentService _forumCommentService;

        public ForumCommentViewModel(User user, ForumCommentView forumCommentView, ForumCommentService commentService,Forum selectedForum)
        {
            User = user;
            SelectedForum=selectedForum;
            ForumCommentView = forumCommentView;
            _forumCommentService = commentService;
        }

        internal void AddComment()
        {
            String comment=ForumCommentView.CommentBox.Text;
            if (string.IsNullOrEmpty(comment))
            {
                MessageBox.Show("Molimo Vas unesite komentar prvo.");
                return;
            }
            ForumComment newComment=new ForumComment();
            newComment.Forum = SelectedForum;
            newComment.User = User;
            newComment.Comment = comment;
            _forumCommentService.Create(newComment);
            ForumCommentView.Close();
        }

        internal void GoBack()
        {
            ForumCommentView.Close();
        }
    }
}
