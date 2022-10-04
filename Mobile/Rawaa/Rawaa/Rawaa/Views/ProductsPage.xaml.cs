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
    //[QueryProperty(nameof(SelectedCategorytID), "category")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public int SelectedCategorytID
        {
            set
            {
                Load(value);
            }
        }
        public ProductsPage()
        {
            InitializeComponent();
        }


        void  Load(int id)
        {
            try
            {
                (BindingContext as ProductsPageVM).FetchProducts(id);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load animal.");
            }
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

                AppSettings.Alert(ex.Message);
            }
        }
    }
}