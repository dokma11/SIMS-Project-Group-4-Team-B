using System.Windows.Controls;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views.Guest1Wizard;

namespace Sims2023.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for GuestOneStartView.xaml
    /// </summary>
    public partial class GuestOneStartView : Page
    {
        User User { get; set; }
        public GuestOneStartView(User user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
        }

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var i = new WizardMainView();
            i.Show();
        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var i = new ReportView(User);
            i.Show();
        }
    }
}
