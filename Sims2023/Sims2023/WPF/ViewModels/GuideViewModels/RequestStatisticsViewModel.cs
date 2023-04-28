using LiveCharts;
using LiveCharts.Wpf;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public partial class RequestStatisticsViewModel
    {
        public ObservableCollection<string> Labels { get; set; }
        public string[] LabelsMonth { get; set; }
        public Func<int, string> Values { get; set; }
        private RequestService _requestService;
        public SeriesCollection LanguageSeriesCollection { get; set; }
        public SeriesCollection LocationSeriesCollection { get; set; }

        public RequestStatisticsViewModel(RequestService requestService)
        {
            _requestService = requestService;

            LanguageSeriesCollection = new SeriesCollection();
            LocationSeriesCollection = new SeriesCollection();

            LabelsMonth = new[] { "Januar", "Februar", "Mart", "April", "Maj", "Jun", "Jul", "Avgust", "Septembar", "Oktobar", "Novembar", "Decembar" };
            Labels = new ObservableCollection<string>();
            foreach (var l in LabelsMonth)
            {
                Labels.Add(l);
            }
        }

        public List<RequestsLanguage> GetLanguages()
        {
            return _requestService.GetLanguages();
        }

        public List<string> GetLocations()
        {
            return _requestService.GetLocations();
        }

        public List<string> GetYears()
        {
            return _requestService.GetYears();
        }

        public void DisplayLanguageStatistics(string language, string year)
        {
            if (year == "Svih vremena")
            {
                DisplayYearlyLanguageStatistics(language, year);
            }
            else
            {
                DisplayMonthlyLanguageStatistics(language, year);
            }
        }

        public void DisplayYearlyLanguageStatistics(string language, string year)
        {
            LanguageSeriesCollection.Clear();
            var yearlyStats = new ChartValues<int>();
            foreach (var y in _requestService.GetYears())
            {
                yearlyStats.Add(_requestService.GetYearlyLanguageStatistics(language, y));
            }
            LanguageSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Broj zahteva po godinama" });
            Labels.Clear();
            foreach (var l in _requestService.GetYears())
            {
                Labels.Add(l);
            }
            Values = value => value.ToString("N");
        }

        public void DisplayMonthlyLanguageStatistics(string language, string year)
        {
            LanguageSeriesCollection.Clear();
            var monthlyStats = new ChartValues<int>();
            for (int month = 1; month <= 12; month++)
            {
                monthlyStats.Add(_requestService.GetMonthlyLanguageStatistics(language, month, year));
            }
            LanguageSeriesCollection.Add(new ColumnSeries { Values = monthlyStats, Title = "Broj zahteva u " + year + ":" });
            Labels.Clear();
            foreach (var l in LabelsMonth)
            {
                Labels.Add(l);
            }
            Values = value => value.ToString("N");
        }

        public void DisplayLocationStatistics(string location, string year)
        {
            if (year == "Svih vremena")
            {
                DisplayYearlyLocationStatistics(location, year);
            }
            else
            {
                DisplayMonthlyLocationStatistics(location, year);
            }
        }

        public void DisplayMonthlyLocationStatistics(string location, string year)
        {
            LocationSeriesCollection.Clear();
            var monthlyStats = new ChartValues<int>();
            for (int month = 1; month <= 12; month++)
            {
                monthlyStats.Add(_requestService.GetMonthlyLocationStatistics(location, month, year));
            }
            LocationSeriesCollection.Add(new ColumnSeries { Values = monthlyStats, Title = "Broj zahteva u " + year + ":" });
            Labels.Clear();
            foreach (var l in LabelsMonth)
            {
                Labels.Add(l);
            }
            Values = value => value.ToString("N");
        }

        public void DisplayYearlyLocationStatistics(string location, string year)
        {
            LocationSeriesCollection.Clear();
            var yearlyStats = new ChartValues<int>();
            foreach (var y in _requestService.GetYears())
            {
                yearlyStats.Add(_requestService.GetYearlyLocationStatistics(location, y));
            }
            LocationSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Broj zahteva po godinama" });
            Labels.Clear();
            foreach (var l in _requestService.GetYears())
            {
                Labels.Add(l);
            }
            Values = value => value.ToString("N");
        }

    }
}
