using Sims2023.Model.DAO;
using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Observer;
using System.Windows;

namespace Sims2023.Controller
{
    public class TourController
    {
        private TourDAO _tour;
        private LocationDAO _location;
        public TourController()
        {
            _tour = new TourDAO();
            _location = new LocationDAO();
        }

        public List<Tour> GetAllTours()
        {
            return _tour.GetAll();
        }

        public void Create(Tour tour, List<DateTime> dateTimes, Location location)
        {
            _tour.Add(tour, dateTimes, location);
        }
        public void AddToursLocation(Tour tour, Location location, int newToursNumber) 
        {
            List<Location> locations = _location.GetAll();
            int counter = 0;
            int toursId = tour.Id - newToursNumber + 1;   

            if (location.City == null || location.Country == null)
            {
                MessageBox.Show("Unesite lokaciju ");
            }

            if (locations.Count == 0)
            {
                for (int i = 0; i < newToursNumber; i++)
                {
                    _tour.AddToursLocation(toursId, location);
                    toursId++;
                }
            }
            else
            {
                //Checking if the location already exists
                for (int i = 0; i < newToursNumber; i++)
                {
                    foreach (var locationInstance in locations)
                    {
                        if (location.City == locationInstance.City && location.Country == locationInstance.Country)
                        {
                            counter++;
                            _tour.AddToursLocation(toursId, locationInstance);
                            break;
                        }
                    }
                    if (counter == 0)
                    {
                        _tour.AddToursLocation(toursId, location);
                    }
                    toursId++;
                }
            }
        }
        public void AddToursKeyPoints(List<string> keyPoints, int firstToursId)
        {
            string keyPointsString = String.Join(",", keyPoints);
            _tour.AddToursKeyPoints(keyPointsString, firstToursId);
        }
        public void Delete(Tour tour)
        {
            _tour.Remove(tour);
        }
        public void Subscribe(IObserver observer)
        {
            _tour.Subscribe(observer);
        }

    }
}
