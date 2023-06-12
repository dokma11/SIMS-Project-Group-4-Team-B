using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private int counter;

        public OpenedForumViewModel(User user, Frame mainFrame, Forum selectedForum, OpenedForumView openedForumView, ForumService forumService,ForumCommentService forumCommentService)
        {
            User = user;
            MainFrame = mainFrame;
            SelectedForum = selectedForum;
            OpenedForumView = openedForumView;
            _forumCommentService = forumCommentService;
            _forumCommentService.Subscribe(this);
            _forumService = forumService;
            _forumService.Subscribe(this);

            counter = 0;
            Update();
            CheckIfUserIsCreator();
            CheckTheForum();
            FillTheTexts();

        }

        private void CheckTheForum()
        {
            if (!SelectedForum.Special && counter<=2)
            {
                counter++;
                MarkForumsAsSpecial();
            }
            
            if (SelectedForum.Closed)
            {
                OpenedForumView.CloseTheForum.Visibility = Visibility.Collapsed;
                OpenedForumView.Label1.Visibility = Visibility.Visible;
                OpenedForumView.postForum.IsEnabled = false;
            }
        }
        public void MarkForumsAsSpecial()
        {
            List<ForumComment> allComments = _forumCommentService.GetAllForumComments();
            _forumService.MarkAsSpecial(allComments);
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
                BackgroundShading();
                var newWindow = new ForumCommentView(User, _forumCommentService, SelectedForum,_forumService);
                newWindow.ShowDialog();
                BackgroundUnshading();
                Update();
            }
            else
            {
                MessageBox.Show("Forum je ugašen. Moguć je samo pregled ovog foruma ne i ostavljanje komentara.");
            }

        }
        internal void BackgroundShading()
        {
            OpenedForumView.Overlay1.Visibility = Visibility.Visible;
        }

        internal void BackgroundUnshading()
        {
            OpenedForumView.Overlay1.Visibility = Visibility.Collapsed;
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
            if(OpenedForumView.CloseTheForum.Visibility==Visibility.Visible)
            {
                ShutDownTheForum();
            }
            else
            {
                MessageBox.Show("Forum može ugasiti samo korisnik koji ga je kreirao.");
            }
           
        }

        private void ShutDownTheForum()
        {
            if (!SelectedForum.Closed)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da nastavite? Nakon što jednom ugasite forum nećete moći ponovo da ga pokrenete",
                                                          "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    CloseForum();
                }
                else
                {
                    MessageBox.Show("Forum nije ugašen, i dalje je aktivan.");
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
            MessageBox.Show("Uspešno ste ugasili forum. Sada je moguć samo pregled ovog foruma ne i ostavljanje komentara.");
        }

        private void FillTheTexts()
        {
            OpenedForumView.label.Content = SelectedForum.Theme;
            OpenedForumView.label1.Content = SelectedForum.User.Name+" "+SelectedForum.User.Surname;
            OpenedForumView.ThemeBox.Text = SelectedForum.MainComment;
            Comments = new();
            Comments = _forumCommentService.FilterComments(Comments, SelectedForum);
            OpenedForumView.CommentsBox.ItemsSource = Comments;
        }
        public void Update()
        {
            Comments.Clear();
            Comments = new();
            Comments = _forumCommentService.FilterComments(Comments, SelectedForum);
            OpenedForumView.CommentsBox.ItemsSource = Comments;
            CheckTheForum();
        }
    }
}
