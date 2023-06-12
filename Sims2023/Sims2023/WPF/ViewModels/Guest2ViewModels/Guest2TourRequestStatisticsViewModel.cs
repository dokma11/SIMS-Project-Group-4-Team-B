using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

        public ChartValues<double> AcceptedRequests { get; set; }
        public ChartValues<double> NotAcceptedRequests { get; set; }

        public string AcceptedPercentage { get; set; }
        public string NotAcceptedPercentage { get; set; }
        public int AverageAcceptedRequestsGuestNumber { get; set; }


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
            AcceptedRequests = new ChartValues<double>();
            NotAcceptedRequests = new ChartValues<double>();
           
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
                yearlyStats.Add(_requestService.GetYearlyStatisticByUser(User,l,year,"location"));
              
            }
            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {
                LocationSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Broj zahteva u " + year + ":" });
            }
            else
            {
                LocationSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Number of requests in " + year + ":" });
            }
            
            Values = value => value.ToString("D");
        }

        public void DisplayAllTimeLocationStatistic()
        {
            
            LocationSeriesCollection.Clear();
            var allTimeStats = new ChartValues<int>();
            foreach (var l in Labels)
            {
                allTimeStats.Add(_requestService.GetAllTimeStatisticByUser(User,l,"location"));
            }
            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {
                LocationSeriesCollection.Add(new ColumnSeries { Values = allTimeStats, Title = "Broj zahteva ikada" });
            }
            else
            {
                LocationSeriesCollection.Add(new ColumnSeries { Values = allTimeStats, Title = "Number of requests all time" });
            }
            Values = value => value.ToString("D");
        }

        public void DisplayLanguageStatistics(string year)
        {
            if (year == "Svih vremena" || year == "All time" )
            {
                DisplayAllTimeLanguageStatistics();
            }
            else
            {
                DisplayYearlyLanguageStatistics(year);
            }
        }

        public void DisplayAllTimeLanguageStatistics()
        {
            LanguageSeriesCollection.Clear();
            var allTimeStats = new ChartValues<int>();
            foreach (var l in LabelsLanguage)
            {
                allTimeStats.Add(_requestService.GetAllTimeStatisticByUser(User,l,"language"));
                
            }
            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {
                LanguageSeriesCollection.Add(new ColumnSeries { Values = allTimeStats, Title = "Broj zahteva ikada" });
            }
            else
            {
                LanguageSeriesCollection.Add(new ColumnSeries { Values = allTimeStats, Title = "Number of requests for all time" });
            }
            LanguageValues = value => value.ToString("D");
        }

        public void DisplayYearlyLanguageStatistics(string year)
        {
            LanguageSeriesCollection.Clear();
            var yearlyStats = new ChartValues<int>();
            foreach (var l in LabelsLanguage)
            {
                yearlyStats.Add(_requestService.GetYearlyStatisticByUser(User,l,year,"language"));
            }
            if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
            {
                LanguageSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Broj zahteva za " + year + ":" });
            }
            else
            {
                LanguageSeriesCollection.Add(new ColumnSeries { Values = yearlyStats, Title = "Number of requests for " + year + ":" });
            }
            Values = value => value.ToString("D");
        }

        public void DisplayTourRequestStatistics(string year)
        {
            if(year=="Svih vremena" || year=="All time")
            {
                DisplayAllTimeTourRequestStatistic();
            }
            else
            {
                DisplayYearlyTourRequestStatistic(year);
            }
            GetAverageYearlyAcceptedRequestsGuestNumber(year);
        }

        public void DisplayAllTimeTourRequestStatistic()
        {
            int acceptedRequestCount = _requestService.GetAcceptedTourRequestsByUser(User).Count();
            int notAcceptedRequestCount = _requestService.GetByUser(User).Count() - _requestService.GetAcceptedTourRequestsByUser(User).Count();
            AcceptedRequests.Clear();
            NotAcceptedRequests.Clear();
            AcceptedRequests.Add(acceptedRequestCount );
            NotAcceptedRequests.Add(notAcceptedRequestCount  );
            GetPercentages(acceptedRequestCount,notAcceptedRequestCount);
        }

        public void DisplayYearlyTourRequestStatistic(string year)
        {
            int acceptedRequestCount = _requestService.GetYearlyFilteredTourRequestsByUser(User, Convert.ToInt32(year),"Accepted").Count();
            int notAcceptedRequestCount = _requestService.GetYearlyFilteredTourRequestsByUser(User, Convert.ToInt32(year),"Not accepted").Count();
            AcceptedRequests.Clear();
            NotAcceptedRequests.Clear();
            AcceptedRequests.Add( acceptedRequestCount);
            NotAcceptedRequests.Add(notAcceptedRequestCount);
            GetPercentages(acceptedRequestCount, notAcceptedRequestCount);  
        }

        public void GetPercentages(double acceptedRequestCount,double notAcceptedRequestCount)
        {
            double acceptedRequests = Math.Round(acceptedRequestCount * 100 / (acceptedRequestCount + notAcceptedRequestCount),2);
            AcceptedPercentage = acceptedRequests + "%";
           
            double notAcceptedRequests = Math.Round(notAcceptedRequestCount * 100 / (acceptedRequestCount + notAcceptedRequestCount),2);
            NotAcceptedPercentage = notAcceptedRequests + "%";
        }

        public void GetAverageYearlyAcceptedRequestsGuestNumber(string year)
        {
            if (year == "Svih vremena" || year=="All time")
            {
                AverageAcceptedRequestsGuestNumber =Convert.ToInt32(_requestService.GetAverageAllTimeAcceptedTourRequestGuestNumber(User));
            }
            else
            {
                AverageAcceptedRequestsGuestNumber = Convert.ToInt32(_requestService.GetAverageYearlyAcceptedTourRequestGuestNumber(User,Convert.ToInt32(year)));
            }

        }
    }
}
