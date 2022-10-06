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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        //[QueryProperty(nameof(SelectedCategorytID), "category")]
        //public int SelectedCategorytID
        //{
        //    set
        //    {
        //        Load(value);
        //    }
        //}
        //void Load(int id)
        //{
        //    try
        //    {
        //        (BindingContext as ProductsPageVM).FetchProducts(id);
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Failed to load animal.");
        //    }
        //}

        public ProductsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as ProductsPageVM).RefreshCountBasket();
        }
        private async void MealsCV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item =e.CurrentSelection.FirstOrDefault() as Product;
                //ProductsPageVM.StaticSelectedProduct = item;
                ProductDetailsPageVM.Initializer(item);
                await Shell.Current.GoToAsync($"ProductDetailsPage");

            }
            catch (Exception ex )
            {

                await AppSettings.Alert(ex.Message);
            }
        }
    }
}