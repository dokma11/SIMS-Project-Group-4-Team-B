using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ITourReadFromCSVRepository
    {
        public Tour GetById(int id);
        public List<Tour> GetFinished(User loggedInGuide);
        public Tour GetTheMostVisited(User loggedInGuide, string year);
        public List<Tour> GetGuidesCreated(User loggedInGuide);   
        public List<Tour> GetCreated();
        public List<Tour> GetAlternatives(int reserveSpace, Tour tour);
        public Uri GetPictureUri(Tour tour, int i);
        public List<Tour> GetFiltered(string citySearchTerm, string countrySearchTerm, int lengthSearchTerm, string guideLanguageSearchTerm, int maxGuestNumberSearchTerm);
        public void Save();
        public int GetTodaysNumber(User loggedInGuide);
        public List<Tour> GetAll();
        public List<DateTime> GetBusyDates(User loggedInGuide);
    }
}
