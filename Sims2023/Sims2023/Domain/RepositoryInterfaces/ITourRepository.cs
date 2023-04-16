using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        public int NextId();
        public void Add(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide);
        
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
        public List<Tour> GetCreatedTours(User loggedInGuide);
        public void ChangeToursState(Tour selectedTour, Tour.State state);
        public void SetToursLanguage(Tour selectedTour, Tour.Language language);

        public bool CanRateTour(Tour tour);
        public bool CanSeeTour(Tour tour);
        public List<Tour> GetAvailable();
        public List<Tour> GetAlternative(int reserveSpace, Tour tour);
        public void UpdateAvailableSpace(int reservedSpace, Tour tour);
        public void Update(Tour tour);
        
        public Uri GetPictureUri(Tour tour, int i);
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
    }
}
