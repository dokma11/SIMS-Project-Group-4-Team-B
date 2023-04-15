using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sims2023.Application.Services
{
    public class TourService
    {
        private readonly ITourRepository _tour;
        private readonly LocationRepository _location;

        public TourService()
        {
            _tour = new TourRepository();
            _location = new LocationRepository();
        }

        public List<Tour> GetAll()
        {
            return _tour.GetAll();
        }

        public void Create(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide)
        {
            _tour.Add(tour, dateTimes, location, loggedInGuide);
        }
        public void Update(Tour tour)//new method and deleted edit
        {
            _tour.Update(tour);
        }
        public bool CanRateTour(Tour tour)//new for guest2
        {
            return _tour.CanRateTour(tour);
        }

        public bool CanSeeTour(Tour tour)//new for guest2
        {
            return _tour.CanSeeTour(tour);
        }
        public void UpdateAvailableSpace(int reservedSpace,Tour tour)//new for guest2
        {
            _tour.UpdateAvailableSpace(reservedSpace, tour);
        }

        public void AddToursLocation(Tour tour, Location location, int newToursNumber)
        {
            _tour.CheckAddToursLocation(tour, location, newToursNumber, _location.GetAll());
        }

        public void AddLocationsToTour(ObservableCollection<Location> locations, ObservableCollection<Tour> tours)//new,delete later
        {
            _tour.AddLocationsToTour(locations, tours);
        }

        public void AddToursKeyPoints(List<string> keyPoints, int firstToursId)
        {
            string keyPointsString = string.Join(",", keyPoints);
            _tour.AddToursKeyPoints(keyPointsString, firstToursId);
        }

        public void Delete(Tour tour)
        {
            _tour.Remove(tour);
        }

        /*public void Subscribe(IObserver observer)
        {
            _tour.Subscribe(observer);
        }*/

        public void Save()
        {
            _tour.Save();
        }

        public Tour GetById(int id)
        {
            return _tour.GetById(id);
        }

        public List<Tour> GetAvailable()//new for guest2
        {
            return _tour.GetAvailable();
        }

        public List<Tour> GetAlternative(int reserved,Tour tour)//new for guest2
        {
            return _tour.GetAlternative(reserved,tour);
        }
    }
}
