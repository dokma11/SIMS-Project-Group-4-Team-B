using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sims2023.Repositories
{
    public class TourRequestStatisticCSVRepository : ITourRequestStatisticCSVRepository
    {
        private List<TourRequest> _requests;
        private TourRequestFileHandler _fileHandler;

        public TourRequestStatisticCSVRepository()
        {
            _fileHandler = new TourRequestFileHandler();
            _requests = _fileHandler.Load();
        }

        public int GetYearlyStatistics(string purpose, string statFor, string year)
        {
            if (purpose == "location")
            {
                return _requests.Count(r => r.Start.Year.ToString() == year && (r.Location.City + ", " + r.Location.Country) == statFor);
            }
            else
            {
                return _requests.Count(r => r.Start.Year.ToString() == year && r.Language.ToString() == statFor);
            }
        }

        public int GetMonthlyStatistics(string purpose, string statFor, string year, int ordinal)
        {
            if (purpose == "location")
            {
                return _requests.Count(r => r.Start.Month == ordinal && r.Start.Year.ToString() == year && (r.Location.City + ", " + r.Location.Country) == statFor);
            }
            else
            {
                return _requests.Count(r => r.Language.ToString() == statFor && r.Start.Year.ToString() == year && r.Start.Month == ordinal);
            }
        }

        public int GetYearlyStatisticByUser(User user, string statFor, string year, string purpose)
        {
            if (purpose == "location")
            {
                return _requests.Count(r => r.Start.Year.ToString() == year && r.Guest.Id == user.Id && (r.Location.City + ", " + r.Location.Country) == statFor);
            }
            else
            {
                return _requests.Count(r => r.Start.Year.ToString() == year && r.Guest.Id == user.Id && r.Language.ToString() == statFor);
            }
        }

        public int GetAllTimeStatisticByUser(User user, string statFor, string purpose)
        {
            if (purpose == "location")
            {
                return _requests.Count(r => r.Guest.Id == user.Id && (r.Location.City + ", " + r.Location.Country) == statFor);
            }
            else
            {
                return _requests.Count(r => r.Guest.Id == user.Id && r.Language.ToString() == statFor);
            }
        }

        public RequestsLanguage GetTheMostRequestedLanguage()
        {
            var lastYearStartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            var lastYearEndDate = DateTime.Today;

            var requestsInLastYear = _requests.Where(r => r.Start >= lastYearStartDate && r.Start <= lastYearEndDate)
                                              .GroupBy(r => r.Language).ToDictionary(g => g.Key, g => g.Count());

            var maxCount = requestsInLastYear.Values.Max();
            return requestsInLastYear.FirstOrDefault(r => r.Value == maxCount).Key;
        }

        public int GetTheMostRequestedLocation()
        {
            var lastYearStartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            var lastYearEndDate = DateTime.Today;

            var requestsInLastYear = _requests.Where(r => r.Start >= lastYearStartDate && r.Start <= lastYearEndDate)
                                              .GroupBy(r => r.Location.Id).ToDictionary(g => g.Key, g => g.Count());

            var maxCount = requestsInLastYear.Values.Max();
            return requestsInLastYear.FirstOrDefault(r => r.Value == maxCount).Key;
        }

        public List<string> GetComboBoxData(string purpose)
        {
            if (purpose == "years")
            {
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    return _requests.Select(r => r.Start.Year.ToString()).Distinct().Prepend("Svih vremena").ToList();
                }
                else
                {
                    return _requests.Select(r => r.Start.Year.ToString()).Distinct().Prepend("All time").ToList();
                }
            }
            else if (purpose == "locations")
            {
                return _requests.Select(r => $"{r.Location.City}, {r.Location.Country}").Distinct().ToList();
            }
            else
            {
                return _requests.Select(r => r.Language.ToString()).Distinct().ToList();
            }
        }

        public List<TourRequest> GetAll()
        {
            return _requests;
        }

        public double GetAverageAllTimeAcceptedTourRequestGuestNumber(User user)
        {
            double sum = 0;
            int counter = GetUsersAccepted(user).Count();
            if (counter == 0)
            {
                return 0;
            }
            foreach (TourRequest request in GetUsersAccepted(user))
            {
                sum += request.GuestNumber;

            }
            return sum / counter;
        }

        public double GetAverageYearlyAcceptedTourRequestGuestNumber(User user, int year)
        {
            double sum = 0;
            int counter = GetYearlyFilteredTourRequestsByUser(user, year, "Accepted").Count();
            if (counter == 0)
            {
                return 0;
            }
            foreach (TourRequest request in GetYearlyFilteredTourRequestsByUser(user, year, "Accepted"))
            {
                sum += request.GuestNumber;

            }
            return sum / counter;
        }

        public List<TourRequest> GetYearlyFilteredTourRequestsByUser(User user, int year, string state)
        {
            if (state == "Accepted")
            {
                return _requests.Where(r => r.Guest.Id == user.Id && r.Start.Year == year && r.State == RequestsState.Accepted).ToList();
            }
            else
            {
                return _requests.Where(r => r.Guest.Id == user.Id && r.Start.Year == year && r.State != RequestsState.Accepted).ToList();
            }
        }

        public List<TourRequest> GetUsersAccepted(User user)
        {
            return _requests.Where(r => r.State == RequestsState.Accepted && r.Guest.Id == user.Id).ToList();
        }

        public void Save()
        {
            _fileHandler.Save(_requests);
        }
    }
}
