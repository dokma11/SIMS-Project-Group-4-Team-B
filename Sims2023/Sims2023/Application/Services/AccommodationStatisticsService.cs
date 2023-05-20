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

        public AccommodationStatisticsService()
        {
            _statistics = Injection.Injector.CreateInstance<IAccommodationStatisticsCSVRepository>();
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
