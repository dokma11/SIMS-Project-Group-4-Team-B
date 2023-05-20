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
            _countriesAndCities = Injection.Injector.CreateInstance<ICountriesAndCitiesCSVRepository>();
        }

        public List<CountriesAndCities> GetAllLocations()
        {
            return _countriesAndCities.GetAll();
        }

    }
}
