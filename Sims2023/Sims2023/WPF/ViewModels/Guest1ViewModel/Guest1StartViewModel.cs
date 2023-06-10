using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class Guest1StartViewModel
    {
        private User User;
        private GuestOneStartView GuestOneStartView;

        public Guest1StartViewModel(User user, GuestOneStartView guestOneStartView)
        {
            User = user;
            GuestOneStartView = guestOneStartView;
        }

        internal void OpenReportView()
        {
            var i = new ReportView(User);
            i.Show();
        }
    }
}
