using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Orders = new HashSet<Order>();
            staff = new HashSet<Staff>();
        }

        public int Id { get; set; }
        public string NameAr { get; set; } = null!;
        public string? NameEn { get; set; }
        public string? Phone { get; set; }
        public byte? State { get; set; }
        public string? Governorate { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Staff> staff { get; set; }
    }
}
