using Rawaa.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rawaa.ViewModels
{
    public  class CategoryPageVM : BaseViewModel
    {
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
        public CategoryPageVM()
        {
            Categories = new List<Category>();
            RefreshCountBasket();
            Categories = categories;
        }


        // private methonds
        private async void SelectedCategoryExecute(Category item)
        {
            if (SelectedCategory == null) return;
            await Shell.Current.GoToAsync($"ProductsPage?category={item.Id}");
            SelectedCategory = null;
        }

    }
}
