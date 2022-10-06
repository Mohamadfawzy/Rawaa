using Rawaa.Helper;
using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rawaa.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        [JsonIgnore]
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        [JsonIgnore]
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string cartIcon = Fonts.IconFont.Cart;
        [JsonIgnore]
        public string CartIcon
        {
            get { return cartIcon; }
            set { SetProperty(ref cartIcon, value); }
        }


        // Cart visible
        bool countBasketVisible = false;
        [JsonIgnore]
        public bool CountBasketVisible
        {
            get { return countBasketVisible; }
            set { SetProperty(ref countBasketVisible, value); }
        }

        // Cart count
        string countBasket = "0";
        [JsonIgnore]
        public string CountBasket
        {
            get { return countBasket; }
            set { SetProperty(ref countBasket, value); }
        }

        FlowDirection currentFlowDirection = FlowDirection.RightToLeft;
        public FlowDirection CurrentFlowDirection
        {
            get
            {
                if (LocalizationResourceManager.storedLanguageName == "ar")
                    return currentFlowDirection;
                return FlowDirection.LeftToRight;

            }
            set { SetProperty(ref currentFlowDirection, value); }
        }

        // Command --------------------------------------------
        public ICommand GoToBasketPageCommand => new Command(GoToBasketPage);
        public ICommand GoToSearchPageCommand => new Command(GoToSearchtPage);
        public ICommand AddProductToBasket => new Command<Product>(ExceuteAddProductToBasket);

        // Execute Methodes -----------------------------------
        protected void GoToBasketPage()
        {
            Shell.Current.GoToAsync("CartPage");
        }
        protected void GoToSearchtPage()
        {
            Shell.Current.GoToAsync("SearchPage", false);
        }

        public async void ExceuteAddProductToBasket(Product product)
        {
            var r = await RequestServices.PostProductToCart(product);
            AppSettings.countOfCart++;
            RefreshCountBasket();
        }
        public void RefreshCountBasket()
        {
            Task.Run(() =>
            {
                IsBusy = true;
                var url = $"ar/api/client/cart/quantity/";


                CountBasket = RequestServices.GetCountOfProductInCart(url);
                CountBasketVisible = true;


                IsBusy = false;
            });
        }


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
