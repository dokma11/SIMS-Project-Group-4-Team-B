using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;

namespace Sims2023.Repositories
{
    public class TourNotificationCSVRepository:ITourNotificationCSVRepository
    {
        
        private readonly List<TourNotification> _tourNotifications;
        private readonly TourNotificationFileHandler _fileHandler;
        private readonly List<IObserver> _observers;

        public TourNotificationCSVRepository()
        {
            _fileHandler = new TourNotificationFileHandler();
            _tourNotifications = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public TourNotification GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            return _tourNotifications.Count == 0 ? 1 : _tourNotifications.Max(t => t.Id) + 1;
        }

        public void Add(TourNotification tourNotification)
        {
            tourNotification.Id = NextId();
            _tourNotifications.Add(tourNotification);
            _fileHandler.Save(_tourNotifications);
            NotifyObservers();
        }

        public List<TourNotification> GetAll()
        {
            return _tourNotifications;
        }

        public List<TourNotification> GetByUser(User user)
        {
            return _tourNotifications
                .Where(r => r.Guest.Id == user.Id)
                .ToList();
        }

        public List<TourNotification> GetAcceptedTourRequest(User user)
        {
            return GetByUser(user)
                .Where(r => r.Type==NotificationType.AcceptedTourRequest && r.IsNotified==false)
                .ToList();
        }

        public List<TourNotification> GetMatchedTourRequestsLocation(User user)
        {
            return GetByUser(user)
                .Where(r => r.Type == NotificationType.MatchedTourRequestsLocation && r.IsNotified==false)
                .ToList();
        }

        public List<TourNotification> GetMatchedTourRequestsLanguage(User user)
        {
            return GetByUser(user)
                .Where(r => r.Type == NotificationType.MatchedTourRequestsLanguage && r.IsNotified==false)
                .ToList();
        }

        public void SetIsNotified(TourNotification tourNotification)
        {
            tourNotification.IsNotified=true;
            _fileHandler.Save(_tourNotifications);
        }
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
