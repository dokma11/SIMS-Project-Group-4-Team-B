using Sims2023.Domain.Models;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourReviewCSVRepository
    {
        public int NextId();
        public void Add(TourReview tourReview);
        public void Save();
        public List<TourReview> GetByToursId(int id);
        public void Report(TourReview tourReview);
        public void AddReviewsPictures(string concatenatedPictures, TourReview tourReview);
        public void Subscribe(IObserver observer);
        void NotifyObservers();
        public List<TourReview> GetAll();
    }
}
