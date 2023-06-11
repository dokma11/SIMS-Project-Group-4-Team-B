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
using System.Windows.Navigation;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class MakeNewForumViewModel
    {
        public event Action GoBackRequested;
        public Location Location;
        private ObservableCollection<Forum> FilteredForums;
        private ForumService _forumService;
        private Frame MainFrame;
        private User User;
        MakeNewForumView MakeNewForumView;

        public MakeNewForumViewModel(Location location, ObservableCollection<Forum> filteredForums, ForumService forumService, Frame mainFrame, User user, MakeNewForumView makeNewForumView)
        {
            Location = location;
            FilteredForums = filteredForums;
            _forumService = forumService;
            MainFrame = mainFrame;
            User = user;
            MakeNewForumView = makeNewForumView;
        }

        public void GoBack()
        {
            NavigationService navigationService = NavigationService.GetNavigationService(MakeNewForumView);

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }

        public void NewForum()
        {
            String theme = MakeNewForumView.ThemeBox.Text;
            String comment = MakeNewForumView.CommentBox.Text;
            if(theme==null || comment == null)
            {
                MessageBox.Show("Sva polja je potrebno popuniti kako biste aktivirali forum");
                return;
            }
            Forum newForum = new();
            newForum.Location = Location;
            newForum.User = User;
            newForum.Special = false;
            newForum.Closed = false;
            newForum.Theme=theme;
            newForum.MainComment = comment;
            newForum.CountGuestComments= 0;
            newForum.CountOwnerComments= 0;
            _forumService.Create(newForum);
            MessageBox.Show("Uspešno ste otvorili forum: " + newForum.Theme);
            GoBack();
    }
    }
}
