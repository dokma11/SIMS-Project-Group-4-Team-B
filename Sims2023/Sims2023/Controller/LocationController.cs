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
            List<Location> locations = new List<Location>();
            locations = _location.GetAll();
            int counter = 0;

            if(locations.Count ==  0) 
            {
                _location.Add(location);
            }
            else
            {
                foreach(var locationInstance in locations)
                {
                    if(location.city == locationInstance.city && location.country == locationInstance.country) 
                    {
                        counter++;
                        break;
                    }
                }
                if(counter == 0)
                {
                    _location.Add(location);
                }
                counter = 0;
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
