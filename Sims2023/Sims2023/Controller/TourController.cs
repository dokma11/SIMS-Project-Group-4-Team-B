using Sims2023.Model;
using Sims2023.Model.DAO;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        public void Edit(Tour tour, Tour oldTour)
        {
            _tour.Remove(oldTour);
            _tour.AddEdited(tour);
        }
       
        public void AddToursLocation(Tour tour, Location location, int newToursNumber) 
        {
            List<Location> locations = _location.GetAll();
            int toursId = tour.Id - newToursNumber + 1;

            if (location.City == null || location.Country == null)
            {
                MessageBox.Show("Lokacija nije uneta");
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
                CheckIfLocationExists(newToursNumber, locations, location, toursId);
            }
        }

        private void CheckIfLocationExists(int newToursNumber, List<Location> locations, Location location, int toursId)
        {
            int counter = 0;
            for (int i = 0; i < newToursNumber; i++)
            {
                foreach (var locationInstance in locations)
                {
                    //if location exists just add the already existing one
                    if (location.City == locationInstance.City && location.Country == locationInstance.Country)
                    {
                        counter++;
                        _tour.AddToursLocation(toursId, locationInstance);
                        break;
                    }
                }
                //if it doesn't exist add the newly created one
                if (counter == 0)
                {
                    _tour.AddToursLocation(toursId, location);
                }
                toursId++;
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

        public void Save()
        {
            _tour.Save();
        }
    }
}
