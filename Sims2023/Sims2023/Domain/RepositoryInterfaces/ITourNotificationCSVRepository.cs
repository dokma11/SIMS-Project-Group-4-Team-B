using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Observer;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourNotificationCSVRepository
    {
        public TourNotification GetById(int id);
        
        public int NextId();
        
        public void Add(TourNotification tourNotification);
        public List<TourNotification> GetAll();

        public List<TourNotification> GetByUser(User user);
        public List<TourNotification> GetAcceptedTourRequest(User user);
        public List<TourNotification> GetMatchedTourRequestsLocation(User user);
        public List<TourNotification> GetMatchedTourRequestsLanguage(User user);
        public void SetIsNotified(TourNotification tourNotification);

        public void Subscribe(IObserver observer);
        public void NotifyObservers();
    }
}
