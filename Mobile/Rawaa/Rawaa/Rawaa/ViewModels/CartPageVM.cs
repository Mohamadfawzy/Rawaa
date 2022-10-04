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
        Cart selectedCartItem;
        public Cart SelectedCartItem
        {
            get { return selectedCartItem; }
            set { SetProperty(ref selectedCartItem, value); }
        }

        double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set { SetProperty(ref totalPrice, value); }
        }
        bool emptyIsVisible;
        public bool EmptyIsVisible
        {
            get { return emptyIsVisible; }
            set { SetProperty(ref emptyIsVisible, value); }
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
        public ICommand SelectedItemCommand => new Command<Cart>(SelectedItemExcuted);
        public ICommand DeleteItemCommand => new Command<Cart>(DeleteItemExcuted);
        public ICommand ContinueRequestCommand => new Command(ContinueRequestExcuted);
        
        // ctor ------------------------------------------------------------------
        public CartPageVM()
        {
            Carts = new ObservableCollection<Cart>();
            FetchCartItems();
        }

        // feth
        private async Task FetchCartItems()
        {
            IsBusy = true;
            var list = await requestProvider.GetListAsync($"{AppSettings.currentLang}/api/client/cart/all/"+AppSettings.UserId);
            if (list == null || list.Count <1)
            {
                EmptyIsVisible = true;
                IsBusy = false;
                return;
            }
            foreach(var item in list)
            {
                var priceQuantity = 0.0D;
                switch (item.Size)
                {
                    case 1:
                        item.Price = item.Product.SmallSizePrice;
                        item.SizeName = "صغير";
                        priceQuantity += item.Product.SmallSizePrice * item.Quantity;
                        break;
                    case 2:
                        item.Price = item.Product.MediumSizePrice;
                        item.SizeName = "متوسط";
                        priceQuantity += item.Product.MediumSizePrice * item.Quantity;
                        break;
                    case 3:
                        item.Price = item.Product.BigSizePrice;
                        item.SizeName = "كبير";
                        priceQuantity += item.Product.BigSizePrice * item.Quantity;
                        break;
                }
                Carts.Add(item);
                TotalPrice += priceQuantity;
            }

            OnPropertyChanged("Carts");
            IsBusy = false;
        }

        // plus Quantity++
        private void PlusExcuted(Cart parm)
        {
            var index = Carts.IndexOf(parm);
            if (Carts[index].Price * (Carts[index].Quantity + 1) >= 999)
            {
                AppSettings.Alert("max");
                return;
            }
            Carts[index].Quantity++;
            TotalPrice += GetSelectedSizePrice(parm.Size, parm.Product);
        }
        // Minus Quantity--
        private void MinusExcuted(Cart parm)
        {
            var index = Carts.IndexOf(parm);
            if (Carts[index].Quantity <= 1)
                return;
            Carts[index].Quantity--;
            TotalPrice -= GetSelectedSizePrice(parm.Size, parm.Product);
        }

        private async void SelectedItemExcuted(Cart parm)
        {
            if (SelectedCartItem == null) return;
            ProductDetailsPageVM.Initializer(parm.Product);
            await Shell.Current.GoToAsync($"ProductDetailsPage");
            SelectedCartItem = null;
        }

        private void DeleteItemExcuted(Cart parm)
        {
            AppSettings.Alert("delete action");
        }

        private async void ContinueRequestExcuted()
        {
            IsBusy = true;
            var list = await requestProvider.GetListAsync($"api/client/DeliveryAddress/all/user/" + AppSettings.UserId);

            if (list == null || list.Count < 1)
            {
                IsBusy = false;
                await Shell.Current.GoToAsync("CreateDeliveryAddressPage");
                return;
            }
            await Shell.Current.GoToAsync("AllDeliveryAddressPage");
        }
        public void UpdateCartData()
        {

        }

        double GetSelectedSizePrice(int size, Product item)
        {
            var price = 0.0D;
            switch (size)
            {
                case 1:
                    price = item.SmallSizePrice;
                    break;
                case 2:
                    price = item.MediumSizePrice;
                    break;
                case 3:
                    price = item.BigSizePrice;
                    break;
            }
            return price;

        }


    }
}
