using Rawaa.Models;
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
    public  class CategoryPageVM : BaseViewModel
    {
        
        
        public RequestProvider<Category> requestProvider = new RequestProvider<Category>();
        public List<Category> FoodMenu { get; set; }

        Category selectedCategory = new Category();
        public Category SelectedCategory
        {
            get {return selectedCategory;}
            set { SetProperty(ref selectedCategory, value); }
        }

        List<Category> categories = new List<Category>()
        {
            new Category{Id=1,Image = "m1.jpg",Title="بيتزا"},
            new Category{Id=2,Image = "m2.jpg",Title="بيتزا"},
            new Category{Id=3,Image = "m1.jpg",Title="بيتزا"},
            new Category{Image = "m4.jpg",Title="بيتزا"},
            new Category{Image = "m2.jpg",Title="بيتزا"},
            new Category{Image = "m3.jpg",Title="بيتزا"},
            new Category{Image = "m1.jpg",Title="بيتزا"},
            new Category{Image = "m2.jpg",Title="بيتزا"},
            new Category{Image = "m1.jpg",Title="بيتزا"},
            new Category{Image = "m3.jpg",Title="بيتزا"},
            new Category{Image = "m2.jpg",Title="بيتزا"},
            new Category{Image = "m3.jpg",Title="بيتزا"},
        };
        // Command 
        public ICommand SelectedCategoryCommand => new Command<Category>(SelectedCategoryExecute);
        public  List<Category> Categories { get; set; }
        public static Category StaticSelectedCategory;
        public CategoryPageVM()
        {
            FoodMenu = new List<Category>();
            Categories = new List<Category>();
            StaticSelectedCategory = new Category();
            Categories = categories;
            Task.Run(() => FetchCategory());
            
        }

        public void OnAppearing()
        {
            RefreshCountBasket();
        }

        // private methonds
        private async void SelectedCategoryExecute(Category item)
        {
            try
            {
                if (SelectedCategory == null) return;
                StaticSelectedCategory = selectedCategory;
                await Shell.Current.GoToAsync($"ProductsPage");
                SelectedCategory = null;
            }
            catch (Exception ex )
            {

                await AppSettings.Alert(ex.Message);
            }
           
        }

        // Categoreis
        private async Task FetchCategory()
        {
            var list = await requestProvider.GetListAsync($"{AppSettings.currentLang}/api/client/Category/all");
            FoodMenu = list.ToList();
            OnPropertyChanged("FoodMenu");
        }



    }
}
