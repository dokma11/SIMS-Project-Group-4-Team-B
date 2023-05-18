using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2TourRequestStatisticsViewModel
    {
        public User User { get; set; }
        private RequestService _requestService;
        public SeriesCollection LocationSeriesCollection { get; set; }
        public SeriesCollection LanguageSeriesCollection { get; set; }
        public ObservableCollection<string> Labels { get; set; }

        public ObservableCollection<string> LabelsLanguage { get; set; }
        public string[] LabelsMonth { get; set; }

        public string[] Languages { get; set; }
        public Func<int, string> Values { get; set; }
        public Func<int, string> LanguageValues { get; set; }
        public Guest2TourRequestStatisticsViewModel(User user)
        {
            _requestService = new RequestService();
            User = user;
            LocationSeriesCollection = new SeriesCollection();
            LanguageSeriesCollection = new SeriesCollection();
            LabelsMonth = _requestService.GetComboBoxData("locations").ToArray();

            Labels = new ObservableCollection<string>();
            foreach (var l in LabelsMonth)
            {
                Labels.Add(l);
            }

            Languages = _requestService.GetComboBoxData("languages").ToArray();
            LabelsLanguage= new ObservableCollection<string>();
            foreach(var l in Languages)
            {
                LabelsLanguage.Add(l);
            }
            
        }

        

        public List<string> GetYears()
        {
            return _requestService.GetComboBoxData("years");
        }

        public void DisplayLocationStatistics(string year)
        {
            if (year == "Svih vremena")
            {
                DisplayAllTimeLocationStatistic();
            }
            else
            {
                DisplayYearlyLocationStatistic(year);
            }
        }

        public void DisplayYearlyLocationStatistic(string year)
        {
            LocationSeriesCollection.Clear();
            var yearlyStats = new ChartValues<int>();
            foreach (var l in Labels)
            {
                yearlyStats.Add(_requestService.GetYerlyStatisticByUser(User,l,year));
              
            }
            LocationSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Broj zahteva u " + year + ":" });
            
            Values = value => value.ToString("D");
        }

        public void DisplayAllTimeLocationStatistic()
        {
            
            LocationSeriesCollection.Clear();
            var allTimeStats = new ChartValues<int>();
            foreach (var l in Labels)
            {
                allTimeStats.Add(_requestService.GetAllTimeLocationStatisticByUser(User,l));
            }
            LocationSeriesCollection.Add(new ColumnSeries { Values = allTimeStats, Title = "Broj zahteva po godinama" });
            
            Values = value => value.ToString("D");
        }

        public void DisplayLanguageStatistics(string year)
        {
            if (year == "Svih vremena")
            {
                DisplayYearlyLanguageStatistics();
            }
            else
            {
                DisplayMonthlyLanguageStatistics(year);
            }
        }

        public void DisplayYearlyLanguageStatistics()
        {
            LanguageSeriesCollection.Clear();
            var yearlyStats = new ChartValues<int>();
            foreach (var l in LabelsLanguage)
            {
                yearlyStats.Add(_requestService.GetAllTimeLanguageStatisticByUser(User,l));
                string message = User + " " + l + " "  + _requestService.GetAllTimeLanguageStatisticByUser(User, l).ToString();
                MessageBox.Show(message);
            }
            LanguageSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Broj zahteva po godinama" });
            
            LanguageValues = value => value.ToString("D");
        }

        public void DisplayMonthlyLanguageStatistics(string year)
        {
            LanguageSeriesCollection.Clear();
            var monthlyStats = new ChartValues<int>();
            foreach (var l in LabelsLanguage)
            {
                monthlyStats.Add(_requestService.GetYearlyLanguageStatisticByUser(User,l,year));
            }
            LanguageSeriesCollection.Add(new ColumnSeries { Values = monthlyStats, Title = "Broj zahteva u " + year + ":" });
            
            Values = value => value.ToString("D");
        }

    }
}
