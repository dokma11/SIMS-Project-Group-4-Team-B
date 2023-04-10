using Sims2023.Domain.Models;
using Sims2023.Model;
using Sims2023.Model.DAO;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class UserService
    {
        private UserRepository _user;

        public UserService()
        {
            _user = new UserRepository();
        }

        public void MarkSuperOwner()
        {
            _user.FindSuperOwners();
        }

        public List<User> GetAllUsers()
        {
            return _user.GetAll();
        }

        public void Create(User user)
        {
            _user.Add(user);
        }

        public void Delete(User user)
        {
            _user.Remove(user);
        }

        public void Subscribe(IObserver observer)
        {
            _user.Subscribe(observer);
        }

        public User GetById(int id)
        {
            return _user.GetById(id);
        }
    }
}
