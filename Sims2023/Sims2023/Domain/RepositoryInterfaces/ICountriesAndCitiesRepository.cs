using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ICountriesAndCitiesRepository
    {
        public List<CountriesAndCities> GetAll();

    }
}
