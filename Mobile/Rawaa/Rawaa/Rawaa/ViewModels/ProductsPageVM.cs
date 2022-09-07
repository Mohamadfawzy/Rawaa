using Rawaa.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rawaa.ViewModels
{
    public class ProductsPageVM : BaseViewModel
    {
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
            Produts = produts;
        }
    }
}
