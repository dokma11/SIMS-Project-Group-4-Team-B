using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourService
    {
        private ITourWriteToCSVRepository _tour;
        private ILocationCSVRepository _location;
        private ITourReadFromCSVRepository _tourReadFromCSVRepository;
        private IUserCSVRepository _user;

        public TourService()
        {
            _tour = Injection.Injector.CreateInstance<ITourWriteToCSVRepository>();
            _location = Injection.Injector.CreateInstance<ILocationCSVRepository>();
            _tourReadFromCSVRepository = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();

            GetLocationReferences();
            GetUserReferences();
        }

        public List<Tour> GetAll()
        {
            return _tourReadFromCSVRepository.GetAll();
        }

        public void Create(Tour tour, List<DateTime> dateTimes, Domain.Models.Location location, User loggedInGuide)
        {
            _tour.Add(tour, dateTimes, location, loggedInGuide);
            Save();
        }

        public void UpdateAvailableSpace(int reservedSpace, Tour tour)//new for guest2
        {
            _tour.UpdateAvailableSpace(reservedSpace, tour);
            Save();
        }

        public void AddToursLocation(Tour tour, Domain.Models.Location location, int newToursNumber)
        {
            _tour.DecideLocationToAdd(tour, location, newToursNumber, _location.GetAll());
            Save();
        }

        public void AddToursKeyPoints(List<string> keyPoints, int firstToursId)
        {
            string keyPointsString = string.Join(",", keyPoints);
            _tour.AddKeyPoints(keyPointsString, firstToursId);
            Save();
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
            List<Tour> ret = _tourReadFromCSVRepository.GetFinished(loggedInGuide);
            Save();
            return ret;
        }

        public Tour GetTheMostVisitedTour(User loggedInGuide, string year)
        {
            return _tourReadFromCSVRepository.GetTheMostVisited(loggedInGuide, year);
        }

        public List<Tour> GetGuidesCreated(User loggedInGuide)
        {
            List<Tour> ret = _tourReadFromCSVRepository.GetGuidesCreated(loggedInGuide);
            Save();
            return ret;
        }

        public void Update(Tour tour)//new method and deleted edit za sad nam ne treba vrv
        {
            _tour.Update(tour);
            Save();
        }

        public void UpdateState(Tour selectedTour, ToursState state)
        {
            _tour.UpdateState(selectedTour, state);
        }

        public void SetLanguage(Tour selectedTour, ToursLanguage language)
        {
            _tour.SetLanguage(selectedTour, language);
            Save();
        }

        public List<Tour> GetFiltered(string citySearchTerm, string countrySearchTerm, string lengthSearchTerm, string guideLanguageSearchTerm, int maxGuestNumberSearchTerm)
        {
            return _tourReadFromCSVRepository.GetFiltered(citySearchTerm, countrySearchTerm, lengthSearchTerm, guideLanguageSearchTerm, maxGuestNumberSearchTerm);
        }

        public void Save()
        {
            _tour.Save();
            _tourReadFromCSVRepository.Save();
            GetLocationReferences();
            GetUserReferences();
        }

        public int GetTodaysNumber(User loggedInGuide)
        {
            return _tourReadFromCSVRepository.GetTodaysNumber(loggedInGuide);
        }

        public void CancelAll(User loggedInGuide)
        {
            _tour.CancelAll(loggedInGuide);
        }

        public void GetLocationReferences()
        {
            foreach (var tour in GetAll())
            {
                tour.Location = _location.GetById(tour.Location.Id) ?? tour.Location;
            }
        }

        public void GetUserReferences()
        {
            foreach (var tour in GetAll())
            {
                tour.Guide = _user.GetById(tour.Guide.Id) ?? tour.Guide;
            }
        }

        public List<DateTime> GetBusyDates(User loggedInGuide)
        {
            return _tourReadFromCSVRepository.GetBusyDates(loggedInGuide);  
        }
    }
}
