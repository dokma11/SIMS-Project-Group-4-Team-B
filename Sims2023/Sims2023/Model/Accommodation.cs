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
        public int id { get; set; }
        public string name { get; set;  }
        public string city { get; set; }
        public string country { get; set; }

        public int locationId { get; set; }
        public string type { get; set; }
        public int maxGuests { get; set; }
        public int minDays { get; set; }
        public int cancelDays { get; set; }
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
            maxGuests = MaxGuests;
            minDays = MinDays;
            cancelDays = CancelDays;
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
                maxGuests.ToString(),
                minDays.ToString(),
                cancelDays.ToString(),
                ImageUrl

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            name = values[1];
            locationId = Convert.ToInt32(values[2]);
            type = values[3];
            maxGuests = Convert.ToInt32(values[4]);
            minDays = Convert.ToInt32(values[5]);
            cancelDays = Convert.ToInt32(values[6]);
            ImageUrl = values[7];

        }

        public string isVaild(Accommodation a)
        {
            if (string.IsNullOrEmpty(a.name) || string.IsNullOrEmpty(a.type) || string.IsNullOrEmpty(ImageUrl))
                return "morate popuniti sve podatke";

            if (a.cancelDays == -1 || a.maxGuests == -1 || a.minDays == -1 || a.locationId == -1)
            {
                return "morate popuniti sve podatke";
            }

          


            return null;
        }

       



    }

 
}
