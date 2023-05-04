using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repositories
{
    internal class AccommodationStatisticsCSVRepository : IAccommodationStatisticsCSVRepository
    {

        private List<IObserver> _observers;
        private AccommodationStatisticsFileHandler _statisticsHandler;
        private List<AccommodationStatistics> _statistics;

        public AccommodationStatisticsCSVRepository()
        {
            _statisticsHandler = new AccommodationStatisticsFileHandler();
            _statistics = _statisticsHandler.Load();
            _observers = new List<IObserver>();
        }
        public void Add(AccommodationStatistics statistic)
        {
            _statistics.Add(statistic);
            _statisticsHandler.Save(_statistics);
            NotifyObservers();
        }
        public List<AccommodationStatistics> GetAll()
        {
            return _statistics;
        }
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
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
