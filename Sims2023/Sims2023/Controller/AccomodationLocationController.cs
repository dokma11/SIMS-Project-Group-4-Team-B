using Sims2023.DAO;
using Sims2023.Model;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Controller
{
    public class AccomodationLocationController
    {

        private AccommodationLocationsDAO _accomodationLocation;

        public AccomodationLocationController()
        {
            _accomodationLocation = new AccommodationLocationsDAO();
        }

        public List<AccommodationLocation> GetAllAccommodationLocations()
        {
            return _accomodationLocation.GetAll();
        }

        public void Create(AccommodationLocation loc)
        {
            _accomodationLocation.Add(loc);
        }



        public int FindById(AccommodationLocation acmLoc)
        {
            foreach (AccommodationLocation location in GetAllAccommodationLocations())
            {
                if (acmLoc.Id == location.Id) return location.Id;
            }
            return -1;
         }

        public Boolean IsAlreadyThere(AccommodationLocation acmLoc)
        {
            foreach (AccommodationLocation location in GetAllAccommodationLocations())
            {
                if (acmLoc.City == location.City && acmLoc.Country == location.Country) return true;
            }
            return false;

        }

        public int FindIdByCityCountry(string city, string country)
        {
            foreach (AccommodationLocation location in GetAllAccommodationLocations())
            {
                if (location.Country== country && location.City== city) return location.Id;
            }
            return -1;
        }

        public void Delete(AccommodationLocation loc)
        {
            _accomodationLocation.Remove(loc);
        }

        public void Update(AccommodationLocation loc)
        {
            _accomodationLocation.Update(loc);
        }

        public void Subscribe(IObserver observer)
        {
            _accomodationLocation.Subscribe(observer);
        }

    }
}
