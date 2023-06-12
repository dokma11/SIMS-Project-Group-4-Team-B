using Sims2023.Application.Services;
using Sims2023.Serialization;
using System;
using System.ComponentModel;

namespace Sims2023.Domain.Models
{
    public class Voucher : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Tour Tour { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }
        public enum VoucherType { FiveReservations, CancelingTour }
        public VoucherType Name { get; set; }
        public string AdditionalComment { get; set; }
        public bool IsUsed { get; set; }
        public Voucher() { }

        public Voucher(VoucherType name,User user,Tour tour)
        {
            if (name == VoucherType.FiveReservations)
            {
                Name = VoucherType.FiveReservations;
                AdditionalComment = "After five reservations you get this coupon";
                Expired = DateTime.Now.AddMonths(6);
            }
            if (name == VoucherType.CancelingTour)
            {
                Name = VoucherType.CancelingTour;
                AdditionalComment = "After guide cancel tour you get this coupon";
                Expired = DateTime.Now.AddMonths(12);
            }
            User = user;
            Tour = tour;
            Created= DateTime.Now;
            IsUsed = false;

        }
        public Voucher(int id,VoucherType name, User user, Tour tour, DateTime created, DateTime expired, string additionalComment, bool isUsed)
        {
            //refactor later
            Id = id;
            User = user;
            Tour = tour;
            Created = created;
            Expired = expired;
            Name = name;
            AdditionalComment = additionalComment;
            IsUsed = isUsed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User = new()
            {
                Id = Convert.ToInt32(values[1])
            };
            Tour = new()
            {
                Id = Convert.ToInt32(values[2])
            };
            Created = DateTime.Parse(values[3]);
            Expired = DateTime.Parse(values[4]);
            Name = (VoucherType)Enum.Parse(typeof(VoucherType), values[5]);
            AdditionalComment = values[6];
            IsUsed = bool.Parse(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                User.Id.ToString(),
                Tour.Id.ToString(),
                Created.ToString(),
                Expired.ToString(),
                Name.ToString(),
                AdditionalComment,
                IsUsed.ToString()
            };
            return csvValues;
        }
    }
}
