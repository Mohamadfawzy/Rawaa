using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rawaa.ViewModels
{
    public  class ProductDetailsPageVM : BaseViewModel
    {
         public RequestProvider<Product> requestProvider = new RequestProvider<Product>();
        Product meal;
        public Product Meal
        {
            get { return meal; }
            set { SetProperty(ref meal,value); }
        }

        public ProductDetailsPageVM()
        {
            //Meal = new Product();
            Meal = ProductsPageVM.StaticSelectedProduct;
            RefreshCountBasket();

        }


        private async Task FetchCategory()
        {
            //var list = requestProvider.GetListAsync($"{AppSettings.currentLang}/api/client/Category/all");
            //FoodMenu = list.Result.ToList();
            
            OnPropertyChanged("FoodMenu");
        }

    }
}
