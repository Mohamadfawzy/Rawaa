using Rawaa.Models;
using Rawaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rawaa.Views
{
    [QueryProperty(nameof(SelectedProduct), "product")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsPage : ContentPage
    {
        public Product SelectedProduct
        {
            set
            {
                (BindingContext as ProductDetailsPageVM).Meal = value;
                //LoadAnimal(value);
            }
        }
        public ProductDetailsPage()
        {
            InitializeComponent();
        }
    }
}