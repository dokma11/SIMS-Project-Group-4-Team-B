using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class CreateComplexTourRequestViewModel
    {
        public ComplexTourRequestService _complexTourRequestService { get; set; }
        public ComplexTourRequest ComplexTourRequest { get; set; }
       

        public CreateComplexTourRequestViewModel()
        {
            
            _complexTourRequestService = new ComplexTourRequestService();
            ComplexTourRequest = new ComplexTourRequest();
           
            
        }
        public void ConfirmCreation(string name, User user)
        {
            ComplexTourRequest ComplexTourRequest = new ComplexTourRequest(name, user);
            _complexTourRequestService.Create(ComplexTourRequest);
        }



    }

   
}
