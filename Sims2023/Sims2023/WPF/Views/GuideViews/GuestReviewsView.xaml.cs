﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    public partial class GuestReviewsView : Page
    {
        public GuestReviewsView(TourService tourService, TourReviewService tourReviewService, LocationService locationService, RequestService requestService, KeyPointService keyPointService, User loggedInGuide, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            InitializeComponent();
            DataContext = new GuestReviewsViewModel(tourService, locationService, keyPointService, tourReviewService, requestService, tourReservationService, voucherService, userService, countriesAndCitiesService, loggedInGuide, tourNotificationService, complexTourRequestService, subTourRequestService);
        }
    }
}
