using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for CreateTourFromFrequentLocation.xaml
    /// </summary>
    public partial class CreateTourFromFrequentLocationView : Page
    {
        public CreateTourFromFrequentLocationView(Location selectedLocation, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, RequestService requestService, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, User loggedInGuide, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            InitializeComponent();
            DataContext = new CreateTourFromFrequentLocationViewModel(selectedLocation, tourService, locationService, keyPointService, tourReviewService, requestService, tourReservationService, voucherService, userService, countriesAndCitiesService, loggedInGuide, tourNotificationService, complexTourRequestService, subTourRequestService);

            foreach (var date in tourService.GetBusyDates(loggedInGuide))
            {
                requestDatePicker.BlackoutDates.Add(new CalendarDateRange(date, date.AddHours(1)));
            }

            requestDatePicker.DisplayDateStart = DateTime.Today.AddDays(1);
        }
    }
}
