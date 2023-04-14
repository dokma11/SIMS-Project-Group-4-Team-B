using Sims2023.Domain.Models;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IKeyPointRepository
    {
        public int NextId();
        public void Add(KeyPoint keyPoint, List<string> keyPointNames, int toursId, int newToursNumber);
        public void Remove(KeyPoint keyPoint);
        public List<KeyPoint> GetAll();
        public KeyPoint GetById(int id);
        public void Save();
        public void GetKeyPointWhereGuestJoined(Tour selectedTour);
        public List<KeyPoint> GetByToursId(int id);
        public void ChangeKeyPointsState(KeyPoint keyPoint, KeyPoint.State state);
        public void AddGuestsId(KeyPoint selectedKeyPoint, int guestsId);
    }
}
