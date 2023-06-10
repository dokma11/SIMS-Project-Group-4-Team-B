using Sims2023.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xceed.Wpf.AvalonDock.Themes;

namespace Sims2023.Domain.Models
{
    public class Forum : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Theme { get; set; }
        public string MainComment { get; set; }
        public Location Location { get; set; }
        public bool Special { get; set; }
        public int NumberOfReports { get; set; }
        public bool Closed { get; set; }
        public int CountGuestComments { get; set; }
        public int CountOwnerComments { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Forum() { }

        public Forum(int id, User user, string theme, string mainComment, Location location, bool special, int numberOfReports, bool closed, int countGuestComments, int countOwnerComments)
        {
            Id = id;
            User = user;
            Theme = theme;
            MainComment = mainComment;
            Location = location;
            Special = special;
            NumberOfReports = numberOfReports;
            Closed = closed;
            CountGuestComments = countGuestComments;
            CountOwnerComments = countOwnerComments;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            User.Id.ToString(),
            Theme,
            MainComment,
            Location.Id.ToString(),
            Special.ToString(),
            NumberOfReports.ToString(),
            Closed.ToString(),
            CountGuestComments.ToString(),
            CountOwnerComments.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            Theme = values[2];
            MainComment = values[3];
            Location = new()
            {
                Id = Convert.ToInt32(values[4])
            };
            Special=Convert.ToBoolean(values[5]);
            NumberOfReports = Convert.ToInt32(values[6]);
            Closed = Convert.ToBoolean(values[7]);
            CountGuestComments= Convert.ToInt32(values[8]);
            CountOwnerComments= Convert.ToInt32(values[9]);
        }
    }

}
