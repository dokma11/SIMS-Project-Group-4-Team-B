using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class CreateTourRequestViewModel
    {
        public CountriesAndCitiesService _countriesAndCitiesService;
        public Request Request { get; set; }
        public Location Location { get; set; }
        public LocationService _locationService { get; set; }

        public RequestService _requestService { get; set; }

        public User User { get; set; }

        
        public CreateTourRequestViewModel()
        {
            _countriesAndCitiesService = new CountriesAndCitiesService();
            _locationService = new LocationService();
            _requestService = new RequestService();
            Request = new Request();
            Location = new Location();
            
            

            
        }

        public List<CountriesAndCities> GetCitiesAndCountries()
        {
            return _countriesAndCitiesService.GetAllLocations();
        }

        public void ConfirmCreation(string country, string city, DateTime startDate, DateTime endDate,string description,User user,RequestsLanguage language,int guestNumber)
        {
            Location.City = city;
            Location.Country = country;
            _locationService.CheckExistance(Location);
            Request Request = new Request(Location, description, language, guestNumber, startDate, endDate, user);
            _requestService.Create(Request);
            
            
            
            
            
        }


    }
}
