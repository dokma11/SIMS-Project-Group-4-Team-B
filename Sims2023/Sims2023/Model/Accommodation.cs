using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Sims2023.Model
{
    public class Accommodation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set;  }
        public string City { get; set; }
        public string Country { get; set; }

        public int LocationId { get; set; }
        public string Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinDays { get; set; }
        public int CancelDays { get; set; }
        public List<string> Imageurls { get; set; }

        string ImageUrl { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public Accommodation()
        {
            Imageurls = new List<string>();
        }
        public Accommodation(int Id,string Name,int LocationId, string Type, int MaxGuests, int MinDays, int CancelDays, string ImageUrls)
        {
            this.Id = Id;
            this.Name = Name;
            this.LocationId = LocationId;
            this.Type = Type;
            this.MaxGuests = MaxGuests;
            this.MinDays = MinDays;
            this.CancelDays = CancelDays;
            ImageUrl = ImageUrls;

        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Type,
                MaxGuests.ToString(),
                MinDays.ToString(),
                CancelDays.ToString(),
                ImageUrl

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            Type = values[3];
            MaxGuests = Convert.ToInt32(values[4]);
            MinDays = Convert.ToInt32(values[5]);
            CancelDays = Convert.ToInt32(values[6]);
            ImageUrl = values[7];

        }

        public string IsVaild(Accommodation a)
        {
            if (string.IsNullOrEmpty(a.Name) || string.IsNullOrEmpty(a.Type) || string.IsNullOrEmpty(ImageUrl))
                return "morate popuniti sve podatke";

            if (a.CancelDays == -1 || a.MaxGuests == -1 || a.MinDays == -1 || a.LocationId == -1)
            {
                return "morate popuniti sve podatke";
            }

          


            return null;
        }

       



    }

 
}
