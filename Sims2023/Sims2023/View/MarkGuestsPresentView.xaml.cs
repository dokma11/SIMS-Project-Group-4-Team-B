using Sims2023.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for MarkGuestsPresentView.xaml
    /// </summary>
    public partial class MarkGuestsPresentView : Window
    {
        public KeyPoint KeyPoint { get; set; }
        public ObservableCollection<string> Guests { get; set; }
        public List<string> Guests2 { get; set; }
        public List<string> Guests3 { get; set; }

        public MarkGuestsPresentView(KeyPoint keyPoint)
        {
            InitializeComponent();
            DataContext = this;

            Guests = new ObservableCollection<string>
            {
                "pera",
                "mika",
                "djoka",
                "sale",
                "rale"
            };

            Guests2 = new List<string>();
            Guests3 = new List<string>();

            KeyPoint = keyPoint;
            if (KeyPoint.ShowedGuestsIdsString == null)
            {
                KeyPoint.ShowedGuestsIdsString = "";
            }

            Update();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var guest in guestDataGrid.SelectedItems)
            {
                if ((string)guest == "pera")
                {
                    KeyPoint.ShowedGuestsIds.Add(1);
                    KeyPoint.ShowedGuestsIdsString += " pera";
                    Guests3.Add((string)guest);
                }
                else if ((string)guest == "mika")
                {
                    KeyPoint.ShowedGuestsIds.Add(2);
                    KeyPoint.ShowedGuestsIdsString += " mika";
                    Guests3.Add((string)guest);
                }
                else if ((string)guest == "djoka")
                {
                    KeyPoint.ShowedGuestsIds.Add(3);
                    KeyPoint.ShowedGuestsIdsString += " djoka";
                    Guests3.Add((string)guest);
                }
                else if ((string)guest == "sale")
                {
                    KeyPoint.ShowedGuestsIds.Add(4);
                    KeyPoint.ShowedGuestsIdsString += " sale";
                    Guests3.Add((string)guest);
                }
                else if ((string)guest == "rale")
                {
                    KeyPoint.ShowedGuestsIds.Add(5);
                    KeyPoint.ShowedGuestsIdsString += " rale";
                    Guests3.Add((string)guest);
                }
                else
                {
                    MessageBox.Show("Odaberite gosta kojeg zelite da dodate");
                }
                Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Update()
        {
            foreach(var guest in Guests)
            {
                Guests2.Add(guest);
            }

            Guests.Clear();

            int counter = 0;
            foreach(var guest2 in Guests2)
            {
                foreach(var guest3 in Guests3)
                {
                    if (guest2 == guest3)
                        counter++;
                }
                if (!KeyPoint.ShowedGuestsIdsString.Contains(guest2) && counter == 0)
                {
                    Guests.Add(guest2);
                }
                counter = 0;
            }
        }
    }
}
