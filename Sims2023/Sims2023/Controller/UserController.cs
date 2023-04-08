using Sims2023.Model;
using Sims2023.Model.DAO;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Controller
{
    public class UserController
    {
        private UserDAO _user;

        public UserController()
        {
            _user = new UserDAO();
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
