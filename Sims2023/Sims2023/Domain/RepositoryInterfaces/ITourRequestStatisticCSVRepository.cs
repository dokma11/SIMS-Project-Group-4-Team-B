﻿using Sims2023.Domain.Models;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourRequestStatisticCSVRepository
    {
        public RequestsLanguage GetTheMostRequestedLanguage();
        public int GetTheMostRequestedLocation();
        public List<string> GetComboBoxData(string purpose);
        public int GetYearlyStatistics(string purpose, string statFor, string year);
        public int GetMonthlyStatistics(string purpose, string statFor, string year, int ordinal);
        public int GetYearlyStatisticByUser(User user, string statFor, string year, string purpose);
        public int GetAllTimeStatisticByUser(User user, string statFor, string purpose);
        public double GetAverageAllTimeAcceptedTourRequestGuestNumber(User user);
        public double GetAverageYearlyAcceptedTourRequestGuestNumber(User user, int year);
        public List<TourRequest> GetYearlyFilteredTourRequestsByUser(User user, int year, string state);
        public List<TourRequest> GetUsersAccepted(User user);
        public void Save();
        public List<TourRequest> GetAll();
        public double GetUsersAcceptedPercentageByYearAndLanguage(string year, string language);
        public double GetUsersDeclinedPercentageByYearAndLanguage(string year, string language);
        public double GetUsersAcceptedPercentageByYearAndLocation(string year, string location);
        public double GetUsersDeclinedPercentageByYearAndLocation(string year, string location);

    }
}
