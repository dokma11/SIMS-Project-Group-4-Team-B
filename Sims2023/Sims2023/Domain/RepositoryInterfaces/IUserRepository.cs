using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public int NextId();
        public List<User> FindOwners();
        public void FindSuperOwners();
        public void Update(User user);
        public void Add(User user);
        public void Remove(User user);
        public List<User> GetAll();
        public User GetById(int id);
        public List<User> GetGuestsWithReservations(KeyPoint keyPoint, List<User> markedGuests);
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
    }
}
