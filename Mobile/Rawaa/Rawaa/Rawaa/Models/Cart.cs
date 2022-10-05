using Newtonsoft.Json;
using Rawaa.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rawaa.Models
{
    public class Cart : BaseViewModel
    {
        public int CustomerId { get; set; } = 0;
        public int ProductId { get; set; } = 0;

        byte _quantity = 1;
        public byte Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
        public byte Taste { get; set; } = 0;
        public byte Size { get; set; } = 1;
        public int DrinkId { get; set; } = 1;
        public DateTime? CreateOn { get; set; }

        public Product Product { get; set; }

        // Ignore
        [JsonIgnore]
        public double Price { get; set; }

        [JsonIgnore]
        public string SizeName { get; set; }

    }
}
