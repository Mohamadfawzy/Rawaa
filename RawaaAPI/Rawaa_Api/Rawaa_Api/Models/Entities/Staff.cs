using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class Staff
    {
        public Staff()
        {
            InverseManager = new HashSet<Staff>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreateOn { get; set; }
        public bool Active { get; set; }
        public string? Jop { get; set; }
        public int RestaurantId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Staff? Manager { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<Staff> InverseManager { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
