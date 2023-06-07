using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class OpenedForumViewModel : IObserver
    {
        private User User;
        private Frame MainFrame;
        private Forum SelectedForum;
        OpenedForumView OpenedForumView;
        ObservableCollection<ForumComment> Comments = new();
        private ForumCommentService _forumCommentService;
        private ForumService _forumService;

        public OpenedForumViewModel(User user, Frame mainFrame, Forum selectedForum, OpenedForumView openedForumView, ForumService forumService)
        {
            User = user;
            MainFrame = mainFrame;
            SelectedForum = selectedForum;
            OpenedForumView = openedForumView;
            _forumCommentService = new ForumCommentService();
            _forumCommentService.Subscribe(this);
            _forumService = forumService;
            CheckIfUserIsCreator();
            CheckTheForum();
            FillTheTexts();

        }

        private void CheckTheForum()
        {
           //ovde treba da proverim da li je mark kao special ako jeste da bude oznacen
            if (SelectedForum.Closed)
            {
                OpenedForumView.CloseTheForum.Visibility = Visibility.Collapsed;
                OpenedForumView.Label1.Visibility = Visibility.Visible;
                OpenedForumView.postForum.IsEnabled = false;
            }
        }

        private void CheckIfUserIsCreator()
        {
            if (SelectedForum.User.Id == User.Id)
            {
                OpenedForumView.CloseTheForum.Visibility = Visibility.Visible;
            }
        }

        internal void AddComment()
        {
            if (SelectedForum.Closed == false)
            {
                var newWindow = new ForumCommentView(User, _forumCommentService, SelectedForum);
                newWindow.Show();
                Update();
            }
            else
            {
                MessageBox.Show("Forum je ugasen. Moguc je samo pregled ovog foruma ne i ostavljanje komentara.");
            }

        }

        public void GoBack()
        {
            NavigationService navigationService = NavigationService.GetNavigationService(OpenedForumView);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }

        public void ShutDown()
        {
            if (!SelectedForum.Closed)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da nastavite? Nakon sto jednom ugasite forum necete moci ponovo da ga pokrenete",
                                                          "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    CloseForum();
                }
                else
                {
                    MessageBox.Show("Forum nije ugasen, i dalje je aktivan.");
                }
            }
            else
            {
                MessageBox.Show("Ugasili ste ovaj forum, nije aktivan.");
            }
        }

        private void CloseForum()
        {
            SelectedForum.Closed = true;
            _forumService.Update(SelectedForum);
            MessageBox.Show("Uspesno ste ugasili forum. Sada je moguc samo pregled ovog foruma ne i ostavljanje komentara.");
        }

        private void FillTheTexts()
        {
            OpenedForumView.label1.Content = SelectedForum.Theme;
            OpenedForumView.ThemeBox.Text = SelectedForum.MainComment;
            Comments = _forumCommentService.FilterComments(Comments, SelectedForum);
            OpenedForumView.CommentsBox.ItemsSource = Comments;
        }
        public void Update()
        {
            Comments.Clear();
            Comments = new();
            Comments = _forumCommentService.FilterComments(Comments, SelectedForum);
            OpenedForumView.CommentsBox.ItemsSource = Comments;
        }
    }
}
