using Sims2023.Domain.Models;
using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.FileHandler
{
    public class TourReviewFileHandler
    {
        private List<TourReview> _tourReviews;
        private readonly Serializer<TourReview> _serializer;
        private const string FilePath = "../../../Resources/Data/tourReviews.csv";

        public TourReviewFileHandler()
        {
            _serializer = new Serializer<TourReview>();
            _tourReviews = _serializer.FromCSV(FilePath);
        }

        public TourReview GetById(int id)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            return _tourReviews.FirstOrDefault(t => t.Id == id);
        }

        public List<TourReview> Load()
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            return _tourReviews;
        }

        public void Save(List<TourReview> tourReviews)
        {
            _serializer.ToCSV(FilePath, tourReviews);
        }
    }
}
