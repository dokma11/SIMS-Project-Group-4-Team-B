﻿using Sims2023.Domain.Models;
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
        public void MarkSuperGuides(List<TourReview> tourReviews, List<Tour> finishedTours, User loggedInGuide);
        public void CalculateGradeAverages(List<TourReview> tourReviews, Tour tour, 
        Dictionary<ToursLanguage, double> averageGradeSumByLanguage,Dictionary<ToursLanguage, int> averageGradeSumByLanguageCount);
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        public void MarkGuestAsSuper(User user);
        public void MarkGuestAsRegular(User user);
        public void RemovePointFromGuest1(User user);
        public void MarkDismissal(User loggedInGuide);
        public void AppHasBeenStarted();
    }
}
