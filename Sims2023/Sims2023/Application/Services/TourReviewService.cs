﻿using Sims2023.Domain.Models;
using Sims2023.Repositories;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Application.Services
{
    public class TourReviewService
    {
        private TourReviewRepository _tourReviews;

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
    }
}