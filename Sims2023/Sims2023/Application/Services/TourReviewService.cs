using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourReviewService
    {
        private ITourReviewCSVRepository _tourReviews;

        public TourReviewService()
        {
            _tourReviews = new TourReviewCSVRepository();
            //_tourReviews = Injection.Injector.CreateInstance<ITourReviewRepository>();
        }

        public void Create(TourReview tourReview)
        {
            _tourReviews.Add(tourReview);
        }

        public void Subscribe(IObserver observer)
        {
            _tourReviews.Subscribe(observer);
        }

        public void Save()
        {
            _tourReviews.Save();
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

