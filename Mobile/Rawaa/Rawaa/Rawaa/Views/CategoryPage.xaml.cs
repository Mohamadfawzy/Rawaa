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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as CategoryPageVM).RefreshCountBasket();
        }
    }
}