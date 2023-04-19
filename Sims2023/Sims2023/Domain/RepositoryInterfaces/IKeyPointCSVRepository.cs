using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IKeyPointCSVRepository
    {
        public int NextId();
        public void Add(KeyPoint keyPoint, List<string> keyPointNames, int toursId, int newToursNumber);
        public void Remove(KeyPoint keyPoint);
        public List<KeyPoint> GetAll();
        public KeyPoint GetById(int id);
        public void Save();
        public List<KeyPoint> GetByToursId(int id);
        public void ChangeState(KeyPoint keyPoint, KeyPointsState state);
        public void AddGuestsId(KeyPoint selectedKeyPoint, int guestsId);
        public KeyPoint GetCurrentKeyPoint(Tour tour);//new method for guest2
        void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
        public KeyPoint GetWhereGuestJoined(Tour selectedTour, User loggedInGuest);
    }
}
