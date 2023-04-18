using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.Repositories;
using Sims2023.Repository;
using System;
using System.Collections.Generic;

namespace Sims2023.Application.Injection
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(IAccommodationCancellationRepository), new AccommodationCancellationRepository() },
            { typeof(IAccommodationGradeRepository), new AccommodationGradeRepository() },
            { typeof(IAccommodationRepository), new AccommodationRepository() },
            { typeof(IAccommodationReservationRepository), new AccommodationReservationRepository() },
            { typeof(IAccommodationReservationReschedulingRepository), new AccommodationReservationReschedulingRepository() },
            { typeof(IGuestGradeRepository), new GuestGradeRepository() },
            { typeof(IKeyPointRepository), new KeyPointRepository() },
            { typeof(ILocationRepository), new LocationRepository() },
            { typeof(ITourWriteToCSVRepository), new TourWriteToCSVRepository() },
            { typeof(ITourReservationRepository), new TourReservationRepository() },
            { typeof(ITourReviewRepository), new TourReviewRepository() },
            { typeof(IVoucherRepository), new VoucherRepository() },
            { typeof(IUserRepository), new UserRepository() }
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
