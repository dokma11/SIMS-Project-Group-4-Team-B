using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ListOfForumsView.xaml
    /// </summary>
    public partial class ListOfForumsView : Page
    {
        public LocationService _locationService;
        public AccommodationService _accommodationService;
        public ForumService _forumService;
        public User user { get; set; }
        public Forum SelectedForum { get; set; }

        public List<Location> locations { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }
        public ListOfForumsView(User owner)
        {
            
            InitializeComponent();
            _locationService = new LocationService();
            _accommodationService = new AccommodationService();
            _forumService = new ForumService();
            user = owner;
            locations = _accommodationService.GetOwnerLocations(_accommodationService.GetOwnerAccommodations(_accommodationService.GetAllAccommodations(), owner));

            Forums = new ObservableCollection<Forum>(_forumService.GetForumsForParticularOwner(locations));
            DataContext = this;
        }

        private void OpenForum_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedForum !=null) 
            {
                if (!SelectedForum.OwnerOpened) 
                { 
                   SelectedForum.OwnerOpened = true;
                    _forumService.Update(SelectedForum);
                }
                CommentsView comments = new CommentsView(user, SelectedForum);
                FrameManager.Instance.MainFrame.Navigate(comments);
            }    
        }
    }
}
