using Sims2023.Application.Services;
using Sims2023.Domain.Models;
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
    /// Interaction logic for ListOfForumsView.xaml
    /// </summary>
    public partial class ListOfForumsView : Page
    {
        public LocationService _locationService;
        public AccommodationService _accommodationService;
        public ForumService _forumService;

        public List<Location> locations { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }
        public ListOfForumsView(User owner)
        {
            
            InitializeComponent();
            _locationService = new LocationService();
            _accommodationService = new AccommodationService();
            _forumService = new ForumService();

            locations = _accommodationService.GetOwnerLocations(_accommodationService.GetOwnerAccommodations(_accommodationService.GetAllAccommodations(), owner));

            Forums = new ObservableCollection<Forum>(_forumService.GetForumsForParticularOwner(locations));
            DataContext = this;
        }
    }
}
