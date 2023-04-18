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
        public List<Tour> GetAvailable();
        public List<Tour> GetAlternative(int reserveSpace, Tour tour);
        public Uri GetPictureUri(Tour tour, int i);
        public List<Tour> GetFiltered(string citySearchTerm, string countrySearchTerm, string lengthSearchTerm, string guideLanguageSearchTerm, int maxGuestNumberSearchTerm);
    }
}
