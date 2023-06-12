using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourReviewService
    {
        private ITourReviewCSVRepository _tourReviews;
        private ITourReadFromCSVRepository _tour;
        private IUserCSVRepository _user;
        private IKeyPointCSVRepository _keyPoint;

        public TourReviewService()
        {
            _tourReviews = Injection.Injector.CreateInstance<ITourReviewCSVRepository>();
            _tour = Injection.Injector.CreateInstance<ITourReadFromCSVRepository>();
            _user = Injection.Injector.CreateInstance<IUserCSVRepository>();
            _keyPoint = Injection.Injector.CreateInstance<IKeyPointCSVRepository>();

            GetTourReferences();
            GetUserReferences();
            GetKeyPointReferences();
        }

        public List<TourReview> GetAll()
        {
            return _tourReviews.GetAll();
        }

        public void Create(TourReview tourReview)
        {
            _tourReviews.Add(tourReview);
            Save();
        }

        public void Subscribe(IObserver observer)
        {
            _tourReviews.Subscribe(observer);
        }

        public void Save()
        {
            _tourReviews.Save();
            GetTourReferences();
            GetUserReferences();
            GetKeyPointReferences();
        }

        public void AddReviewsPictures(List<string> pictures, TourReview tourReview)
        {
            string picturesString = string.Join("!", pictures);
            _tourReviews.AddReviewsPictures(picturesString, tourReview);
            Save();
        }

        public List<TourReview> GetByToursId(int id)
        {
            return _tourReviews.GetByToursId(id);
        }

        public void Report(TourReview tourReview)
        {
            _tourReviews.Report(tourReview);
            Save();
        }

        public void GetTourReferences()
        {
            foreach (var review in GetAll())
            {
                review.Tour = _tour.GetById(review.Tour.Id) ?? review.Tour;
            }
        }

        public void GetUserReferences()
        {
            foreach (var review in GetAll())
            {
                review.Guest = _user.GetById(review.Guest.Id) ?? review.Guest;
            }
        }

        public void GetKeyPointReferences()
        {
            foreach (var review in GetAll())
            {
                review.KeyPointJoined = _keyPoint.GetById(review.KeyPointJoined.Id) ?? review.KeyPointJoined;
            }
        }
    }
}

