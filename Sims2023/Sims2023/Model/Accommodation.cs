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
    public class Accommodation : Serializable, INotifyPropertyChanged
    {
        public int id { get; set; }
        public string name { get; set;  }
        public string city { get; set; }
        public string country { get; set; }

        public int locationId { get; set; }
        public string type { get; set; }
        public int maxguests { get; set; }
        public int mindays { get; set; }
        public int canceldays { get; set; }
        public List<string> imageurls { get; set; }

        string ImageUrl { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public Accommodation()
        {
            imageurls = new List<string>();
        }
        public Accommodation(int Id,string Name,int LocationId, string Type, int MaxGuests, int MinDays, int CancelDays, string ImageUrls)
        {
            id = Id;
            name = Name;
            locationId = LocationId;
            type = Type;
            maxguests = MaxGuests;
            mindays = MinDays;
            canceldays = CancelDays;
            ImageUrl = ImageUrls;

        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                name,
                locationId.ToString(),
                type,
                maxguests.ToString(),
                mindays.ToString(),
                canceldays.ToString(),
                ImageUrl

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            name = values[1];
            locationId = int.Parse(values[2]);
            type = values[3];
            maxguests = int.Parse(values[4]);
            mindays = int.Parse(values[5]);
            canceldays = int.Parse(values[6]);
            ImageUrl = values[7];

        }

        public string isVaild(Accommodation a)
        {
            if (string.IsNullOrEmpty(a.name) || string.IsNullOrEmpty(a.city) || string.IsNullOrEmpty(a.country) || string.IsNullOrEmpty(a.type) || string.IsNullOrEmpty(ImageUrl))
                return "morate popuniti sve podatke";

            if (a.canceldays == -1 || a.maxguests == -1 || a.mindays == -1)
            {
                return "morate popuniti sve podatke";
            }

          


            return null;
        }

       



    }

 
}
