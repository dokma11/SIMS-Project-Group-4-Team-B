using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        public int NextId();
        public void Add(TourReview tourReview);
        public TourReview GetById(int id);
        public List<TourReview> GetAll();
        public void Remove(TourReview tourReview);
        public void Save();
        public void AddReviewsPictures(string picturesString, TourReview tourReview);

    }
}
