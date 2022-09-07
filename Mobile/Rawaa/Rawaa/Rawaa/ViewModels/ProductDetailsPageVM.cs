using Rawaa.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rawaa.ViewModels
{
    public  class ProductDetailsPageVM : BaseViewModel
    {
        Product meal;
        public Product Meal
        {
            get { return meal; }
            set { SetProperty(ref meal,value); }
        }
        public ProductDetailsPageVM()
        {
            RefreshCountBasket();

        }
    }
}
