using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;

namespace Sims2023.Application.Services
{
    public class TourNotificationService
    {
        private readonly ITourNotificationCSVRepository _tourNotifications;
        public TourNotificationService()
        {
            _tourNotifications = new TourNotificationCSVRepository();
            //_tourNotifications = Injection.Injector.CreateInstance<ITourNotificationCSVRepository>();
        }

        public TourNotification GetById(int id)
        {
            return _tourNotifications.GetById(id);
        }

        public void Create(TourNotification acceptedTourRequest)
        {
            _tourNotifications.Add(acceptedTourRequest);
        }

        
        public void Subscribe(IObserver observer)
        {
            _tourNotifications.Subscribe(observer);
        }

        
        public List<TourNotification> GetAcceptedTourRequest(User user)
        {
            return _tourNotifications.GetAcceptedTourRequest(user);
        }
        public List<TourNotification> GetMatchedTourRequestsLocation(User user)
        {
            return _tourNotifications.GetMatchedTourRequestsLocation(user);
        }
        public List<TourNotification> GetMatchedTourRequestsLanguage(User user)
        {
            return _tourNotifications.GetMatchedTourRequestsLanguage(user);
        }
        public void SetIsNotified(TourNotification tourNotification)
        {
            _tourNotifications.SetIsNotified(tourNotification);
        }
    }
}
