using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Model.DAO
{
    public class UserDAO
    {
        private List<IObserver> _observers;
        private List<User> _users;
        private UserFileHandler _fileHandler;

        public UserDAO()
        {
            _observers = new List<IObserver>();
            _fileHandler = new UserFileHandler();
            _users = _fileHandler.Load();
        }

        public int NextId()
        {
            if (_users.Count == 0) return 1;
            return _users.Max(u => u.Id) + 1;
        }

        public void Add(User user)
        {
            user.Id = NextId();
            _users.Add(user);
            _fileHandler.Save(_users);
            NotifyObservers();
        }

        public void Remove(User user)
        {
            _users.Remove(user);
            _fileHandler.Save(_users);
            NotifyObservers();
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
