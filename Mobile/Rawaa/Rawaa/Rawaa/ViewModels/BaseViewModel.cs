using Rawaa.Helper;
using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rawaa.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string cartIcon = Fonts.IconFont.Cart;
        public string CartIcon
        {
            get { return cartIcon; }
            set { SetProperty(ref cartIcon, value); }
        }

        
        // Cart visible
        public bool countBasketVisible = false;
        public bool CountBasketVisible
        {
            get => countBasketVisible;
            set
            {
                if (countBasketVisible == value) return;
                countBasketVisible = value;
                OnPropertyChanged(nameof(CountBasketVisible));
            }
        }

        // Cart count
        public string countBasket = "";
        public string CountBasket
        {
            get => countBasket;
            set
            {
                if (countBasket == value) return;
                countBasket = value;
                OnPropertyChanged(nameof(CountBasket));
            }
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

        // Execute Methodes -----------------------------------
        protected void GoToBasketPage()
        {
            Shell.Current.GoToAsync("SettingPage");
        }
        protected void GoToSearchtPage()
        {
            Shell.Current.GoToAsync("SearchPage",false);
        }
        protected void RefreshCountBasket()
        {
            Task.Run(() =>
            {
                var item = 2;//new BasketCount();
                //item = AllServices.GetCountBasket();
                if (item != null)
                {
                    if (item > 0)
                    {
                        CountBasket = item.ToString();
                        CountBasketVisible = true;
                    }
                    else
                    {
                        CountBasketVisible = false;
                    }
                }
                else
                {
                    CountBasketVisible = false;
                }
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
