using Rawaa.Models;
using Rawaa.Resources.Languages;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rawaa.ViewModels
{
    public class OrdersPageVM : BaseViewModel
    {
        //private RequestProvider<Order> requestProvider = new RequestProvider<Order>();



        List<Order> orders;
        public List<Order> Orders
        {
            get { return orders; }
            set { SetProperty(ref orders, value); }
        }

        Order selectedOrderItem;
        public Order  SelectedOrderItem
        {
            get { return selectedOrderItem; }
            set { SetProperty(ref selectedOrderItem, value); }
        }


        public ICommand SelectedItemCommand => new Command<Order>(SelectedItemExcuted);

        // ctor 
        public OrdersPageVM()
        {
            Fetch();
            
        }
        public void OnAppearing()
        {
            Fetch();
        }
        private async void SelectedItemExcuted(Order parm)
        {
            if (SelectedOrderItem == null)
                return;
            OrderDetailsPageVM.Initializer(parm);
            await Shell.Current.GoToAsync("OrderDetailsPage");
            SelectedOrderItem = null;
        }

        string OrderPending = "#FFF8DC";
        string OrderProcessing = "#E0FFFF";
        string OrderRejected = "#FFF8DC";
        string OrderCompleted = "#CEF09D";
        string OrderCanceled = "#ffcccc";

        string OrderPendingText = "#B8860B";
        string OrderProcessingText = "#B8860B";
        string OrderRejectedText = "#B8860B";
        string OrderCompletedText = "#1C646D";
        string OrderCanceledText = "#ff1a1a";

        private void HandleStatuseName(ref List<Order> ListOrders)
        {
            foreach (var item in ListOrders)
            {
                switch (item.OrderStatus)
                {
                    case 1:
                        item.StatusName = LanguageResources.OrderPending;
                        item.StatuseBackgrounColore = OrderPending;
                        item.StatuseTextColore = OrderPendingText;
                        break;
                    case 2:
                        item.StatusName = LanguageResources.OrderProcessing;
                        item.StatuseBackgrounColore = OrderProcessing;
                        item.StatuseTextColore = OrderProcessingText;
                        break;
                    case 3:
                        item.StatusName = LanguageResources.OrderRejected;
                        item.StatuseBackgrounColore = OrderRejected;
                        item.StatuseTextColore = OrderRejectedText;
                        break;
                    case 4:
                        item.StatusName = LanguageResources.OrderCompleted;
                        item.StatuseBackgrounColore = OrderCompleted;
                        item.StatuseTextColore = OrderCompletedText;
                        break;
                    case 5:
                        item.StatusName = LanguageResources.OrderCanceled;
                        item.StatuseBackgrounColore = OrderCanceled;
                        item.StatuseTextColore = OrderCanceledText;
                        break;
                }
            }
        }
        
        private void Fetch()
        {
            Task.Run(async () =>
            {
                var request = new RequestProvider<Order>();
                var url = $"ar/api/client/order/all/{AppSettings.UserId}";
                var res = await request.GetListAsync(url);
                if (res != null)
                {
                    Orders = res;
                }
                HandleStatuseName(ref orders);
            });
        }

    }
}
