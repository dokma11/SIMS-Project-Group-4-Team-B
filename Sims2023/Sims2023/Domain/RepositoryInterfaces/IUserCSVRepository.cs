using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface IUserCSVRepository
    {
        public int NextId();
        public List<User> FindOwners();
        public void FindSuperOwners();
        public void Update(User user);
        public List<User> GetAll();
        public User GetById(int id);
        public List<User> GetGuestsWithReservations(KeyPoint keyPoint, List<User> markedGuests);
        public void GetSuperGuides();
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        void MarkGuestAsSuper(User user);
        void MarkGuestAsRegular(User user);
        void RemovePointFromGuest1(User user);
    }
}
