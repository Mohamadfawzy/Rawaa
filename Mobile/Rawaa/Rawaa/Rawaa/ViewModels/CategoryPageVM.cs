using Rawaa.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rawaa.ViewModels
{
    public  class CategoryPageVM : BaseViewModel
    {
        List<Category> categories = new List<Category>()
        {
            new Category{Image = "m1.jpg",Title="بيتزا"},
            new Category{Image = "m2.jpg",Title="بيتزا"},
            new Category{Image = "m1.jpg",Title="بيتزا"},
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
        public  List<Category> Categories { get; set; }
        public CategoryPageVM()
        {
            Categories = new List<Category>();
            RefreshCountBasket();
            Categories = categories;
        }



    }
}
