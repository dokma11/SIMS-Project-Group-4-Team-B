using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourService
    {
        private readonly TourRepository _tour;
        private readonly LocationRepository _location;

        public TourService()
        {
            _tour = new TourRepository();
            _location = new LocationRepository();
        }

        public List<Tour> GetAllTours()
        {
            return _tour.GetAll();
        }

        public void Create(Tour tour, List<DateTime> dateTimes, Location location)
        {
            _tour.Add(tour, dateTimes, location);
        }
        public void Edit(Tour tour, Tour oldTour)
        {
            _tour.Remove(oldTour);
            _tour.AddEdited(tour);
        }

        public void AddToursLocation(Tour tour, Location location, int newToursNumber)
        {
            _tour.CheckAddToursLocation(tour, location, newToursNumber, _location.GetAll());
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

        public void Subscribe(IObserver observer)
        {
            _tour.Subscribe(observer);
        }

        public void Save()
        {
            _tour.Save();
        }

        public Tour GetById(int id)
        {
            return _tour.GetById(id);
        }
    }
}
