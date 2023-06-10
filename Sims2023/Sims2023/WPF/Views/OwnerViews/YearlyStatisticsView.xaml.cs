using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using PdfSharp.Drawing;
using System.IO;
using iText.Kernel.Pdf;
using System.IO;
using iText.Kernel.Pdf;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for YearlyStatisticsView.xaml
    /// </summary>
    public partial class YearlyStatisticsView : Page
    {
        public YearlyStatisticsViewModel yearlyStatisticsViewModel;

        public string welcomeString { get; set; }

        public string welcomeString2 { get; set; }
        public YearlyStatisticsView(Accommodation Selected)
        {
            yearlyStatisticsViewModel = new YearlyStatisticsViewModel(Selected);
            InitializeComponent();
            DataContext = yearlyStatisticsViewModel;
            welcomeString = "Statistika smještaja " + Selected.Name;
            welcomeString2 = "Smještaj je bio najzauzetiji u " + yearlyStatisticsViewModel.BusiestYear() + ". godini";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService?.GoBack();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            yearlyStatisticsViewModel.Details_Click();
        }


        private void GenerateingPDF_Click(object sender, RoutedEventArgs e)
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
                foreach (var statistic in yearlyStatisticsViewModel.GetStatistics())
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

            // Close the document
            document.Close();

            }

        }


    }
