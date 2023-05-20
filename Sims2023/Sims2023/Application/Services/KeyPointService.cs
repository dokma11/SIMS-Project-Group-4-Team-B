using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class KeyPointService
    {
        private IKeyPointCSVRepository _keyPoint;
        private ITourReadFromCSVRepository _tour;
        public KeyPointService()
        {
            _keyPoint = Injection.Injector.CreateInstance<IKeyPointCSVRepository>();
            _tour = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();

            GetTourReferences();
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
            Save();
        }

        public void Subscribe(IObserver observer)
        {
            _keyPoint.Subscribe(observer);
        }

        public void Save()
        {
            _keyPoint.Save();
            GetTourReferences();
        }

        public List<KeyPoint> GetByToursId(int id)
        {
            return _keyPoint.GetByToursId(id);
        }

        public void ChangeKeyPointsState(KeyPoint keyPoint, KeyPointsState state)
        {
            _keyPoint.ChangeState(keyPoint, state);
            Save();
        }

        public void AddGuestsId(KeyPoint selectedKeyPoint, int guestsId)
        {
            _keyPoint.AddGuestsId(selectedKeyPoint, guestsId);
            Save();
        }

        public KeyPoint GetWhereGuestJoined(Tour selectedTour, User loggedInGuest)
        {
            return _keyPoint.GetWhereGuestJoined(selectedTour, loggedInGuest);
        }

        public void GetTourReferences()
        {
            foreach (var keyPoint in GetAll())
            {
                keyPoint.Tour = _tour.GetById(keyPoint.Tour.Id) ?? keyPoint.Tour;
            }
        }
    }
}

