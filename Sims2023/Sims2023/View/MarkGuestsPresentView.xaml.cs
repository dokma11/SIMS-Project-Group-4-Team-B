using Sims2023.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for MarkGuestsPresentView.xaml
    /// </summary>
    public partial class MarkGuestsPresentView : Window
    {
        public KeyPoint KeyPoint { get; set; }
        public ObservableCollection<string> Guests { get; set; }
        public ObservableCollection<string> SelectedGuests { get; set; }
        public MarkGuestsPresentView(KeyPoint keyPoint)
        {
            InitializeComponent();
            DataContext = this;

            Guests = new ObservableCollection<string>();

            KeyPoint = keyPoint;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var guest in SelectedGuests)
            {
                //KeyPoint.ShowedGuestsIds.Add(guest);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
