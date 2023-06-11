using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    internal class Guest2ReportViewModel
    {
        private User User;
        private Guest2ReportView Guest2ReportView;
        DateTime startDateSelected;
        DateTime endDateSelected;
        private TourReservationService _tourReservationService { get; set; }

        public Guest2ReportViewModel(User user, Guest2ReportView reportView)
        {
            User = user;
            Guest2ReportView = reportView;

            _tourReservationService= new TourReservationService();
        }

        internal void GenerateReport()
        {

            GenerateReportFile("ReservationsReport.pdf");


        }

        private void GenerateReportFile(String filename)
        {
            try
            {
                string relativePath = $"../../../Resources/GuestTwoResources/Report/{filename}";
                string absolutePath = Path.GetFullPath(relativePath);
                Document document = new Document();
                FileStream fs = new FileStream(absolutePath, FileMode.Create);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, fs);
                
                document.Open();


                WriteToReservationsFile(document);


                document.Close();

                MessageBox.Show("Uspešno ste izgenerisali pdf izveštaj.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while generating the PDF report: {ex.Message}");
            }
        }

        

        private void WriteToReservationsFile(Document document)
        {
            Paragraph title = new Paragraph("Vaše prisustvo na turama", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.BLACK));
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            Paragraph dateRange = new Paragraph($"Ture odrzane između {startDateSelected:dd/MM/yyyy} i {endDateSelected:dd/MM/yyyy}:", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD, BaseColor.BLACK));
            document.Add(dateRange);

            List<TourReservation> reservations = _tourReservationService.GetReportsTourReservation(User, startDateSelected, endDateSelected);

            foreach (TourReservation reservation in reservations)
            {
                Paragraph tourName = new Paragraph(reservation.Tour.Name, new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK));
                document.Add(tourName);

                Paragraph location = new Paragraph($"Lokacija: {reservation.Tour.Location.City}, {reservation.Tour.Location.Country}", new Font(Font.FontFamily.COURIER, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(location);

                Paragraph type = new Paragraph($"Tip smeštaja: {reservation.Tour.Description}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(type);

                Paragraph startDateText = new Paragraph($"Datum rezervacije: {reservation.ReservationTime:dd/MM/yyyy}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(startDateText);


            }
        }

        private bool CheckFields()
        {
            
            if (Guest2ReportView.startDatePicker.SelectedDate == null || Guest2ReportView.endDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Molimo Vas da selektujete oba datuma.");
                return false;
            }
            startDateSelected = Guest2ReportView.startDatePicker.SelectedDate.Value;
            endDateSelected = Guest2ReportView.endDatePicker.SelectedDate.Value;
            if (DateTime.Compare(startDateSelected, endDateSelected) > 0)
            {
                MessageBox.Show("Molimo Vas selektujete pravilno datume. Datum kraja ne može biti pre datuma početka.");
                return false;
            }

            return true;
        }

        internal void GoBack()
        {
            Guest2ReportView.Close();
        }

       
    }
}
