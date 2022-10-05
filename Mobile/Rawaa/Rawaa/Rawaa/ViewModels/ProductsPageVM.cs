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
        public List<Product> Produts { get; set; } = new List<Product>();
        public ProductsPageVM()
        {
            RefreshCountBasket();
            FetchProducts(CategoryPageVM.StaticSelectedCategory.Id);

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
