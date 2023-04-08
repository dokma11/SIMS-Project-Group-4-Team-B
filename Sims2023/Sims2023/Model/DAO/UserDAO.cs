using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace Sims2023.Model.DAO
{
    public class UserDAO
    {
        private List<IObserver> _observers;
        private List<User> _users;
        private UserFileHandler _fileHandler;
        private AccommodationGradeDAO guests;

        public UserDAO()
        {
            _observers = new List<IObserver>();
            _fileHandler = new UserFileHandler();
            _users = _fileHandler.Load();
    //        guests = new GuestGradeDAO();
        }

        public int NextId()
        {
            if (_users.Count == 0) return 1;
            return _users.Max(u => u.Id) + 1;
        }

        public List<User> FindOwners()
        {
            List<User> users = new List<User>();
            foreach(User user in _users)
            {
                if(user.UserType == User.Type.Owner)
                {
                    users.Add(user);
                }
            }
            return users;
        }

        public void FindSuperOwners()
        {
            guests = new AccommodationGradeDAO();
            foreach (User user in FindOwners())
            {
                int counter = 0;
                double zbir = 0;
                double Average;
                foreach (AccommodationGrade grade in guests.GetAll())
                {
                   
                    if(grade.Accommodation.Id == user.Id)
                    {
                        counter++;
                        zbir += guests.FindAverage(grade);

                     }
                }
                Average = zbir / counter;
                if (Average > 4.5)
                {
                    user.superOwner = true;
                    Update(user);
                }
                else
                {
                    user.superOwner = false;
                    Update(user);
                }
            }
        }

        public void Update(User user)
        {
            int index = _users.FindIndex(p => p.Id == user.Id);
            if (index != -1)
            {
                _users[index] = user;
            }

            _fileHandler.Save(_users);
            NotifyObservers();
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

        public User GetById(int id)
        {
            return _fileHandler.GetById(id);
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
