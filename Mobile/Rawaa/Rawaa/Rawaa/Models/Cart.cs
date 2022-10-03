using Rawaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rawaa.Models
{
    public class Cart : BaseViewModel
    {
        public int CustomerId { get; set; } = 0;
        public int ProductId { get; set; } = 0;

        double _quantity = 1;
        public double Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
        public byte Taste { get; set; } = 1;
        public byte Size { get; set; } = 1;
        public int DrinkId { get; set; } = 0;
        public DateTime? CreateOn { get; set; }
        public Product Product  { get; set; }

        //
        public double Price { get; set; }
        public string SizeName { get; set; }

    }
}
