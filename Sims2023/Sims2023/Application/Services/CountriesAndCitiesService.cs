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
        private ICountriesAndCitiesRepository _countriesAndCities;

        public CountriesAndCitiesService()
        {
            _countriesAndCities = new CountriesAndCitiesRepository();
            //_countriesAndCities = Injection.Injector.CreateInstance<ICountriesAndCitiesRepository>();
        }

        public List<CountriesAndCities> GetAllLocations()
        {
            return _countriesAndCities.GetAll();
        }
       
    }
}
