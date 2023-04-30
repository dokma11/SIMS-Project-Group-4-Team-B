using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims2023.Repositories
{
    internal class AccommodationRenovationCSVRepository : IAccommodationRenovationCSVRepository
    {
        private List<IObserver> _observers;

        private AccommodationRenovationFileHandler _fileHandler;
        private List<AccommodationRenovation> _renovations;

        public AccommodationRenovationCSVRepository()
        {
            _fileHandler = new AccommodationRenovationFileHandler();
            _renovations = _fileHandler.Load();
            UpdateStatus(_renovations);
          
        }
        public int NextId()
        {
            if (_renovations.Count == 0) return 1;
            return _renovations.Max(s => s.Id) + 1;
        }

        public void Add(AccommodationRenovation grade)
        {
            grade.Id = NextId();
            _renovations.Add(grade);
            _fileHandler.Save(_renovations);
       
        }



        public void Remove(AccommodationRenovation renovation)
        {
            _renovations.Remove(renovation);
            _fileHandler.Save(_renovations);
        }

        public void Update(AccommodationRenovation renovation)
        {
            int index = _renovations.FindIndex(p => p.Id == renovation.Id);
            if (index != -1)
            {
                _renovations[index] = renovation;
            }

            _fileHandler.Save(_renovations);
         
        }

        public List<AccommodationRenovation> GetAll()
        {
            UpdateStatus(_renovations);
            return _renovations;
        }

        public void UpdateStatus(List<AccommodationRenovation> _renovations)
        {
            List<AccommodationRenovation> updateRenovations = new List<AccommodationRenovation>();
            foreach (AccommodationRenovation renovation in _renovations)
            {
                if (renovation.EndDate < DateTime.Today)
                {
                    renovation.Status = "završeno";
                    updateRenovations.Add(renovation);
                }
            }

            foreach (AccommodationRenovation renovation in updateRenovations)
            {
                Update(renovation);
            }
        }

      
    }
}
