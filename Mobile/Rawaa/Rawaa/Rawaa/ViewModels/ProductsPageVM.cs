using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rawaa.ViewModels
{
    public class ProductsPageVM : BaseViewModel
    {
        
        public RequestProvider<Product> requestProvider = new RequestProvider<Product>();
        List<Product> produts = new List<Product>()
        {
           new Product { Image = "m2.jpg", Calories = 1234, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.50 },
            new Product { Image = "m1.jpg", Calories = 1234, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.99 },
            new Product { Image = "p1.jpg", Calories = 1241, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.50 },
            new Product { Image = "p2.jpg", Calories = 1341, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.99 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.50 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "met", SmallSizePrice = 21.50 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "met", SmallSizePrice = 21.50 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "bitza", SmallSizePrice = 21.50 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "bitza", SmallSizePrice = 21.50 },
        };
        public List<Product> Produts { get; set; } = new List<Product>();
        public ProductsPageVM()
        {
            RefreshCountBasket();
            Task.Run(() => FetchProducts(CategoryPageVM.StaticSelectedCategory.Id));
            //Produts = produts;

        }
        public static Product StaticSelectedProduct;

        // productes
        public async void FetchProducts(int id)
        {

                //await Task.Delay(0);
                StaticSelectedProduct = new Product();
                var list = await requestProvider.GetListAsync($"{AppSettings.currentLang}/api/client/Product/all/{id}");
                Produts = list.ToList();
                OnPropertyChanged("Produts");

        }
    }
}
