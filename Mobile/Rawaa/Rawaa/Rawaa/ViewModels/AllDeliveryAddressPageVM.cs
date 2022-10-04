using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rawaa.ViewModels
{
    public class AllDeliveryAddressPageVM : BaseViewModel
    {

        public RequestProvider<DeliveryAddress> requestProvider = new RequestProvider<DeliveryAddress>();

        public ObservableCollection<DeliveryAddress> ListAddress { get; set; }

        DeliveryAddress selectedCartItem;
        public DeliveryAddress SelectedCartItem
        {
            get { return selectedCartItem; }
            set { SetProperty(ref selectedCartItem, value); }
        }

        bool selectedAddressVisible;
        public bool SelectedAddressVisible
        {
            get { return selectedAddressVisible; }
            set { SetProperty(ref selectedAddressVisible, value); }
        }

        // commands 
        public ICommand SelectedItemCommand => new Command<DeliveryAddress>(SelectedItemExcuted);
        public ICommand DeleteItemCommand => new Command<DeliveryAddress>(DeleteItemExcuted);
        public ICommand SelectedThisAddressCommand => new Command<DeliveryAddress>(SelectedThisAddressExcuted);
        public ICommand AddNewAddressCommand => new Command(AddNewAddressExcuted);

        // ctor
        public AllDeliveryAddressPageVM()
        {
            ListAddress = new ObservableCollection<DeliveryAddress>();
            Fetch();
        }

        // feth
        private async void Fetch()
        {
            IsBusy = true;
            var list = await requestProvider.GetListAsync($"api/client/DeliveryAddress/all/user/" + AppSettings.UserId);

            if (list == null || list.Count < 1)
            {
                IsBusy = false;
                return;
            }
            foreach (var item in list)
            {
                if(item.Id.ToString() == AppSettings.AddressId)
                {
                    item.IsSelected = true;
                }
                ListAddress.Add(item);
            }
            //ListAddress = list;
            OnPropertyChanged("ListAddress");
            IsBusy = false;
        }

        private async void SelectedItemExcuted(DeliveryAddress parm)
        {
            if (SelectedCartItem == null) 
                return;
            //await Shell.Current.GoToAsync($"ProductDetailsPage");
            SelectedCartItem = null;
        }
        private async void SelectedThisAddressExcuted(DeliveryAddress parm)
        {
            IsBusy = true;
            var index = ListAddress.IndexOf(parm);
           

            foreach (var item in ListAddress)
            {
                if (item.IsSelected)
                {
                    item.IsSelected=false;
                }
            }

            ListAddress[index].IsSelected = true;
            AppSettings.AddressId = parm.Id.ToString();

            IsBusy = false;
        }

        private void DeleteItemExcuted(DeliveryAddress parm)
        {
            AppSettings.Alert("delete action");
        }

        private async void AddNewAddressExcuted()
        {
            await Shell.Current.GoToAsync($"CreateDeliveryAddressPage");
        }

    }
}
