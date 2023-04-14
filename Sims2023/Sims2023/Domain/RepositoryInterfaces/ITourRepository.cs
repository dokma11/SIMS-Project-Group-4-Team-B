using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        public int NextId();
        public void Add(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide);
        public void AddEdited(Tour tour);
        public void AddToursLocation(int toursId, Location location);
        public void CheckAddToursLocation(Tour tour, Location location, int newToursNumber, List<Location> locations);
        public void AddToursKeyPoints(string keyPointsString, int firstToursId);
        public void Remove(Tour tour);
        public List<Tour> GetAll();
        public Tour GetById(int id);
        public void Save();
        public List<Tour> GetFinishedTours(User loggedInGuide);
        public void GetAttendedGuestsNumber(User loggedInGuide);
        public Tour GetTheMostVisitedTour(User loggedInGuide, string year);
        public string GetAgeStatistics(Tour selectedTour, string ageGroup);
        public string GetVoucherStatistics(Tour selectedTour, bool used);
        public List<Tour> GetCreatedTours(User loggedInGuide);
    }
}
