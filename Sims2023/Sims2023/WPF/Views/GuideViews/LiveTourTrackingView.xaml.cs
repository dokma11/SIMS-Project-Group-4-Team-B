using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class LiveTourTrackingView : Page
    {
        public LiveTourTrackingView(Tour selectedTour, KeyPointService keyPointService, TourReservationService tourReservationService, UserService userService, TourService tourService, TourReviewService tourReviewService, RequestService requestService, User loggedInGuide, LocationService locationService, VoucherService voucherService, CountriesAndCitiesService countriesAndCitiesService, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            InitializeComponent();
            DataContext = new LiveTourTrackingViewModel(selectedTour, tourService, locationService, keyPointService, tourReviewService, requestService, tourReservationService, voucherService, userService, countriesAndCitiesService, loggedInGuide, tourNotificationService, complexTourRequestService, subTourRequestService);
        }
    }
}
