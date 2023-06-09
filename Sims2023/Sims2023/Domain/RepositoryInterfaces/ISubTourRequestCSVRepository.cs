using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims2023.Domain.Models;
using Sims2023.Observer;

namespace Sims2023.Domain.RepositoryInterfaces
{
    public interface ISubTourRequestCSVRepository
    {
        public SubTourRequest GetById(int id);
        public int NextId();
        public void Add(SubTourRequest subTourRequest);
        public List<SubTourRequest> GetAll();
        public List<SubTourRequest> GetByComplexTourRequest(ComplexTourRequest complexTourRequest);
        public string GetEarliestSubTourDateByComplexTourRequest(ComplexTourRequest complexTourRequest);
        //public void CheckExpirationDate(ComplexTourRequest complexTourRequest);
        //public bool IsComplexTourInvalid(ComplexTourRequest complexTourRequest);
        //public void UpdateComplexTourState(ComplexTourRequest complexTourRequest);
        //public void UpdateSubTourRequestStates(ComplexTourRequest complexTourRequest);
        public void Subscribe(IObserver observer);
        public void NotifyObservers();
        public void Save();
    }
}
