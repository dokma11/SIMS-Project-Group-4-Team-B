using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class KeyPointService
    {
        private IKeyPointRepository _keyPoint;
        public KeyPointService()
        {
            _keyPoint = new KeyPointRepository();
            //_keyPoint = Injection.Injector.CreateInstance<IKeyPointRepository>();
        }

        public List<KeyPoint> GetAll()
        {
            return _keyPoint.GetAll();
        }

        public KeyPoint GetById(int id)
        {
            return _keyPoint.GetById(id);
        }

        public KeyPoint GetCurrentKeyPoint(Tour tour)//added for guest2
        {
            return _keyPoint.GetCurrentKeyPoint(tour);
        }

        public void Create(KeyPoint keyPoint, List<string> keyPointNames, int toursId, int newToursNumber)
        {
            _keyPoint.Add(keyPoint, keyPointNames, toursId, newToursNumber);
        }

        public void Delete(KeyPoint keyPoint)
        {
            _keyPoint.Remove(keyPoint);
        }

        public void Subscribe(IObserver observer)
        {
            _keyPoint.Subscribe(observer);
        }

        public void Save()
        {
            _keyPoint.Save();
        }

        public List<KeyPoint> GetByToursId(int id)
        {
            return _keyPoint.GetByToursId(id);
        }

        public void ChangeKeyPointsState(KeyPoint keyPoint, KeyPointsState state)
        {
            _keyPoint.ChangeState(keyPoint, state);
        }

        public void AddGuestsId(KeyPoint selectedKeyPoint, int guestsId)
        {
            _keyPoint.AddGuestsId(selectedKeyPoint, guestsId);
        }

        public KeyPoint GetWhereGuestJoined(Tour selectedTour, User loggedInGuest)
        {
            return _keyPoint.GetWhereGuestJoined(selectedTour, loggedInGuest);
        }
    }
}

