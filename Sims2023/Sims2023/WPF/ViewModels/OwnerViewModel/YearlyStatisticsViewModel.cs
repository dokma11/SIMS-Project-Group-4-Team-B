using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.OwnerViews;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using iText.Layout;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class YearlyStatisticsViewModel
    {
        public AccommodationStatisticsService _statisticsService;

        public AccommodationReservationService _reservationService;
        public List<AccommodationReservation> _reservations { get; set; }
        public ObservableCollection<YearlyStatistics> Statistics { get; set; }
        public Accommodation Accommodation { get; set; }
        public YearlyStatistics SelectedYear { get; set; }

        int busiestYear = 0;
        public RelayCommand Back { get; set; }
        public RelayCommand Details { get; set; }
        public RelayCommand GeneratePDF { get; set; }
        public string welcomeString { get; set; }
        public string welcomeString2 { get; set; }

        public YearlyStatisticsViewModel(Accommodation selectedAccommodation)
        {
            Details = new RelayCommand(Executed_DetailsCommand, CanExecute_DetailsCommand);
            Back = new RelayCommand(Executed_BackCommand, CanExecute_BackCommand);
            GeneratePDF = new RelayCommand(Executed_GeneratePDFCommand, CanExecute_GeneratePDFCommand);
            Statistics = new ObservableCollection<YearlyStatistics>();
            _statisticsService = new AccommodationStatisticsService();
            _reservationService = new AccommodationReservationService();
            _reservations = _reservationService.GetAllReservations();
            Accommodation = selectedAccommodation;
            LoadData();
            welcomeString = "Statistika smještaja " + selectedAccommodation.Name;
            welcomeString2 = "Smještaj je bio najzauzetiji u " + BusiestYear() + ". godini";
        }

        private bool CanExecute_GeneratePDFCommand(object obj)
        {
            return true;
        }

        public void Executed_DetailsCommand(object obj)
        {
            if (SelectedYear != null)
            {
                MonthlyStatiticsView monthly = new MonthlyStatiticsView(Accommodation, SelectedYear.Year);
                FrameManager.Instance.MainFrame.Navigate(monthly);
            }
        }

        public bool CanExecute_DetailsCommand(object obj)
        {
            return true;
        }

        public bool CanExecute_BackCommand(object obj)
        {
            return true;
        }

        public void Executed_BackCommand(object obj)
        {
            if (FrameManager.Instance.MainFrame.CanGoBack)
            {
                FrameManager.Instance.MainFrame.GoBack();
            }
        }

        private void LoadData()
        {
            double maxOccupancy = 0;
            
            for (int year = 2018; year <= 2023; year++)
            {
                int NumReservationss = CountReservations(Accommodation, year);
                YearlyStatistics yearlyStat = new YearlyStatistics
                {
                    Year = year,
                    NumReservations = NumReservationss,
                    NumCanceled = 0,
                    NumRescheduled = 0,
                    NumRenovationRecommendation = 0
                };
                foreach (AccommodationStatistics stat in _statisticsService.GetAll()) 
                {
                    if (stat.Accommodation.Name == Accommodation.Name && stat.DateOfEntry.Year == year)
                    {
                        if (stat.isCanceled) yearlyStat.NumCanceled++;
                        if (stat.isRescheduled) yearlyStat.NumRescheduled++;
                        if (stat.RenovationRecommendation) yearlyStat.NumRenovationRecommendation++;

                    }
                }

                if (NumReservationss > 0)
                {
                    double occupancyPercentage = (double)NumReservationss / 365 * 100;

                    if (occupancyPercentage > maxOccupancy)
                    {
                        maxOccupancy = occupancyPercentage;
                        busiestYear = year;
                    }
                }
                Statistics.Add(yearlyStat);
            }
        }

        public void Executed_GeneratePDFCommand(object obj)
        {

            // Define the file path
            string relativePath = $"../../../Resources/OwnerResources/ReservationsReport.pdf";

            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, "Resources", "OwnerResources", "ReservationsReport.pdf");

            // Create a new PDF document
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(filePath));


            // Create a document
            Document document = new Document(pdfDocument);

            // Define the header text
            string headerText = "          Izvještaj za statistiku o smještaju";

            // Create a bold font for the header
            PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            // Create a paragraph with the header text and apply the bold font
            iText.Layout.Element.Paragraph headerParagraph = new iText.Layout.Element.Paragraph(headerText)
                .SetFont(boldFont)
                .SetFontSize(16f);

            // Add the header paragraph to the document
            document.Add(headerParagraph);

            // Add a blank line as a separator
            document.Add(new iText.Layout.Element.Paragraph());

            // Add the welcome string to the document
            document.Add(new iText.Layout.Element.Paragraph(welcomeString));

            // Create a table with 5 columns
            iText.Layout.Element.Table table = new iText.Layout.Element.Table(5);

            // Add table headers
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Godina")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Br. rezervacija")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Otkazivano")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Pomjerano")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Preporuka o renoviranju")));

            // Add table content
            foreach (var statistic in GetStatistics())
            {
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(statistic.Year.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(statistic.NumReservations.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(statistic.NumCanceled.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(statistic.NumRescheduled.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(statistic.NumRenovationRecommendation.ToString())));
            }


            // Add the table to the document
            document.Add(table);
            document.Add(new iText.Layout.Element.Paragraph());
            document.Add(new iText.Layout.Element.Paragraph());
            document.Add(new iText.Layout.Element.Paragraph(welcomeString2));
            document.Add(new iText.Layout.Element.Paragraph());
            document.Add(new iText.Layout.Element.Paragraph("Zauzetost se gleda kao broj dana kada je smeštaj bio zauzet u odnosu na ukupan broj dana u mesecu ili godini."));

            ToastNotificationService.ShowInformation("Uspiješno generisanje PDF");
            // Close the document
            document.Close();

        }




        public ObservableCollection<YearlyStatistics> GetStatistics()
        {
            return Statistics;
        }

        private int CountReservations(Accommodation accommodation, int year)
        {
            int counter = 0;
            foreach (AccommodationReservation reservation in _reservations)
            {
                if (reservation.Accommodation.Name == Accommodation.Name && reservation.StartDate.Year == year)
                {
                    counter++;
                }
            }
            return counter;
        }

        public string BusiestYear()
        {
            return busiestYear.ToString();
        }


    }
}
