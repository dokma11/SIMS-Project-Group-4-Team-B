using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Observer;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        public int NextId();
        public void Add(Tour tour, List<DateTime> dateTimes, Location location, User loggedInGuide);
        public void Update(Tour tour);
        public void AddToursLocation(int toursId, Location location);
        public bool CanRateTour(Tour tour);
        public bool CanSeeTour(Tour tour);
        public void CheckIfLocationExists(int newToursNumber, List<Location> locations, Location location, int toursId);
        public void CheckAddToursLocation(Tour tour, Location location, int newToursNumber, List<Location> locations);
        public void AddLocationsToTour(ObservableCollection<Location> locations, ObservableCollection<Tour> tours);
        public void AddToursKeyPoints(string keyPointsString, int firstToursId);
        public void Remove(Tour tour);
        public List<Tour> GetAll();
        public List<Tour> GetAvailable();
        public List<Tour> GetAlternative(int reserveSpace, Tour tour);
        public Tour GetById(int id);
        public void UpdateAvailableSpace(int reservedSpace, Tour tour);
        public void Save();




    }
}
