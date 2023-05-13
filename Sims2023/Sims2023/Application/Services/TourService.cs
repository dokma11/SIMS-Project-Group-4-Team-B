using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using Sims2023.Repository;
using System;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourService
    {
        private ITourWriteToCSVRepository _tour;
        private ILocationCSVRepository _location;
        private ITourReadFromCSVRepository _tourReadFromCSVRepository;
        public TourService()
        {
            _tour = new TourWriteToCSVRepository();
            //_tour = Injection.Injector.CreateInstance<ITourWriteToCSVRepository>();
            _location = new LocationCSVRepository();
            //_location = Injection.Injector.CreateInstance<ILocationRepository>();
            _tourReadFromCSVRepository = new TourReadFromCSVRepository();
            //_tourReadFromCSVRepository = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();
        }

        public void Create(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide)
        {
            _tour.Add(tour, dateTimes, location, loggedInGuide);
        }
        
        public void UpdateAvailableSpace(int reservedSpace, Tour tour)//new for guest2
        {
            _tour.UpdateAvailableSpace(reservedSpace, tour);
        }

        public void AddToursLocation(Tour tour, Location location, int newToursNumber)
        {
            _tour.DecideLocationToAdd(tour, location, newToursNumber, _location.GetAll());
        }

        public void AddToursKeyPoints(List<string> keyPoints, int firstToursId)
        {
            string keyPointsString = string.Join(",", keyPoints);
            _tour.AddKeyPoints(keyPointsString, firstToursId);
        }

        public Uri GetPictureUri(Tour tour, int i)
        {
            return _tourReadFromCSVRepository.GetPictureUri(tour, i);
        }

        public void Subscribe(IObserver observer)
        {
            _tour.Subscribe(observer);
        }

        public Tour GetById(int id)
        {
            return _tourReadFromCSVRepository.GetById(id);
        }

        public List<Tour> GetCreated()//new for guest2
        {
            return _tourReadFromCSVRepository.GetCreated();
        }

        public List<Tour> GetAlternatives(int reserved, Tour tour)//new for guest2
        {
            return _tourReadFromCSVRepository.GetAlternatives(reserved, tour);
        }
        
        public List<Tour> GetFinished(User loggedInGuide)
        {
            return _tourReadFromCSVRepository.GetFinished(loggedInGuide);
        }

        public void GetAttendedGuestsNumber(User loggedInGuide)
        {
            _tour.CalculateAttendedGuestsNumber(loggedInGuide);
        }

        public Tour GetTheMostVisitedTour(User loggedInGuide, string year)
        {
            return _tourReadFromCSVRepository.GetTheMostVisited(loggedInGuide, year);
        }

        public List<Tour> GetGuidesCreated(User loggedInGuide)
        {
            return _tourReadFromCSVRepository.GetGuidesCreated(loggedInGuide);
        }

        public void Update(Tour tour)//new method and deleted edit za sad nam ne treba vrv
        {
            _tour.Update(tour);
        }

        public void UpdateState(Tour selectedTour, ToursState state)
        {
            _tour.UpdateState(selectedTour, state);
        }

        public void SetLanguage(Tour selectedTour, ToursLanguage language)
        {
            _tour.SetLanguage(selectedTour, language);
        }

        public List<Tour> GetFiltered(string citySearchTerm, string countrySearchTerm, int lengthSearchTerm, string guideLanguageSearchTerm, int maxGuestNumberSearchTerm)
        {
            return _tourReadFromCSVRepository.GetFiltered(citySearchTerm, countrySearchTerm, lengthSearchTerm, guideLanguageSearchTerm, maxGuestNumberSearchTerm);
        }

        public void Save()
        {
            _tour.Save();
        }

        public int GetTodaysNumber(User loggedInGuide)
        {
            return _tourReadFromCSVRepository.GetTodaysNumber(loggedInGuide);
        }

        public void CancelAll(User loggedInGuide)
        {
            _tour.CancelAll(loggedInGuide);
        }
    }
}
