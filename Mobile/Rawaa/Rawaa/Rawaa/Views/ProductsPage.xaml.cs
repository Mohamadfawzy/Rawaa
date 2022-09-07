using Rawaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rawaa.Views
{
    [QueryProperty(nameof(SelectedCategorytID), "category")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public int SelectedCategorytID
        {
            set
            {
                LoadAnimal(value);
            }
        }
        public ProductsPage()
        {
            InitializeComponent();
        }


        void LoadAnimal(int name)
        {
            try
            {
                //DisplayAlert("", "id=" + name.ToString(), "ok"); ;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load animal.");
            }
        }
    }
}