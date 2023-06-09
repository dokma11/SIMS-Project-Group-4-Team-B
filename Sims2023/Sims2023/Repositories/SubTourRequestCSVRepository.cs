using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sims2023.Domain.Models;
using Sims2023.Domain.RepositoryInterfaces;
using Sims2023.FileHandler;
using Sims2023.Observer;

namespace Sims2023.Repositories
{
    public class SubTourRequestCSVRepository: ISubTourRequestCSVRepository
    {
        private readonly List<SubTourRequest> _subTourRequests;
        private readonly SubTourRequestFileHandler _fileHandler;
        private readonly List<IObserver> _observers;

        public SubTourRequestCSVRepository()
        {
            _fileHandler = new SubTourRequestFileHandler();
            _subTourRequests = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public SubTourRequest GetById(int id)
        {
            return _fileHandler.GetById(id);
        }

        public int NextId()
        {
            return _subTourRequests.Count == 0 ? 1 : _subTourRequests.Max(t => t.Id) + 1;
        }

        public void Add(SubTourRequest subTourRequest)
        {
            subTourRequest.Id = NextId();
            _subTourRequests.Add(subTourRequest);
            _fileHandler.Save(_subTourRequests);
            NotifyObservers();
        }

        public List<SubTourRequest> GetAll()
        {
            return _subTourRequests;
        }

        public List<SubTourRequest> GetByComplexTourRequest(ComplexTourRequest complexTourRequest)
        {
            return _subTourRequests.Where(r=>r.ComplexTourRequest.Id==complexTourRequest.Id).ToList();    
        }
        public string GetEarliestSubTourDateByComplexTourRequest(ComplexTourRequest complexTourRequest)
        {
            return GetByComplexTourRequest(complexTourRequest).Min(t => t.TourRequest.Start).ToString();
        }

        /*public void CheckExpirationDate(ComplexTourRequest complexTourRequest)
        {
            DateTime FirstDate  = GetEarliestSubTourDateByComplexTourRequest(complexTourRequest);
            if (FirstDate != null)
                return;
            
            TimeSpan tillExpiration = FirstDate - DateTime.Now;
            if (tillExpiration.TotalHours < 48 && IsComplexTourInvalid(complexTourRequest))
            {
                UpdateComplexTourState(complexTourRequest);
                UpdateSubTourRequestStates(complexTourRequest);
            }
            
        }
        public bool IsComplexTourInvalid(ComplexTourRequest complexTourRequest)
        {
            foreach(SubTourRequest subTourRequest in GetByComplexTourRequest(complexTourRequest))
            {
                if (subTourRequest.TourRequest.State == RequestsState.Accepted)
                {
                    return false;
                }
            }
            return true;
        }
        
        public void UpdateComplexTourState(ComplexTourRequest complexTourRequest)
        {
            complexTourRequest.CurrentState = ComplexRequestsState.Invalid;
            Save();
        }

        public void UpdateSubTourRequestStates(ComplexTourRequest complexTourRequest)
        {
            foreach (SubTourRequest subTourRequest in GetByComplexTourRequest(complexTourRequest))
            {
                subTourRequest.TourRequest.State = RequestsState.Invalid;
                Save();
            }
        }*/




        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
        public void Save()
        {
            _fileHandler.Save(_subTourRequests);
        }
    }
}
