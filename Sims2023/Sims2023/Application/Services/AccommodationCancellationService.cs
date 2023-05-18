using Sims2023.Application.Injection;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Observer;
using Sims2023.Repositories;
using System.Collections.Generic;

namespace Sims2023.Application.Services
{
    public class AccommodationCancellationService
    {
        private IAccommodationCancellationCSVRepository _accommodationCancellation;

        public AccommodationCancellationService()
        {
           _accommodationCancellation = new AccommodationCancellationCSVRepository();
           //_accommodationCancellation = Injector.CreateInstance<IAccommodationCancellationCSVRepository>();
        }

        public List<AccommodationCancellation> GetAllAccommodationCancellations()
        {
            return _accommodationCancellation.GetAll();
        }

        public void Create(AccommodationCancellation accommodationCancellation)
        {
            _accommodationCancellation.Add(accommodationCancellation);
        }

        public void Delete(AccommodationCancellation accommodationCancellation)
        {
            _accommodationCancellation.Remove(accommodationCancellation);
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationCancellation.Subscribe(observer);
        }

        public AccommodationCancellation GetById(int id)
        {
            return _accommodationCancellation.GetById(id);
        }

        public void Update(AccommodationCancellation cancellation)
        {
            _accommodationCancellation.Update(cancellation);
        }
    }
}
