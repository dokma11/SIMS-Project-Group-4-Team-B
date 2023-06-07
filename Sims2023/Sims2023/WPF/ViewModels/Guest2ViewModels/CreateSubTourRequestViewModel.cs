﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class CreateSubTourRequestViewModel
    {
        public CountriesAndCitiesService _countriesAndCitiesService;
        public TourRequest Request { get; set; }
        public Location Location { get; set; }
        public LocationService _locationService { get; set; }
        public RequestService _requestService { get; set; }
        public SubTourRequestService _subTourRequestService { get; set; }
        public SubTourRequest SubTourRequest { get; set; }
        public ComplexTourRequest ComplexTourRequest { get; set; }

        public User User { get; set; }


        public CreateSubTourRequestViewModel(ComplexTourRequest complexTourRequest,User user)
        {
            _countriesAndCitiesService = new CountriesAndCitiesService();
            _locationService = new LocationService();
            _requestService = new RequestService();
            _subTourRequestService = new SubTourRequestService();
            Request = new TourRequest();
            SubTourRequest = new SubTourRequest();
            Location = new Location();
            ComplexTourRequest = complexTourRequest;

            _requestService.CheckExpirationDate(user);




        }

        public List<CountriesAndCities> GetCitiesAndCountries()
        {
            return _countriesAndCitiesService.GetAllLocations();
        }

        public void ConfirmCreation(string country, string city, DateTime startDate, DateTime endDate, string description, User user, RequestsLanguage language, int guestNumber)
        {
            Location.City = city;
            Location.Country = country;
            _locationService.CheckExistance(Location);
            TourRequest Request = new TourRequest(Location, description, language, guestNumber, startDate, endDate, user);
            _requestService.Create(Request);
            
            SubTourRequest SubTourRequest = new SubTourRequest(Request,ComplexTourRequest);
           
            
            _subTourRequestService.Create(SubTourRequest);
           




        }
    }
}
