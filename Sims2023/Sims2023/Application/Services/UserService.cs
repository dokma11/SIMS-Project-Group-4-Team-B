using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class UserService
    {
        private IUserCSVRepository _user;

        public UserService()
        {
            _user = new UserCSVRepository();
            //_user = Injection.Injector.CreateInstance<IUserRepository>();
        }

        public void MarkSuperOwner()
        {
            _user.FindSuperOwners();
        }

        public List<User> GetAllUsers()
        {
            return _user.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _user.Subscribe(observer);
        }

        public User GetById(int id)
        {
            return _user.GetById(id);
        }

        public List<User> GetGuestsWithReservations(KeyPoint keyPoint, List<User> markedGuests)
        {
            return _user.GetGuestsWithReservations(keyPoint, markedGuests);
        }
    }
}
