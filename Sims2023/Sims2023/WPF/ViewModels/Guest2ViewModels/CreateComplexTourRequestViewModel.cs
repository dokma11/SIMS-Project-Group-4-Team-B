using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Commands;
using Sims2023.WPF.Views.Guest2Views;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class CreateComplexTourRequestViewModel
    {
        public ComplexTourRequestService _complexTourRequestService { get; set; }
        public ComplexTourRequest ComplexTourRequest { get; set; }
        public CreateComplexTourRequestView CreateComplexTourRequestView { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public User User { get; set; }


        public CreateComplexTourRequestViewModel(CreateComplexTourRequestView createComplexTourRequestView,User user)
        {
            
            this._complexTourRequestService = new ComplexTourRequestService();
            this.ComplexTourRequest = new ComplexTourRequest();
            this.CreateComplexTourRequestView = createComplexTourRequestView;
            this.User = user;
            this.CreateCommand = new RelayCommand(Execute_CreateCommand, CanExecute_NavigateCommand);
            this.CancelCommand=new RelayCommand(Execute_CancelCommand,CanExecute_NavigateCommand);  
           
            
        }
        public void ConfirmCreation(string name, User user)
        {
            ComplexTourRequest ComplexTourRequest = new ComplexTourRequest(name, user);
            _complexTourRequestService.Create(ComplexTourRequest);
        }

        private bool IsFilled()
        {
            return !string.IsNullOrWhiteSpace(CreateComplexTourRequestView.nameTextBox.Text);
        }

        private void Execute_CreateCommand(object obj)
        {
            if (IsFilled())
            {
                ConfirmCreation(CreateComplexTourRequestView.nameTextBox.Text, User);
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    MessageBox.Show("Uspešno kreiranje");
                }
                else
                {
                    MessageBox.Show("Creating successfully");
                }
                CreateComplexTourRequestView.Close();
            }
            else
            {
                if (CultureInfo.CurrentCulture.ToString() == "sr-Latn")
                {
                    MessageBox.Show("Molim Vas upišite ime kompleksne ture");
                }
                else
                {
                    MessageBox.Show("Please enter the name of the complex tour");
                }
                
            }
        }

        private void Execute_CancelCommand(object obj)
        {
            CreateComplexTourRequestView.Close();
        }
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

    }

   
}
