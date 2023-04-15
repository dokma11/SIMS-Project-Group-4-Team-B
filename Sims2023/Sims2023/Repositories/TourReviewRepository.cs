using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class TourReviewRepository: ITourReviewRepository
    {
        private List<IObserver> _observers;
        private List<TourReview> _tourReviews;
        private TourReviewFileHandler _fileHandler;
        private KeyPointRepository _keyPointRepository;
        public TourReviewRepository()
        {
            _fileHandler = new TourReviewFileHandler();
            _tourReviews = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public int NextId()
        {
            return _tourReviews.Count == 0 ? 1 : _tourReviews.Max(t => t.Id) + 1;
        }

        public void Add(TourReview tourReview)
        {
            tourReview.Id = NextId();
            _tourReviews.Add(tourReview);
            _fileHandler.Save(_tourReviews);
            NotifyObservers();
        }

        public void Remove(TourReview tourReview)
        {
            _tourReviews.Remove(tourReview);
            _fileHandler.Save(_tourReviews);
            NotifyObservers();
        }

        public List<TourReview> GetAll()
        {
            return _tourReviews;
        }

        public TourReview GetById(int id)
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
            _fileHandler.Save(_tourReviews);
        }

        public List<TourReview> GetByToursId(int id)
        {
            return _tourReviews.Where(tr => tr.Tour.Id == id).ToList();
        }

        public void Report(TourReview tourReview)
        {
            tourReview.IsValid = false;
        }

        public void GetKeyPointWhereGuestJoined(Tour selectedTour)
        {
            _keyPointRepository = new();
            foreach (var tourReview in _tourReviews)
            {
                var keyPoint = _keyPointRepository.GetAll().Where(k => k.Tour.Id == selectedTour.Id)
                                                  .FirstOrDefault(k => k.ShowedGuestsIds.Contains(tourReview.Guest.Id));
                if (keyPoint != null)
                {
                    tourReview.KeyPointJoined = keyPoint;
                    Save();
                }
            }
        }
    }
}
