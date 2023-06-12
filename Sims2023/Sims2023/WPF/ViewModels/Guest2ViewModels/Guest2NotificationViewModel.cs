using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    
    public class Guest2NotificationViewModel
    {
        private TourNotificationService _notificationService;
        public TourNotification SelectedNotification { get; set; }
        public List<TourNotification> Notifications { get; set; }
        public Guest2NotificationView Guest2NotificationView { get; set; }
        public Guest2NotificationViewModel(User user,Guest2NotificationView guest2NotificationView)
        {
            _notificationService = new TourNotificationService();
            SelectedNotification = null;
            Guest2NotificationView = guest2NotificationView;
            Notifications = _notificationService.GetByUser(user);
        }
    }
}
