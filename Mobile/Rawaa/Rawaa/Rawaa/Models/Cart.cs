using Newtonsoft.Json;
using Rawaa.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rawaa.Models
{
    public class Cart : INotifyPropertyChanged
    {
        public int CustomerId { get; set; } = 0;
        public int ProductId { get; set; } = 0;

        int _quantity = 1;
        public int Quantity
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

        // INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName] string propertyName = "",
           Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
