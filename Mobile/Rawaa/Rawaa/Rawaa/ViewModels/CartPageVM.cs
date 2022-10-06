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
using Rawaa.Views;

namespace Rawaa.ViewModels
{
    public class CartPageVM : BaseViewModel
    {
        public RequestProvider<Cart> requestProvider = new RequestProvider<Cart>();
        public ObservableCollection<Cart> Carts { get; set; }

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
        bool contentsVisible = true;
        public bool ContentsVisible
        {
            get { return contentsVisible; }
            set { SetProperty(ref contentsVisible, value); }
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
        private async void FetchCartItems()
        {
            IsBusy = true;
            var list = await requestProvider.GetListAsync($"{AppSettings.currentLang}/api/client/cart/all/" + AppSettings.UserId);
            if (list == null || list.Count < 1)
            {
                EmptyIsVisible = true;
                ContentsVisible = false;
                IsBusy = false;
                return;
            }
            foreach (var item in list)
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
                        item.Price = (double)item.Product.MediumSizePrice;
                        item.SizeName = "متوسط";
                        priceQuantity += (double)item.Product.MediumSizePrice * item.Quantity;
                        break;
                    case 3:
                        item.Price = (double)item.Product.BigSizePrice;
                        item.SizeName = "كبير";
                        priceQuantity += (double)item.Product.BigSizePrice * item.Quantity;
                        break;
                }
                Carts.Add(item);
                TotalPrice += priceQuantity;
            }

            OnPropertyChanged("Carts");
            IsBusy = false;
        }

        // plus Quantity++
        private async void PlusExcuted(Cart parm)
        {
            var index = Carts.IndexOf(parm);
            if (totalPrice + (Carts[index].Price + (Carts[index].Quantity + 1)) >= 999)
            {
                await AppSettings.Alert("max",2);
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
            ProductDetailsPageVM.Initializer(parm.Product, parm);
            await Shell.Current.GoToAsync($"ProductDetailsPage");
            SelectedCartItem = null;
        }

        private async void DeleteItemExcuted(Cart parm)
        {
            var url = $"ar/api/client/cart/{AppSettings.UserId}/{parm.ProductId}";
            var res = await requestProvider.DeleteOneAsync(url);
            if (res)
            {
                Carts.Remove(parm);
                OnPropertyChanged("Carts");
            }
            if (Carts.Count < 1)
            {
                EmptyIsVisible = true;
            }
            await AppSettings.Alert($"delete action is: {res}");
        }

        private async void ContinueRequestExcuted()
        {
            IsBusy = true;
            var url = $"api/client/DeliveryAddress/all/user/" + AppSettings.UserId;
            var list = await requestProvider.GetListAsync(url);

            if (list == null || list.Count < 1)
            {
                IsBusy = false;
                await Shell.Current.GoToAsync("CreateDeliveryAddressPage");
                return;
            }
            else
            {
                if (AppSettings.AddressId == "0")
                    await Shell.Current.GoToAsync("AllDeliveryAddressPage");
                else
                {
                    AppSettings.staticTotalPrice = TotalPrice;
                    OrderDetailsPageVM.Initializer(Carts.ToList());
                    await Shell.Current.GoToAsync("OrderDetailsPage");
                }
            }
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
                    price = (double)item.MediumSizePrice;
                    break;
                case 3:
                    price = (double)item.BigSizePrice;
                    break;
            }
            return price;

        }


    }
}
