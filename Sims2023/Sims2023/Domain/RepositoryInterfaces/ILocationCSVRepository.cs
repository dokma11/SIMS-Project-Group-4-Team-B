using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ILocationCSVRepository
    {
        public Location GetById(int id);
        public int GetIdByLocation(string city, string country);
        public int NextId();
        public void CheckIdItShouldBeAdded(Location location);
        public void Add(Location location);
        public List<Location> GetAll();
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
    }
}
