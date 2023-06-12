﻿using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class AccommodationRenovationRecommodationViewModel
    {
        private AccommodationGrade Grade;

        private AccommodationRenovationRecommodationView _accommodationRenovationRecommodationView;

        private AccommodationGradeService _accommodationGradeService;
        public ObservableCollection<AccommodationGrade> AccommodationGrades { get; set; }

        private AccommodationReservationService _accommodationReservationService;

        private AccommodationStatisticsService _statisticSerice;
        public AccommodationReservation SelectedAccommodationReservation { get; set; }
        public User User { get; set; }

        public AccommodationRenovationRecommodationViewModel(AccommodationRenovationRecommodationView accommodationRenovationRecommodationView, AccommodationGrade grade, AccommodationReservation selectedAccommodationReservationn, User guest1, AccommodationReservationService accommodationReservationController, AccommodationGradeService accommodationGradeService)
        {
            _accommodationGradeService = accommodationGradeService;
            _accommodationRenovationRecommodationView = accommodationRenovationRecommodationView;
            AccommodationGrades = new ObservableCollection<AccommodationGrade>(_accommodationGradeService.GetAllAccommodationGrades());
            Grade = grade;
            SelectedAccommodationReservation = selectedAccommodationReservationn;
            User = guest1;
            _statisticSerice = new AccommodationStatisticsService();
            _accommodationReservationService = accommodationReservationController;
        }

        public void AddRenovationRecommodation()
        {
            if (Grade != null)
            {
                AccommodationStatistics statistic = new AccommodationStatistics(SelectedAccommodationReservation.Accommodation, DateTime.Now, false, false, true);
                _statisticSerice.Create(statistic);
                MakeRenovationRecommodation();
                _accommodationGradeService.Update(Grade);
                _accommodationReservationService.Update(SelectedAccommodationReservation);
                MessageBox.Show("Uspesno ste ocenili ovaj smeštaj.");
            }
        }

        private void MakeRenovationRecommodation()
        {
            Grade.CurrentAccommodationState = _accommodationRenovationRecommodationView.textBox.Text;
            Grade.RenovationUrgency = _accommodationRenovationRecommodationView.typeComboBox.Text;
            SelectedAccommodationReservation.RecommendedRenovation = true;
        }

        public bool CheckIfAllFiledsFilled()
        {
            if (string.IsNullOrEmpty(_accommodationRenovationRecommodationView.textBox.Text)) return false;
            else if (string.IsNullOrEmpty(_accommodationRenovationRecommodationView.typeComboBox.Text)) return false;
            else return true;
        }

        internal void NotAllFieldsAreFiled()
        {
            if (string.IsNullOrEmpty(_accommodationRenovationRecommodationView.textBox.Text))
            {
                MessageBox.Show("Unesite opis trenutnog stanja smestaja.");
                return;
            }
            else if (string.IsNullOrEmpty(_accommodationRenovationRecommodationView.typeComboBox.Text))
            {
                MessageBox.Show("Unesite ocenu hitnosti renoviranja.");
                return;
            }
        }
    }
}
