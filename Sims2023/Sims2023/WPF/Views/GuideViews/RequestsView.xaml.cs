using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for RequestStatisticsView.xaml
    /// </summary>
    public partial class RequestsView : Page
    {
        public RequestsView(RequestService requestService, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, User loggedInGuide, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            InitializeComponent();
            DataContext = new RequestsViewModel(requestService, tourService, locationService, keyPointService, tourReviewService, loggedInGuide, tourReservationService, voucherService, userService, countriesAndCitiesService, tourNotificationService, complexTourRequestService, subTourRequestService);
        }
    }
}
