using Rawaa.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace Rawaa.Models
{
    public class DeliveryAddress : BaseViewModel
    {
        public int Id { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public byte FloorrUmber { get; set; }
        public byte ApartmentNumber { get; set; }
        public string Notes { get; set; }
        public string ShortName { get; set; }
        public bool IsDeleted { get; set; }
        public int CustomerId { get; set; }

        // 
        bool isSelected ;
        [JsonIgnore]
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }
    }
}
