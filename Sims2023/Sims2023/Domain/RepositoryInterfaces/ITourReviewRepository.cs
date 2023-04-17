using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        public int NextId();
        public void Add(TourReview tourReview);
        public void Remove(TourReview tourReview);
        public List<TourReview> GetAll();
        public TourReview GetById(int id);
        public void Save();
        public List<TourReview> GetByToursId(int id);
        public void GetKeyPointWhereGuestJoined(Tour selectedTour);
        public void Report(TourReview tourReview);
        public void AddReviewsPictures(string concatenatedPictures, TourReview tourReview);
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);
        void NotifyObservers();
    }
}
