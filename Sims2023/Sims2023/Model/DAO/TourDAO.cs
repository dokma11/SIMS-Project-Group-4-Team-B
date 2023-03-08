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
        public void Add(Tour tour, List<DateTime> dateTimes)
        {
            List<Tour> newList = new List<Tour>();

            foreach(var date in dateTimes) 
            {
                Tour newTour = new Tour();
                newList.Add(newTour);
            }

            foreach (var date in dateTimes)
            {
                Tour newTour = new Tour();
                string dtstring = date.ToString();
                MessageBox.Show("Ovo je: " + dtstring);
                tour.Id = NextId();
                MessageBox.Show("Id: " + tour.Id);
                tour.Start = date;
                newTour = tour;
                _tours.Add(newTour);
            }
            _repository.Save(_tours);
            NotifyObservers();
        }

        public void AddToursLocation(Tour tour, Location location) 
        {
            tour.LocationId = location.Id;
            _repository.Save(_tours);
            NotifyObservers();
        }
        public void Remove(Tour tour) 
        {
                
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
