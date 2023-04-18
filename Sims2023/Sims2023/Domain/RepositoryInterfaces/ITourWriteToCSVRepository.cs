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
        public void AddLocation(int toursId, Location location);                   //mozda prebaciti
        public void CheckAddLocation(Tour tour, Location location, int newToursNumber, List<Location> locations);  //mozda prebaciti
        public void AddKeyPoints(string keyPointsString, int firstToursId);        //mozda prebaciti
        public void Save();
        public void CalculateAttendedGuestsNumber(User loggedInGuide);       
        public void UpdateState(Tour selectedTour, ToursState state);      
        public void SetLanguage(Tour selectedTour, ToursLanguage language);  
        public bool CanRateTour(Tour tour); //Ovo ide u vm
        public bool CanSeeTour(Tour tour);  //Ovo ide u vm
        public void UpdateAvailableSpace(int reservedSpace, Tour tour);
        public void Update(Tour tour);
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        public void NotifyObservers();
    }
}
