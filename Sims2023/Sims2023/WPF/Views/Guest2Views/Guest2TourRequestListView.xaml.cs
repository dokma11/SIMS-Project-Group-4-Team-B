using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for Guest2TourRequestListView.xaml
    /// </summary>
    public partial class Guest2TourRequestListView : Window
    {
        public List<Request> TourRequests { get; set; }

        public User User { get; set; }
        public RequestService _requestService { get; set; }
        public Guest2TourRequestListView(User user)
        {
            InitializeComponent();
            _requestService = new RequestService();
            User = user;
            DataContext = this;
            TourRequests = _requestService.GetByUser(user);
        }
    }
}
