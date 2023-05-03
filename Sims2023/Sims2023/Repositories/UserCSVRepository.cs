﻿using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class UserCSVRepository: IUserCSVRepository
    {
        private List<IObserver> _observers;
        private List<User> _users;
        private UserFileHandler _fileHandler;
        private TourReservationCSVRepository _tourReservationRepository;
        private AccommodationGradeCSVRepository guests;

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
            guests = new AccommodationGradeCSVRepository();
            foreach (User user in FindOwners())
            {
                double counter = 0.0;
                double zbir = 0.0;
                double Average;
                foreach (AccommodationGrade grade in guests.GetAll())
                {

                    if (grade.Accommodation.Owner.Id == user.Id)
                    {
                        ++counter;
                        zbir += guests.FindAverage(grade);
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

        public List<User> GetGuestsWithReservations(KeyPoint keyPoint, List<User> markedGuests)
        {
            _tourReservationRepository = new TourReservationCSVRepository();
            return _tourReservationRepository
                .GetAll()
                .Where(reservation => reservation.Tour.Id == keyPoint.Tour.Id)
                .Select(reservation => GetById(reservation.User.Id))
                .Where(guest => CheckIfGuestMarked(guest, keyPoint, markedGuests))
                .ToList();
        }

        private bool CheckIfGuestMarked(User guest, KeyPoint keyPoint, List<User> markedGuests)
        {
            return !keyPoint.ShowedGuestsIds.Contains(guest.Id) &&
                !markedGuests.Any(markedGuest => markedGuest.Id == guest.Id);
        }
    }
}