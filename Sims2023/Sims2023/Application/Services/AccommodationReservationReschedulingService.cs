using Sims2023.Model.DAO;
using Sims2023.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Repositories;
using System.Collections.ObjectModel;

namespace Sims2023.Application.Services;

public class AccommodationReservationReschedulingService
{
    private AccommodationReservationReschedulingRepository _accommodationReservationRescheduling;

    public AccommodationReservationReschedulingService()
    {
        _accommodationReservationRescheduling = new AccommodationReservationReschedulingRepository();
    }

    public AccommodationReservationRescheduling GetById(int id)
    {
        return _accommodationReservationRescheduling.GetById(id);
    }

    public List<AccommodationReservationRescheduling> GetAllReservationReschedulings()
    {
        return _accommodationReservationRescheduling.GetAll();
    }


    public List<AccommodationReservationRescheduling> GetGuestsReservationMove(User owner, List<AccommodationReservationRescheduling> guests)
    {
        return _accommodationReservationRescheduling.FindGuestsForOwner(owner, guests);
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
    public List<AccommodationReservationRescheduling> FindSuitableReservationReschedulings(User guest1)
    {
        return _accommodationReservationRescheduling.FindSuitableReservationReschedulings( guest1);
    }
    public void checkForNotifications(User guest1)
    {
        _accommodationReservationRescheduling.checkForNotifications(guest1);
    }
}
