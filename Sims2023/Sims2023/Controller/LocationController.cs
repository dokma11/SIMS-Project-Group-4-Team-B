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
        private LocationDAO _locations;
        public LocationController()
        {
            _locations = new LocationDAO();
        }

        public List<Location> GetAllLocations()
        {
            return _locations.GetAll();
        }

        public void Create(Location location)
        {
            _locations.Add(location);
        }
        public void Delete(Location location)
        {
            _locations.Remove(location);
        }
        public void Subscribe(IObserver observer)
        {
            _locations.Subscribe(observer);
        }

    }
}
