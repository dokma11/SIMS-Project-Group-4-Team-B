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

        public UserCSVRepository()
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

        public void MarkSuperGuides(List<TourReview> tourReviews, List<Tour> finishedTours, User loggedInGuide)
        {
            var averageGradeSumByLanguage = new Dictionary<ToursLanguage, double>();
            var averageGradeSumByLanguageCount = new Dictionary<ToursLanguage, int>();

            var lastYearStartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            var lastYearEndDate = DateTime.Today;

            foreach (var tour in finishedTours.Where(r => r.Start >= lastYearStartDate && r.Start <= lastYearEndDate))
            {
                CalculateGradeAverages(tourReviews, tour, averageGradeSumByLanguage, averageGradeSumByLanguageCount);
            }

            loggedInGuide.SuperGuide = averageGradeSumByLanguageCount.Any(kv => kv.Value >= 20 && 
                         averageGradeSumByLanguage.ContainsKey(kv.Key) && averageGradeSumByLanguage[kv.Key] / kv.Value > 4.0);
            _fileHandler.Save(_users);
            NotifyObservers();
        }

        public void CalculateGradeAverages(List<TourReview> tourReviews, Tour tour, Dictionary<ToursLanguage, double> averageGradeSumByLanguage,
            Dictionary<ToursLanguage, int> averageGradeSumByLanguageCount)
        {
            if (!averageGradeSumByLanguage.ContainsKey(tour.GuideLanguage))
            {
                averageGradeSumByLanguage[tour.GuideLanguage] = 0.0;
                averageGradeSumByLanguageCount[tour.GuideLanguage] = 0;
            }

            foreach (var tourReview in tourReviews.Where(tr => tr.Tour.Id == tour.Id))
            {
                averageGradeSumByLanguage[tour.GuideLanguage] += tourReview.AverageGrade;
                averageGradeSumByLanguageCount[tour.GuideLanguage]++;
            }
        }

        public void MarkDismissal(User loggedInGuide)
        {
            loggedInGuide.AbleToLogIn = false;
            _fileHandler.Save(_users);
            NotifyObservers();
        }

    }
}
