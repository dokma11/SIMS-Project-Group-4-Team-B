﻿using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.Application.Services
{
    public class AccommodationService
    {
        private AccommodationRepository _accomodation;

        public AccommodationService()
        {
            _accomodation = new AccommodationRepository();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return _accomodation.GetAll();
        }

        public void Create(Accommodation accommodation)
        {
            _accomodation.Add(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {
            _accomodation.Remove(accommodation);
        }

        public void Update(Accommodation accommodation)
        {
            _accomodation.Update(accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            _accomodation.Subscribe(observer);
        }
        public Accommodation GetById(int id)
        {
            return _accomodation.GetById(id);
        }

    }
}