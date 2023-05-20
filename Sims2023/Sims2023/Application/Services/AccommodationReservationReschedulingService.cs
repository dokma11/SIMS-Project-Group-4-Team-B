using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Repositories;
using System.Collections.ObjectModel;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Application.Injection;
using System.Windows;

namespace Sims2023.Application.Services;

public class AccommodationReservationReschedulingService
{
    private IUserCSVRepository _user;
    private ILocationCSVRepository _location;
    private IAccommodationCSVRepository _accommodation;
    private IAccommodationReservationCSVRepository _accommodationReservation;
    private IAccommodationReservationReschedulingCSVRepository _accommodationReservationRescheduling;

    public AccommodationReservationReschedulingService()
    {
        _user = Injector.CreateInstance<IUserCSVRepository>();
        _accommodation = Injector.CreateInstance<IAccommodationCSVRepository>();
        _accommodationReservation = Injector.CreateInstance<IAccommodationReservationCSVRepository>();
        _location = Injector.CreateInstance<ILocationCSVRepository>();
        _accommodationReservationRescheduling = Injector.CreateInstance<IAccommodationReservationReschedulingCSVRepository>();
        GetReservationReferences();
    }

    public void GetReservationReferences()
    {
        foreach (var item in GetAllReservationReschedulings())
        {
            var accommodationReservation = _accommodationReservation.GetById(item.AccommodationReservation.Id);
            var accommodation = _accommodation.GetById(accommodationReservation.Accommodation.Id);
            var location = _location.GetById(accommodation.Location.Id);
            var guest = _user.GetById(accommodationReservation.Guest.Id);
            var owner = _user.GetById(accommodation.Owner.Id);
            if (accommodationReservation != null)
            {
                item.AccommodationReservation = accommodationReservation;
                item.AccommodationReservation.Accommodation.Owner = owner;
                item.AccommodationReservation.Guest = guest;
                item.AccommodationReservation.Accommodation.Location = location;
                item.AccommodationReservation.Guest.Name = guest.Name;
                item.AccommodationReservation.Guest.Surname = guest.Surname;
                item.AccommodationReservation.Accommodation.Name = accommodation.Name;

            }
        }
    }

    public AccommodationReservationRescheduling GetById(int id)
    {
        return _accommodationReservationRescheduling.GetById(id);
    }
    public void Save()
    {
        _accommodationReservationRescheduling.Save();
    }

    public List<AccommodationReservationRescheduling> GetAllReservationReschedulings()
    {
        return _accommodationReservationRescheduling.GetAll();
    }


    public List<AccommodationReservationRescheduling> GetGuestsForOwner(User owner, List<AccommodationReservationRescheduling> guests)
    {
        return _accommodationReservationRescheduling.GetGuestsForOwner(owner, guests);
    }

    public bool isAccommodationFree(AccommodationReservationRescheduling request)
    {
        return _accommodationReservationRescheduling.IsDateSpanAvailable(request);
    }

    public void Create(AccommodationReservationRescheduling reservationRescheduling)
    {
        _accommodationReservationRescheduling.Add(reservationRescheduling);
    }

    public void Delete(AccommodationReservationRescheduling reservationRescheduling)
    {
        _accommodationReservationRescheduling.Remove(reservationRescheduling);
    }

    public void Update(AccommodationReservationRescheduling reservationRescheduling)
    {
        _accommodationReservationRescheduling.Update(reservationRescheduling);
    }

    public void Subscribe(IObserver observer)
    {
        _accommodationReservationRescheduling.Subscribe(observer);
    }
    public ObservableCollection<AccommodationReservationRescheduling> FindSuitableReservationReschedulings(User guest1, ObservableCollection<AccommodationReservationRescheduling> accommodationReservationReschedulings)
    {
        return _accommodationReservationRescheduling.FindSuitableReservationReschedulings( guest1,accommodationReservationReschedulings);
    }

    public bool CheckForActiveRequest(AccommodationReservation selectedAccommodationReservation)
    {
        return _accommodationReservationRescheduling.CheckForActiveRequest(selectedAccommodationReservation);
    }
}
