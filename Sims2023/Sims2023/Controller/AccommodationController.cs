using Sims2023.DAO;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Controller
{
    public class AccommodationController
    {
        private AccommodationDAO _accomodations;

        public AccommodationController()
        {
            _accomodations = new AccommodationDAO();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return _accomodations.GetAll();
        }

        public void Create(Accommodation accommodation)
        {
            _accomodations.Add(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accomodations.Remove(accommodation);
        }

        public void Update(Accommodation accommodation)
        {
            _accomodations.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            _accomodations.Subscribe(observer);
        }


    }
}
