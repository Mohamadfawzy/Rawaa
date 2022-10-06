using Rawaa.Helper;
using Rawaa.Models;
using Rawaa.Resources.Languages;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace Rawaa.ViewModels
{
    public class OrderDetailsPageVM : BaseViewModel
    {
        private RequestProvider<Order> requestProvider = new RequestProvider<Order>();

        static bool isCart;
        static List<Cart> staticCart = new List<Cart>();
        static Order staticOrder = new Order();


        bool cancellingOrderIsVisible = false;
        public bool CancellingOrderIsVisible
        {
            get { return cancellingOrderIsVisible; }
            set { SetProperty(ref cancellingOrderIsVisible, value); }
        }

        bool buttonsVisible = true;
        public bool ButtonsVisible
        {
            get { return buttonsVisible; }
            set { SetProperty(ref buttonsVisible, value); }
        }

        double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set { SetProperty(ref totalPrice, value); }
        }

        double deliveryFee;
        public double DeliveryFee
        {
            get { return deliveryFee; }
            set { SetProperty(ref deliveryFee, value); }
        }

        double totalBill;
        public double TotalBill
        {
            get { return totalBill; }
            set { SetProperty(ref totalBill, value); }
        }

        // Properteis public 
        Order _order;
        public Order Order
        {
            get { return _order; }
            set { SetProperty(ref _order, value); }
        }
        public ObservableCollection<Order> List { get; set; }


        // Commands
        public ICommand ConformOrderCommand => new Command(ConformOrder);
        public ICommand CancelOrderCommand => new Command(Cancelrder);

        // ctor
        public OrderDetailsPageVM()
        {
            Order = new Order();
            OnAppearing();
        }

        public static void Initializer(List<Cart> carts)
        {
            isCart = true;
            staticCart = carts;
        }

        public static void Initializer(Order order)
        {
            isCart = false;
            staticOrder = order;
        }

        private void OnAppearing()
        {
            if (isCart)
                ComingConformNewOrder();
            else
                ComingOldOrderDetails();
        }

        private void ComingConformNewOrder()
        {
            TotalPrice = AppSettings.staticTotalPrice;
            DeliveryFee = FetchDeliveryFee();
            TotalBill = deliveryFee + totalPrice;
            FetchSelectedAddressById(AppSettings.AddressId);
            CancellingOrderIsVisible = false;
        }

        private void ComingOldOrderDetails()
        {
            Order = staticOrder;
            TotalPrice = staticOrder.Total;
            DeliveryFee = FetchDeliveryFee();
            if (staticOrder.DeliveryFee == null)
                TotalBill = totalPrice + FetchDeliveryFee();
            else
                TotalBill = (double)staticOrder.DeliveryFee + totalPrice;
            CancellingOrderIsVisible = true;
            FetchSelectedAddressById(staticOrder.DeliveryAddressId.ToString());
            HandleUI();
        }

        private void FetchSelectedAddressById(string deliveryAddressId)
        {
            Task.Run(async () =>
            {
                var request = new RequestProvider<DeliveryAddress>();
                var url = "api/client/DeliveryAddress/";
                var res = await request.GetById(url, deliveryAddressId);
                if (res != null)
                {
                    Order.DeliveryAddress = res;
                }
            });
        }

        private double FetchDeliveryFee()
        {
            return 14;
        }

        private async void ConformOrder()
        {
            var request = new RequestProvider<Order>();
            var order = new Order()
            {
                OrderStatus = 1,
                DeliveryFee = FetchDeliveryFee(),
                PymentMethod = 1,
                CustomerId = Convert.ToInt32(AppSettings.UserId),
                DeliveryAddressId = Convert.ToInt32(AppSettings.AddressId),
                OrderDetails = new List<OrderDetails>()
            };

            foreach (var item in staticCart)
            {
                var od = new OrderDetails()
                {
                    ProductId = item.ProductId,
                    Taste = item.Taste,
                    Size = item.Size,
                    Quantity = item.Quantity,
                    DrinkId = item.DrinkId,
                };
                order.OrderDetails.Add(od);
            }

            var url = $"ar/api/client/Order";
            var res = await request.PostOneAsync(order, url);

            if (res != null)
            {
                RefreshAlgorithms(res);
            }
        }

        private async void Cancelrder()
        {
            var request = new RequestProvider<Order>();
            var order = new Order()
            {
                Id = _order.Id,
                OrderStatus = 5,
                CustomerId = Convert.ToInt32(AppSettings.UserId),
                DeliveryFee = FetchDeliveryFee()
            };


            var url = $"en/api/client/Order/OrderStatus";
            var res = await request.PutOneAsync(order,url);
            if (res != null)
            {
                _order = res;
                MyHelper.HandleStatuseName(ref _order);
                RefreshAlgorithms(res);
                OnPropertyChanged("Order");
            }
          
        }

        private void RefreshAlgorithms(Order order)
        {
            if (order.DeliveryFee == null)
                order.DeliveryFee = FetchDeliveryFee();
            TotalPrice = order.Total;
            DeliveryFee = FetchDeliveryFee();
            TotalBill = (double)order.DeliveryFee + TotalPrice;
            CancellingOrderIsVisible = true;
            HandleUI();
        }

        private void HandleUI()
        {
            if(_order.OrderStatus > 1)
            {
                ButtonsVisible = false;
            }

        }


    }
}
