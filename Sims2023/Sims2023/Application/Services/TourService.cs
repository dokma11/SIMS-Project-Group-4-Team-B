using Sims2023.Controller;
using Sims2023.Domain.Models;
using Sims2023.Model.DAO;
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

        public List<Tour> GetAll()
        {
            return _tour.GetAll();
        }

        public void Create(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide)
        {
            _tour.Add(tour, dateTimes, location, loggedInGuide);
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

        public List<Tour> GetFinishedTours(User loggedInGuide)
        {
            return _tour.GetFinishedTours(loggedInGuide);
        }

        public void GetAttendedGuestsNumber(User loggedInGuide)
        {
            _tour.GetAttendedGuestsNumber(loggedInGuide);
        }

        public Tour GetTheMostVisitedTour(User loggedInGuide, string year)
        {
            return _tour.GetTheMostVisitedTour(loggedInGuide, year);
        }

        public string GetAgeStatistics(Tour selectedTour, string ageGroup)
        {
            return _tour.GetAgeStatistics(selectedTour, ageGroup);
        }

        public string GetVoucherStatistics(Tour selectedTour, bool used)
        {
            return _tour.GetVoucherStatistics(selectedTour, used);
        }

        public List<Tour> GetCreatedTours(User loggedInGuide)
        {
            return _tour.GetCreatedTours(loggedInGuide);
        }
    }
}
