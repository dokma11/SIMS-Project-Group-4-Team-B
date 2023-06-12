using Sims2023.Domain.Models;
using Sims2023.Observer;
using System;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourWriteToCSVRepository
    {
        public int NextId();
        public void Add(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide);
        public void AddLocation(int toursId, Location location);                   
        public void DecideLocationToAdd(Tour tour, Location location, int newToursNumber, List<Location> locations);  
        public void AddKeyPoints(string keyPointsString, int firstToursId);        
        public void Save();
        public void UpdateState(Tour selectedTour, ToursState state);      
        public void SetLanguage(Tour selectedTour, ToursLanguage language);  
        public void UpdateAvailableSpace(int reservedSpace, Tour tour);
        public void Update(Tour tour);
        public void CancelAll(User loggedInGuide);
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
    }
}
