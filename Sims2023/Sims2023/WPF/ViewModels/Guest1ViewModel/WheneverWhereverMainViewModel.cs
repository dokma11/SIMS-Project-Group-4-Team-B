using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class WheneverWhereverMainViewModel
    {
        public User User { get; set; }
        public Frame MainFrame;
        bool dateCheck;
        WheneverWhereverMainView WheneverWhereverMainView;

        public WheneverWhereverMainViewModel(User user, Frame mainFrame, WheneverWhereverMainView wheneverWhereverMainView)
        {
            User = user;
            MainFrame = mainFrame;
            WheneverWhereverMainView = wheneverWhereverMainView;
            WheneverWhereverMainView.startDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            WheneverWhereverMainView.endDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }

        public void MakeReservation()
        {
            DateTime startDateSelected;
            DateTime endDateSelected;

            if (WheneverWhereverMainView.startDatePicker.SelectedDate == null && WheneverWhereverMainView.endDatePicker.SelectedDate == null)
            {
                startDateSelected = DateTime.Today;
                endDateSelected = startDateSelected.AddDays(365);
            }
            else if (WheneverWhereverMainView.startDatePicker.SelectedDate == null)
            {
                startDateSelected = DateTime.Today;
                endDateSelected = WheneverWhereverMainView.endDatePicker.SelectedDate.Value;
            }
            else if (WheneverWhereverMainView.endDatePicker.SelectedDate == null)
            {
                startDateSelected = WheneverWhereverMainView.startDatePicker.SelectedDate.Value;
                endDateSelected = startDateSelected.AddDays(365);
            }
            else
            {
                startDateSelected = WheneverWhereverMainView.startDatePicker.SelectedDate.Value;
                endDateSelected = WheneverWhereverMainView.endDatePicker.SelectedDate.Value;
            }

            if (!CheckFields(startDateSelected, endDateSelected))
            {
                return;
            }

            int stayLength = (int)WheneverWhereverMainView.numberOfDays.Value;
            int numberOfGuests = (int)WheneverWhereverMainView.numberOfGuests.Value;

            MainFrame.Navigate(new WheneverWhereverOptionsView(startDateSelected, endDateSelected, stayLength, numberOfGuests, User, MainFrame));
        }

        private bool CheckFields(DateTime startDateSelected, DateTime endDateSelected)
        {
            int numberOfGuests = (int)WheneverWhereverMainView.numberOfGuests.Value;
            int stayLength = (int)WheneverWhereverMainView.numberOfDays.Value;
            
            if (numberOfGuests == 0)
            {
                MessageBox.Show("Popunite polje za broj gostiju!");
                return false;
            }

            if (DateTime.Compare(startDateSelected, endDateSelected) > 0)
            {
                MessageBox.Show("Molimo Vas selektujte pravilno datume. Krajnji datum mora biti posle početnog!");
                return false;
            }

            if (stayLength == 0)
            {
                MessageBox.Show("Popunite polje za broj dana!");
                return false;
            }

            if (endDateSelected.AddDays(-stayLength) < startDateSelected)
            {
                MessageBox.Show("Krajnji datum minus broj dana ne može biti pre početnog datuma!");
                return false;
            }
            return true;
        }

    }
}
