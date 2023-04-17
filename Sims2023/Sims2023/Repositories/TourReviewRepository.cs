using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using System.Collections.Generic;
using System.Linq;

namespace Sims2023.Repositories
{
    public class TourReviewRepository : ITourReviewRepository
    {
        private List<IObserver> _observers;
        private List<TourReview> _tourReviews;
        private TourReviewFileHandler _fileHandler;
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

        public void AddReviewsPictures(string concatenatedPictures, TourReview tourReview)
        {

            tourReview.ConcatenatedPictures = concatenatedPictures;

            _fileHandler.Save(_tourReviews);
            NotifyObservers();
        }

        public List<TourReview> GetByToursId(int id)
        {
            return _tourReviews.Where(tr => tr.Tour.Id == id).ToList();
        }

        public void Report(TourReview tourReview)
        {
            tourReview.IsValid = false;
        }
    }
}
