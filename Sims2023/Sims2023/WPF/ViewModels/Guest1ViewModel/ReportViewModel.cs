using iTextSharp.text;
using iTextSharp.text.pdf;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using Sims2023.WPF.Views.Guest1Views.Guest1HelpViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    internal class ReportViewModel
    {
        private User User;
        private ReportView ReportView;
        DateTime startDateSelected;
        DateTime endDateSelected;
        AccommodationReservationService _accommodationReservationService;
        AccommodationCancellationService _accommodationCancellationService;

        public ReportViewModel(User user, ReportView reportView)
        {
            User = user;
            ReportView = reportView;

            _accommodationReservationService = new();
            _accommodationCancellationService = new();
        }

        internal void GenerateReport()
        {
            if (CheckFields())
            {
                if (ReportView.typeComboBox.Text == "O zakazanim rezervacijama")
                {
                    GenerateReportFile("ReservationsReport.pdf");
                }
                else
                {
                    GenerateReportFile("CancellationsReport.pdf");
                }
            }
        }

        private void GenerateReportFile(String filename)
        {
            string relativePath = $"../../../Resources/GuestOneResources/Report/{filename}";
            //
            Document document = new Document();
            FileStream fs = new FileStream(relativePath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.Open();

            if (filename == "ReservationsReport.pdf")
            {
                WriteToReservationsFile(document);
            }
            else
            {
                WriteToCancellationsFile(document);
            }

            document.Close();

            MessageBox.Show("Uspešno ste izgenerisali pdf izveštaj.");
        }

        private void WriteToCancellationsFile(Document document)
        {
            Paragraph title = new Paragraph("Vaša otkazivanja rezervacije smeštaja", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.BLACK));
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            Paragraph dateRange = new Paragraph($"Rezervacije otkazane između {startDateSelected:dd/MM/yyyy} i {endDateSelected:dd/MM/yyyy}:", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD, BaseColor.BLACK));
            document.Add(dateRange);

            List<AccommodationCancellation> cancellations = _accommodationCancellationService.FindReservationCancellationsInDateFrame(User, startDateSelected, endDateSelected);

            foreach (AccommodationCancellation reservationCancellation in cancellations)
            {
                Paragraph accommodationName = new Paragraph(reservationCancellation.Accommodation.Name, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK));
                document.Add(accommodationName);

                Paragraph location = new Paragraph($"Lokacija: {reservationCancellation.Accommodation.Location.City}, {reservationCancellation.Accommodation.Location.Country}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(location);

                Paragraph type = new Paragraph($"Tip smeštaja: {reservationCancellation.Accommodation.Type}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(type);

                Paragraph startDateText = new Paragraph($"Datum početka rezervacije: {reservationCancellation.StartDate:dd/MM/yyyy}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(startDateText);

                Paragraph endDateText = new Paragraph($"Datum kraja rezervacije: {reservationCancellation.EndDate:dd/MM/yyyy}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(endDateText);
            }
        }

        private void WriteToReservationsFile(Document document)
        {
            Paragraph title = new Paragraph("Vaše rezervacije smeštaja", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD, BaseColor.BLACK));
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            Paragraph dateRange = new Paragraph($"Rezervacije napravljene između {startDateSelected:dd/MM/yyyy} i {endDateSelected:dd/MM/yyyy}:", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLD, BaseColor.BLACK));
            document.Add(dateRange);

            List<AccommodationReservation> reservations = _accommodationReservationService.FindReservationsInDateFrame(User, startDateSelected, endDateSelected);

            foreach (AccommodationReservation reservation in reservations)
            {
                Paragraph accommodationName = new Paragraph(reservation.Accommodation.Name, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK));
                document.Add(accommodationName);

                Paragraph location = new Paragraph($"Lokacija: {reservation.Accommodation.Location.City}, {reservation.Accommodation.Location.Country}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(location);

                Paragraph type = new Paragraph($"Tip smeštaja: {reservation.Accommodation.Type}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(type);

                Paragraph startDateText = new Paragraph($"Datum početka rezervacije: {reservation.StartDate:dd/MM/yyyy}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(startDateText);

                Paragraph endDateText = new Paragraph($"Datum kraja rezervacije: {reservation.EndDate:dd/MM/yyyy}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK));
                document.Add(endDateText);
            }
        }

        private bool CheckFields()
        {
            if (string.IsNullOrEmpty(ReportView.typeComboBox.Text))
            {
                MessageBox.Show("Molimo Vas da izaberete tip izveštaja.");
                return false;
            }
            if (ReportView.startDatePicker.SelectedDate == null || ReportView.endDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Molimo Vas da selektujete oba datuma.");
                return false;
            }
            startDateSelected = ReportView.startDatePicker.SelectedDate.Value;
            endDateSelected = ReportView.endDatePicker.SelectedDate.Value;
            if (DateTime.Compare(startDateSelected, endDateSelected) > 0)
            {
                MessageBox.Show("Molimo Vas selektujete pravilno datume. Datum kraja ne može biti pre datuma početka.");
                return false;
            }

            return true;
        }

        internal void GoBack()
        {
            ReportView.Close();
        }

        internal void OpenHelp()
        {

            var GuestOneMainHelpView = new GuestOneMainHelpView("ReportView");
            GuestOneMainHelpView.Show();
        }
    }
}
