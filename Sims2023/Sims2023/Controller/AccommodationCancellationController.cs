using Sims2023.Model.DAO;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Controller
{
    public class AccommodationCancellationController
    {
        private AccommodationCancellationDAO _accommodationCancellation;

        public AccommodationCancellationController()
        {
            _accommodationCancellation = new AccommodationCancellationDAO();
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
    }
}
