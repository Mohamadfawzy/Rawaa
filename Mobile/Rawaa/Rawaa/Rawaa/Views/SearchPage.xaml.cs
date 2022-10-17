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
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("..", false);
            return true;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(() => SetFocus());
            (BindingContext as SearchPageVM).RefreshCountBasket();
        }

        async Task SetFocus()
        {
            await Task.Delay(70);
            entrySearch.Focus();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (Product)e.CurrentSelection.FirstOrDefault();
                if (item == null) return;
                ProductDetailsPageVM.Initializer(item);
                await Shell.Current.GoToAsync($"ProductDetailsPage");
                item = null;
            }
            catch (Exception ex)
            {

                await AppSettings.Alert(ex.Message);
            }
           
        }
    }
}