using Sims2023.Application.Injection;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Application.Services
{
    public class CountriesAndCitiesService
    {
        private ICountriesAndCitiesCSVRepository _countriesAndCities;

        public CountriesAndCitiesService()
        {
           // _countriesAndCities = new CountriesAndCitiesCSVRepository();
            _countriesAndCities = Injector.CreateInstance<ICountriesAndCitiesCSVRepository>();
        }

        public List<CountriesAndCities> GetAllLocations()
        {
            return _countriesAndCities.GetAll();
        }
       
    }
}
