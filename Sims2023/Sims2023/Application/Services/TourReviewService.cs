using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourReviewService
    {
        private ITourReviewRepository _tourReviews;

        public TourReviewService()
        {
            _tourReviews = new TourReviewRepository();
            //_tourReviews = Injection.Injector.CreateInstance<ITourReviewRepository>();
        }

        public List<TourReview> GetAllTourReviews()
        {
            return _tourReviews.GetAll();
        }

        public void Create(TourReview tourReview)
        {
            _tourReviews.Add(tourReview);
        }

        public void Delete(TourReview tourReview)
        {
            _tourReviews.Remove(tourReview);
        }

        public void Subscribe(IObserver observer)
        {
            _tourReviews.Subscribe(observer);
        }

        public void Save()
        {
            _tourReviews.Save();
        }

        public TourReview GetById(int id)
        {
            return _tourReviews.GetById(id);
        }
        public void AddReviewsPictures(List<string> pictures, TourReview tourReview)
        {
            string picturesString = string.Join("!", pictures);
            _tourReviews.AddReviewsPictures(picturesString, tourReview);
        }

        public List<TourReview> GetByToursId(int id)
        {
            return _tourReviews.GetByToursId(id);
        }

        public void Report(TourReview tourReview)
        {
            _tourReviews.Report(tourReview);
        }
    }
}

