using System;
using System.Collections.Generic;
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
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.Guest2ViewModels;

namespace Sims2023.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for CreateComplexTourRequestView.xaml
    /// </summary>
    public partial class CreateComplexTourRequestView : Window
    {
        private User User { get; set; }
        public CreateComplexTourRequestViewModel CreateComplexTourRequestViewModel { get; set; }
        public CreateComplexTourRequestView(User user)
        {
            InitializeComponent();
            User = user;
            CreateComplexTourRequestViewModel = new CreateComplexTourRequestViewModel();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFilled())
            {
                CreateComplexTourRequestViewModel.ConfirmCreation(nameTextBox.Text, User);
                MessageBox.Show("Uspesno kreiranje");
                this.Close();
            }
            else
            {
                MessageBox.Show("Molim Vas popunite sva polja");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsFilled()
        {
            return !string.IsNullOrWhiteSpace(nameTextBox.Text);
        }
    }
}
