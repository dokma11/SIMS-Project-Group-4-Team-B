using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Application.Services
{
    public class AccommodationStatisticsService
    {
        private IAccommodationStatisticsCSVRepository _statistics;
        private IAccommodationCSVRepository _accommodations;

        public AccommodationStatisticsService()
        {
            _accommodations = Injection.Injector.CreateInstance<IAccommodationCSVRepository>();
            //_statistics = new AccommodationStatisticsCSVRepository();
            _statistics = Injection.Injector.CreateInstance<IAccommodationStatisticsCSVRepository>();
            FindForeignAtributes();
        }

        private void FindForeignAtributes()
        {
            foreach (var item in _statistics.GetAll())
            {
                item.Accommodation = _accommodations.GetById(item.Accommodation.Id);
            }
        }
        public List<AccommodationStatistics> GetAll()
        {
            return _statistics.GetAll();
        }

        public void Create(AccommodationStatistics grade)
        {
            _statistics.Add(grade);
        }

        public void Subscribe(IObserver observer)
        {
            _statistics.Subscribe(observer);
        }
    }
}
