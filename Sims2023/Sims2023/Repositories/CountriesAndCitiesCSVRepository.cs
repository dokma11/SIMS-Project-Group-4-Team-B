using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;
using Sims2023.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Repositories
{
    public class CountriesAndCitiesCSVRepository: ICountriesAndCitiesCSVRepository
    {
        

        private List<IObserver> _observers;
        private CountriesAndCitiesFileHandler _fileHandler;
        private List<CountriesAndCities> _locations;


        public CountriesAndCitiesCSVRepository()
        {
            _fileHandler = new CountriesAndCitiesFileHandler();
            _locations = _fileHandler.Load();
        }

        public List<CountriesAndCities> GetAll()
        {
            return _locations;
        }

    }
}
