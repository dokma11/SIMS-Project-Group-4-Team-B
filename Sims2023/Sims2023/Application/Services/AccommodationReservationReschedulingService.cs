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

namespace Sims2023.Application.Services;

public class AccommodationReservationReschedulingService
{
    private IAccommodationReservationReschedulingRepository _accommodationReservationRescheduling;

    public AccommodationReservationReschedulingService()
    {
        _accommodationReservationRescheduling = new AccommodationReservationReschedulingRepository();
        //_accommodationReservationRescheduling = Injection.Injector.CreateInstance<IAccommodationReservationReschedulingRepository>();
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
