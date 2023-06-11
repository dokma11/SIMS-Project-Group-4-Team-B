using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class GuideHomePageView : Page, IObserver
    {
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private VoucherService _voucherService;
        private RequestService _requestService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private TourNotificationService _tourNotificationService;
        private ComplexTourRequestService _complexTourRequestService;
        private SubTourRequestService _subTourRequestService;

        public User LoggedInGuide { get; set; }
        public GuideViewModel GuideViewModel;

        public GuideHomePageView(User user)
        {
            InitializeComponent();

            LoggedInGuide = user;

            _tourService = new TourService();
            _tourService.Subscribe(this);

            _tourReservationService = new TourReservationService();
            _tourReservationService.Subscribe(this);

            _locationService = new LocationService();
            _locationService.Subscribe(this);

            _keyPointService = new KeyPointService();
            _keyPointService.Subscribe(this);

            _userService = new UserService();
            _userService.Subscribe(this);

            _tourReviewService = new TourReviewService();
            _tourReviewService.Subscribe(this);

            _voucherService = new VoucherService();
            _voucherService.Subscribe(this);

            _requestService = new RequestService();
            _requestService.Subscribe(this);

            _countriesAndCitiesService = new CountriesAndCitiesService();

            _tourNotificationService = new TourNotificationService();
            _tourNotificationService.Subscribe(this);

            _complexTourRequestService = new ComplexTourRequestService();
            _complexTourRequestService.Subscribe(this);

            _subTourRequestService = new SubTourRequestService();
            _subTourRequestService.Subscribe(this);

            GuideViewModel = new(_tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            DataContext = GuideViewModel;

            DisplayLabels();

            _userService.MarkSuperGuides(LoggedInGuide);
        }

        private void DisplayLabels()
        {
            if (_tourService.GetTodaysNumber(LoggedInGuide) > 0)
            {
                scheduledToursLabel.Content = "- Danas imate " + _tourService.GetTodaysNumber(LoggedInGuide) + " zakazane ture!";
            }
            else
            {
                scheduledToursLabel.Content = "- Nemate zakazanih tura za danas!";
            }

            if (_requestService.GetOnHold().Count > 0)
            {
                tourRequestsLabel.Content = "- Imate " + _requestService.GetOnHold().Count + " nova zahteva koja niste pregledali!";
            }
            else
            {
                tourRequestsLabel.Content = "- Pregledali ste sve zahteve!";
            }

            tourReviewsLabel.Content = "- Proverite recenzije gostiju";
        }

        //Toolbar options

        public void Update()
        {
            GuideViewModel.Update();
        }
    }
}
