using Sims2023.Application.Injection;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using System.Collections.Generic;

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
