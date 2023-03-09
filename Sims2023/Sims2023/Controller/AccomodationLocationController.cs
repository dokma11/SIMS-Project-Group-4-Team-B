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
    public class AccomodationLocationController
    {

        private AccommodationLocationsDAO _accomodationLocations;

        public AccomodationLocationController()
        {
            _accomodationLocations = new AccommodationLocationsDAO();
        }

        public List<AccommodationLocation> GetAllAccommodations()
        {
            return _accomodationLocations.GetAll();
        }

        public void Create(AccommodationLocation loc)
        {
            _accomodationLocations.Add(loc);
        }

        public void Delete(AccommodationLocation loc)
        {
            _accomodationLocations.Remove(loc);
        }

        public void Update(AccommodationLocation loc)
        {
            _accomodationLocations.Update(loc);
        }

        public void Subscribe(IObserver observer)
        {
            _accomodationLocations.Subscribe(observer);
        }

    }
}
