using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public FakeCommentService _fakeCommentService;
        public User owner { get; set; }
        public Forum SelectedForum { get; set; }

        public ForumComment SelectedComment { get; set; }
        public string welcomeString { get; set; }
        public ObservableCollection<ForumComment> ForumComment { get; set; }
        public CommentsView(User user, Forum selectedForum)
        {
            InitializeComponent();
            DataContext = this;
            welcomeString = "Tema: " + selectedForum.Theme;
            SelectedForum = selectedForum;
            owner = user;
            _forumService = new ForumService();
            _commentService = new ForumCommentService();
            ForumComment = new ObservableCollection<ForumComment>();
            _fakeCommentService = new FakeCommentService();
            FillTheList();
        }

        public void FillTheList()
        {
            ForumComment.Clear();

            ForumComment.Add(new ForumComment { Id = 100, Forum = SelectedForum, Comment = SelectedForum.MainComment, Special = false, User = SelectedForum.User, NumberOfReports = 0 });
            var filteredComments = _commentService.FilterComments(ForumComment, SelectedForum);
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

        private void ReportComment_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedComment != null)
            {
                if (!isAlreadyReported(_fakeCommentService.GetAll(), SelectedComment) && SelectedComment.Special == false)
                {
                    ToastNotificationService.ShowInformation("Uspiješno prijavljivanje komentara");
                    FakeComment fake = new FakeComment(owner, SelectedComment);
                    ++SelectedComment.NumberOfReports;
                    _commentService.Update(SelectedComment);
                    _fakeCommentService.Create(fake);
                    FillTheList();

                }
                else if (SelectedComment.User.UserType == User.Type.Owner) ToastNotificationService.ShowInformation("Ne možete prijaviti komentar vlasnika smještaja na ovoj lokaciji");
                else if (SelectedComment.Special == true) ToastNotificationService.ShowInformation("Ne možete prijaviti korisnika koji je boravio na ovoj lokaciji");
                
            }
            else ToastNotificationService.ShowInformation("Selektujte komentar koji želite da prijavite");
        }

        private bool isAlreadyReported(List<FakeComment> fake, ForumComment comment)
        {
            foreach (FakeComment fakee in fake)
            {
                if (fakee.Owner.Id == owner.Id && fakee.Comment.Id == comment.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
