using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class Staff
    {
        public Staff()
        {
            InverseManager = new HashSet<Staff>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Active { get; set; }
        public int RestaurantId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Staff? Manager { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<Staff> InverseManager { get; set; }
    }
}
