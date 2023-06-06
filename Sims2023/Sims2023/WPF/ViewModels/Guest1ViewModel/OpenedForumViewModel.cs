using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _forumService = forumService;
            CheckIfUserIsCreator();
            FillTheTexts();

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
            //Proveri da li je selektovani smestaj shut down
            MessageBox.Show("Nije jos implementirana funkcionalnost");
        }

        public void GoBack()
        {
            NavigationService navigationService = NavigationService.GetNavigationService(OpenedForumView);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }

        internal void ShutDown()
        {
            if (SelectedForum.Closed == false)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da nastavite? Nakon sto jednom ugasite forum necete moci ponovo da ga pokrenete", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    SelectedForum.Closed = true;
                    _forumService.Update(SelectedForum);
                    MessageBox.Show("Uspesno ste ugasili forum. Sada je moguc samo pregled ovog foruma ne i ostavljanje komentara");
                }
                else
                {
                    MessageBox.Show("Forum nije ugasen, idalje je aktivan.");
                }
            }
            else
            {
                MessageBox.Show("Ugasili ste ovaj forum, nije aktivan.");
            }
        }

        private void FillTheTexts()
        {
            OpenedForumView.label1.Content = SelectedForum.Theme;
            OpenedForumView.label2.Content=SelectedForum.MainComment;
            Comments = _forumCommentService.FilterComments(Comments, SelectedForum);
        }
        public void Update()
        {
            Comments.Clear();
            Comments = new();
            Comments = _forumCommentService.FilterComments(Comments,SelectedForum);
            OpenedForumView.CommentsBox.ItemsSource = Comments;
        }
    }
}
