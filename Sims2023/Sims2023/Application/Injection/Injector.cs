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
            { typeof(IUserCSVRepository), new UserCSVRepository() },
            { typeof(IAccommodationCancellationCSVRepository), new AccommodationCancellationCSVRepository() },
            { typeof(IAccommodationGradeCSVRepository), new AccommodationGradeCSVRepository() },
            { typeof(IAccommodationCSVRepository), new AccommodationCSVRepository() },
            { typeof(IAccommodationReservationCSVRepository), new AccommodationReservationCSVRepository() },
            { typeof(IAccommodationReservationReschedulingCSVRepository), new AccommodationReservationReschedulingCSVRepository() },
            { typeof(IGuestGradeCSVRepository), new GuestGradeCSVRepository() },
            { typeof(IKeyPointCSVRepository), new KeyPointCSVRepository() },
            { typeof(ILocationCSVRepository), new LocationCSVRepository() },
            { typeof(ITourWriteToCSVRepository), new TourWriteToCSVRepository() },
            { typeof(ITourReadFromCSVRepository), new TourReadFromCSVRepository() },
            { typeof(ITourReservationCSVRepository), new TourReservationCSVRepository() },
            { typeof(ITourReviewCSVRepository), new TourReviewCSVRepository() },
            { typeof(IVoucherCSVRepository), new VoucherCSVRepository() },
            { typeof(IAccommodationStatisticsCSVRepository), new AccommodationStatisticsCSVRepository() },
            { typeof(ICountriesAndCitiesCSVRepository), new CountriesAndCitiesCSVRepository() },
            { typeof(IAccommodationRenovationCSVRepository), new AccommodationRenovationCSVRepository() }

        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (Injector._implementations.ContainsKey(type))
            {
                return (T)Injector._implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
            return default(T);
        }
    }
}
