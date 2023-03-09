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

        private AccommodationLocationsDAO _accomodationLocations;

        public AccomodationLocationController()
        {
            _accomodationLocations = new AccommodationLocationsDAO();
        }

        public List<AccommodationLocation> GetAllAccommodationLocations()
        {
            return _accomodationLocations.GetAll();
        }

        public void Create(AccommodationLocation loc)
        {
            _accomodationLocations.Add(loc);
        }



        public int FindById(AccommodationLocation acmLoc)
        {
            foreach (AccommodationLocation location in GetAllAccommodationLocations())
            {
                if (acmLoc.id == location.id) return location.id;
            }
            return -1;
         }

        public Boolean isAlreadyThere(AccommodationLocation acmLoc)
        {
            foreach (AccommodationLocation location in GetAllAccommodationLocations())
            {
                if (acmLoc.city == location.city && acmLoc.country == location.country) return true;
            }
            return false;

        }

        public int findIdByCityCountry(string city, string country)
        {
            foreach (AccommodationLocation location in GetAllAccommodationLocations())
            {
                if (location.country== country && location.city== city) return location.id;
            }
            return -1;
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
