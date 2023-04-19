using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class TourReadFromCSVRepository : ITourReadFromCSVRepository
    {
        private List<IObserver> _observers;
        private List<Tour> _tours;
        private TourFileHandler _fileHandler;
        public TourReadFromCSVRepository()
        {
            _fileHandler = new TourFileHandler();
            _tours = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public List<Tour> GetAvailable()//new method for guest2
        {
            List<Tour> available = new List<Tour>();
            foreach (var tourInstance in _tours)
            {
                if (tourInstance.CurrentState == ToursState.Created)
                {
                    available.Add(tourInstance);
                }
            }
            return available;
        }

        public List<Tour> GetAlternative(int reserveSpace, Tour tour)//new method for guest2
        {
            var alternativeTours = _tours
                .Where(tour => tour.Location.Id == tour.Location.Id && tour.AvailableSpace >= reserveSpace && tour.CurrentState == ToursState.Created)
                .ToList();

            return alternativeTours;
        }

        public List<Tour> GetFinished(User loggedInGuide)
        {
            _tours = _fileHandler.Load();
            return _tours.Where(t => (t.CurrentState == ToursState.Finished || t.CurrentState == ToursState.Interrupted)
                         && t.Guide.Id == loggedInGuide.Id).ToList();
        }

        public Tour GetTheMostVisited(User loggedInGuide, string year)
        {
            Tour ret = new();
            var tours = _tours.Where(tour => tour.Guide.Id == loggedInGuide.Id &&
                        (tour.CurrentState == ToursState.Finished || tour.CurrentState == ToursState.Interrupted));
            if (year == "Svih vremena")
            {
                return tours.OrderByDescending(tour => tour.AttendedGuestsNumber).FirstOrDefault();
            }
            else
            {
                if (tours.Where(tour => tour.Start.Year.ToString() == year) != null)
                {
                    return tours.Where(tour => tour.Start.Year.ToString() == year)
                             .OrderByDescending(tour => tour.AttendedGuestsNumber).FirstOrDefault();
                }
                else
                {
                    return ret;
                }
            }
        }

        public List<Tour> GetGuidesCreated(User loggedInGuide)
        {
            //_tours = _fileHandler.Load();
            return _tours.Where(tour => tour.CurrentState == ToursState.Created && tour.Guide.Id == loggedInGuide.Id).ToList();
        }

        public List<Tour> GetFiltered(string citySearchTerm, string countrySearchTerm, string lengthSearchTerm, string guideLanguageSearchTerm, int maxGuestNumberSearchTerm)
        {
            List<Tour> FilteredData = GetAvailable().Where(tour =>
                (string.IsNullOrEmpty(citySearchTerm) || tour.Location.City.ToLower().Contains(citySearchTerm)) &&
                (string.IsNullOrEmpty(countrySearchTerm) || tour.Location.Country.ToLower().Contains(countrySearchTerm)) &&
                (string.IsNullOrEmpty(lengthSearchTerm) || tour.Length.ToString().ToLower().Contains(lengthSearchTerm)) &&
                (string.IsNullOrEmpty(guideLanguageSearchTerm) || tour.GuideLanguage.ToString().ToLower().Contains(guideLanguageSearchTerm)) &&
                tour.MaxGuestNumber >= maxGuestNumberSearchTerm
            ).ToList();

            return FilteredData;
        }

        public Uri GetPictureUri(Tour tour, int i)
        {

            Uri imageUri = new Uri(tour.Pictures[i], UriKind.RelativeOrAbsolute);
            return imageUri;
        }

        public Tour GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Save()
        {
            _fileHandler.Save(_tours);
        }
    }
}
