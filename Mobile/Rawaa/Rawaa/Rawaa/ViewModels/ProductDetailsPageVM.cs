using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rawaa.ViewModels
{
    public class ProductDetailsPageVM : BaseViewModel
    {
        public RequestProvider<Cart> requestProvider = new RequestProvider<Cart>();
        public Cart CartOption = new Cart();
        Product meal;
        public Product Meal
        {
            get { return meal; }
            set { SetProperty(ref meal, value); }
        }

        double price;
        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        int _quantity = 1;
        public int Quantity
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

        public ICommand Plus_Command => new Command(PlusExcuted);
        public ICommand Minus_Command => new Command(MinusExcuted);
        public ICommand AddProductToBasketCommand => new Command(AddProductToBasketexEcuted);

        // ctor
        public ProductDetailsPageVM()
        {
            //Meal = new Product();
            Meal = staticProduct;//ProductsPageVM.StaticSelectedProduct;
            price = meal.SmallSizePrice;
            RefreshCountBasket();
            SelectedSizePrice = meal.SmallSizePrice;
        }

        // quantity section
        private void PlusExcuted()
        {
            if (selectedSize * (_quantity + 1) >= 999)
            {
                AppSettings.Alert("max");
                return;
            }
            Quantity++;
            Price = SelectedSizePrice * Quantity;

            CartOption.Quantity = Quantity;

        }

        private void MinusExcuted()
        {
            if (Quantity <= 1)
                return;
            Quantity--;
            Price = SelectedSizePrice * Quantity;

            CartOption.Quantity = Quantity;
        }

        public void CalculatePrice()
        {
            Price = SelectedSizePrice * Quantity;

            if (selectedSize * (_quantity + 1) >= 999)
            {
                AppSettings.Alert("max");
                while (price > 999)
                {
                    MinusExcuted();
                }
                return;
            }
        }

        private static Product staticProduct = new Product();
        public static void Initializer(Product product)
        {
            staticProduct = product;
        }

        private async void AddProductToBasketexEcuted()
        {
            IsBusy = true;
            CartOption.ProductId = meal.Id;
            CartOption.CustomerId = Convert.ToInt32(AppSettings.UserId);
            
            var rs = await requestProvider.PostOneAsync<Cart>(CartOption, $"{AppSettings.currentLang}/api/client/cart");
            if (rs != null)
            {
                await AppSettings.Alert("added",2);
                IsBusy = false;
                AppSettings.countOfCart++;
                RefreshCountBasket();
                return;
            }
            await AppSettings.Alert("Can not added",2);


        }
        private async Task FetchCategory()
        {
            //var list = requestProvider.GetListAsync($"{AppSettings.currentLang}/api/client/Category/all");
            //FoodMenu = list.Result.ToList();

            OnPropertyChanged("FoodMenu");
        }

    }
}
