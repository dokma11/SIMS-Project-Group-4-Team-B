﻿using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using Sims2023.Repository;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class TourReviewService
    {
        private ITourReviewRepository _tourReviews;

        public TourReviewService()
        {
            _tourReviews = new TourReviewRepository();
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

        public List<TourReview> GetByToursId(int id)
        {
            return _tourReviews.GetByToursId(id);
        }

        public void Report(TourReview tourReview)
        {
            _tourReviews.Report(tourReview);
        }
        public void GetKeyPointWhereGuestJoined(Tour selectedTour)
        {
            _tourReviews.GetKeyPointWhereGuestJoined(selectedTour);
        }
    }
}
