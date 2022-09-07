using Rawaa.ViewModels;
using Rawaa.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Rawaa
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
            Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
            Routing.RegisterRoute(nameof(ProductDetailsPage), typeof(ProductDetailsPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
        
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
            //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            // Command="{Binding Source={RelativeSource AncestorType={x:Type local:ItemsViewModel}}, Path=ItemTapped}"		

        }

        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), 0);
            set => Preferences.Set(nameof(Theme), value);
        }
    }
}
