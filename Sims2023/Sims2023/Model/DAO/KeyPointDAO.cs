using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model.DAO
{
    public class KeyPointDAO
    {
        private List<IObserver> _observers;
        private List<KeyPoint> _keyPoints;
        private KeyPointRepository _repository;
        public KeyPointDAO()
        {
            _repository = new KeyPointRepository();
            _keyPoints = _repository.Load();
            _observers = new List<IObserver>();
        }
        public int NextId()
        {
            if (_keyPoints.Count == 0) return 1;
            return _keyPoints.Max(k => k.Id) + 1;
        }
        public void Add(KeyPoint keyPoint, List<string> keyPointNames, int toursId, int newToursNumber)
        {
            for(int i = 1; i <= newToursNumber; i++)
            {
                foreach (string keyPointName in keyPointNames)
                {
                    _keyPoints = _repository.Load();
                    keyPoint.Id = NextId();
                    keyPoint.ToursId = toursId;
                    keyPoint.Name = keyPointName;
                    keyPoint.CurrentState = KeyPoint.State.NotVisited;
                    _keyPoints.Add(keyPoint);
                    _repository.Save(_keyPoints);
                    NotifyObservers();
                }
                toursId++;
            }
        }
        public void Remove(KeyPoint keyPoint)
        {
            _keyPoints.Remove(keyPoint);
            _repository.Save(_keyPoints);
            NotifyObservers();
        }
        public List<KeyPoint> GetAll()
        {
            return _keyPoints;
        }
        public KeyPoint GetById(int id)
        {
            return _repository.GetById(id);
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
