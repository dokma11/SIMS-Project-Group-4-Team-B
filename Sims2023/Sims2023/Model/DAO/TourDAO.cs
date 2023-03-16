using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.Model.DAO
{
    public class TourDAO
    {
        private List<IObserver> _observers;
        private List<Tour> _tours;
        private TourRepository _repository;
        public TourDAO()
        {
            _repository = new TourRepository();
            _tours = _repository.Load();
            _observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (_tours.Count == 0) return 1;
            return _tours.Max(t => t.Id) + 1;
        }
        public void Add(Tour tour, List<DateTime> dateTimes, Location location)
        {
            foreach (var date in dateTimes)
            {
                _tours = _repository.Load();
                tour.Id = NextId();
                tour.Start = date;
                AddToursLocation(tour.Id, location);
                _tours.Add(tour);
                _repository.Save(_tours);
                NotifyObservers();
            }
        }

        public void AddToursLocation(int toursId, Location location) 
        {
            foreach (var tour in _tours)
            {
                if (tour.Id == toursId)
                {
                    tour.LocationId = location.Id;
                    _repository.Save(_tours);
                    NotifyObservers();
                    break;
                }
            }
        }
        public void AddToursKeyPoints(string keyPointsString, int firstToursId)
        {
            foreach (var tourInstance in _tours)
            {
                if (tourInstance.Id == firstToursId)
                {
                    tourInstance.KeyPointsString = keyPointsString;
                    firstToursId++;
                    _repository.Save(_tours);
                    NotifyObservers();
                }
            }
        }
        public void Remove(Tour tour) 
        {
            _tours.Remove(tour);
            _repository.Save(_tours);   
            NotifyObservers();
        }
        public List<Tour> GetAll()
        {
            return _tours;
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
