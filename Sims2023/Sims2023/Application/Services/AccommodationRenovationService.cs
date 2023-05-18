using Sims2023.Application.Injection;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.Application.Services
{
    public class AccommodationRenovationService
    {
        private IAccommodationRenovationCSVRepository _accommodationRenovation;

        public AccommodationRenovationService()
        {
            _accommodationRenovation = new AccommodationRenovationCSVRepository();
            //_accommodationRenovation = Injector.CreateInstance<IAccommodationRenovationCSVRepository>();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _accommodationRenovation.GetAll();
        }

        public void Create(AccommodationRenovation renovation)
        {
            _accommodationRenovation.Add(renovation);
        }

        public void Delete(AccommodationRenovation renovation)
        {
           _accommodationRenovation.Remove(renovation);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationRenovation.Subscribe(observer);
        }

    }
}
