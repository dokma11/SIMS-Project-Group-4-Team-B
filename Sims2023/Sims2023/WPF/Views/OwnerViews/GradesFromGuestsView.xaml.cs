﻿using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.View
{
    /// <summary>
    /// Interaction logic for GradesFromGuestsView.xaml
    /// </summary>
    public partial class GradesFromGuestsView : Page
    {
        public ObservableCollection<AccommodationGrade> people { get; set; }
        public User owner { get; set; }
        private GradesFromGuestsViewModel GradesFromGuestsViewModel;
        public AccommodationGrade SelectedPerson { get; set; }
        public GradesFromGuestsView(User user)
        {
            InitializeComponent();
            DataContext = this;
            owner = user;
            GradesFromGuestsViewModel = new GradesFromGuestsViewModel(owner);
            people = new ObservableCollection<AccommodationGrade>(GradesFromGuestsViewModel.FindAllGuestsWhoGraded());
        }
        private void Details_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson != null)
            {
                var GuestsGrades = new DetailedGradeView(SelectedPerson);
                FrameManager.Instance.MainFrame.Navigate(GuestsGrades); 
            }
        }
    
    }
}
