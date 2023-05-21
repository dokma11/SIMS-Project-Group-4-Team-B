using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class UserCSVRepository : IUserCSVRepository
    {
        private List<IObserver> _observers;
        private List<User> _users;
        private UserFileHandler _fileHandler;
     //   private AccommodationGradeCSVRepository guests;

        public UserCSVRepository()
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
            foreach (User user in _users)
            {
                if (user.UserType == User.Type.Owner)
                {
                    users.Add(user);
                }
            }
            return users;
        }

        public void FindSuperOwners()
        {
          /*  foreach (User user in FindOwners())
            {
                double counter = 0.0;
                double zbir = 1;
                double Average;
         //       guests = new AccommodationGradeCSVRepository();
        //        foreach (AccommodationGrade grade in guests.GetAll())
                {

             //       if (grade.Accommodation.Owner.Id == user.Id)
                    {
                        ++counter;
              //          zbir += FindAverageGrade(grade);
                    }
                }
                Average = zbir / counter;

                if (Average > 4.5 && counter > 50)
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
          */
        }

        public double FindAverageGrade(AccommodationGrade grade)
        {
            double avg;
            avg = (grade.Cleanliness + grade.Comfort + grade.Location + grade.Owner + grade.ValueForMoney) / 5;
            return avg;
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

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void MarkGuestAsSuper(User user)
        {
            user.SuperGuest1 = true;
            user.Guest1Points = 5;
            user.DateOfBecomingSuperGuest = DateTime.Today;
            Update(user);
        }
        public void MarkGuestAsRegular(User user)
        {
            user.SuperGuest1 = false;
            user.Guest1Points = 0;
            Update(user);
        }
        public void RemovePointFromGuest1(User user)
        {
            if (user.Guest1Points > 0)
            {
                user.Guest1Points--;
            }
            Update(user);
        }
    }
}
