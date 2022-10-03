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
        public RequestProvider<Product> requestProvider = new RequestProvider<Product>();
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

        public ICommand Plus_Command => new Command(PlusExcuted);
        public ICommand Minus_Command => new Command(MinusExcuted);

        // ctor
        public ProductDetailsPageVM()
        {
            //Meal = new Product();
            Meal = ProductsPageVM.StaticSelectedProduct;
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

        }

        private void MinusExcuted()
        {
            if (Quantity <= 1)
                return;
            Quantity--;
            Price = SelectedSizePrice * Quantity;
        }

        public void CalculatePrice()
        {
            Price = SelectedSizePrice * Quantity;

            if (selectedSize * (_quantity + 1) >= 999)
            {
                AppSettings.Alert("max");
                while(price > 999)
                {
                    MinusExcuted();
                }
                return;
            }
        }
        private async Task FetchCategory()
        {
            //var list = requestProvider.GetListAsync($"{AppSettings.currentLang}/api/client/Category/all");
            //FoodMenu = list.Result.ToList();

            OnPropertyChanged("FoodMenu");
        }

    }
}
