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
    public class LocationController
    {
        private LocationDAO _location;
        public LocationController()
        {
            _location = new LocationDAO();
        }

        public List<Location> GetAllLocations()
        {
            return _location.GetAll();
        }

        public void Create(Location location)
        {
            List<Location> locations = _location.GetAll();
            int counter = 0;

            if(locations.Count ==  0) 
            {
                _location.Add(location);
            }
            else
            {
                //if the location already exists, there is no need to recreate it
                foreach (var locationInstance in locations)
                {
                    if(location.City == locationInstance.City && location.Country == locationInstance.Country) 
                    {
                        counter++;
                        location.Id = locationInstance.Id;
                        break;
                    }
                }
                //this means that it doesn't exist, counter increases if it finds a location with same city name and country name
                if(counter == 0)
                {
                    _location.Add(location);
                }
            }
        }
        public void Delete(Location location)
        {
            _location.Remove(location);
        }
        public void Subscribe(IObserver observer)
        {
            _location.Subscribe(observer);
        }

    }
}
