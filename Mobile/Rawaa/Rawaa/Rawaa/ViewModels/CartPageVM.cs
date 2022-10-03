using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Rawaa.ViewModels
{
    public  class CartPageVM : BaseViewModel
    {
        
        public RequestProvider<Cart> requestProvider = new RequestProvider<Cart>();


        public ObservableCollection<Cart> carts;
        
        public ObservableCollection<Cart> Carts { get; set; }
        //{
        //    get { return carts; }
        //    set { SetProperty(ref carts, value); }
        //}

        double price;
        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        double _quantity = 1;
        public double Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        double selectedSize;
        public double SelectedSizePrice
        {
            get { return selectedSize; }
            set { SetProperty(ref selectedSize, value); }
        }

        public ICommand Plus_Command => new Command<Cart>(PlusExcuted);
        public ICommand Minus_Command => new Command<Cart>(MinusExcuted);
        // ctor
        public CartPageVM()
        {
            Carts = new ObservableCollection<Cart>();
            FetchCategory();
        }

        // feth
        private async Task FetchCategory()
        {
            var list = await requestProvider.GetListAsync($"{AppSettings.currentLang}/api/client/cart/all/"+AppSettings.UserId);
            if (list == null) return;
            foreach(var item in list)
            {
                switch (item.Size)
                {
                    case 1:
                        item.Price = item.Product.SmallSizePrice;
                        item.SizeName = "صغير";
                        break;
                    case 2:
                        item.Price = item.Product.MediumSizePrice;
                        item.SizeName = "متوسط";
                        break;
                    case 3:
                        item.Price = item.Product.BigSizePrice;
                        item.SizeName = "كبير";
                        break;
                }
                Carts.Add(item);
            }

            OnPropertyChanged("Carts");
        }

        private void PlusExcuted(Cart parm)
        {
            var index = Carts.IndexOf(parm);
            if (Carts[index].Price * (Carts[index].Quantity + 1) >= 999)
            {
                AppSettings.Alert("max");
                return;
            }
            Carts[index].Quantity++;
            Carts[index].Price = Carts[index].Price * Carts[index].Quantity;
        }

        private void MinusExcuted(Cart parm)
        {
            var index = Carts.IndexOf(parm);
            if (Carts[index].Quantity <= 1)
                return;
            Carts[index].Quantity--;
            Carts[index].Price = Carts[index].Price * Carts[index].Quantity;

            OnPropertyChanged("Carts");
        }

        public void UpdateCartData()
        {

        }


    }
}
