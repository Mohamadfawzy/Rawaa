using Rawaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rawaa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    Shell.Current.GoToAsync("SettingPage",false);
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Carousel.TranslationY = 0;
            Carousel.Opacity = 1;
            (BindingContext as HomePageVM).SliderIsLoop = true;
            (BindingContext as HomePageVM).OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as HomePageVM).SliderIsLoop = false ;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("SearchPage", false);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Carousel.TranslationY = e.ScrollY / 2;
            var s = Carousel.Opacity = Math.Abs((e.ScrollY /255 * 1)-1);
            Console.WriteLine("opasity =================="+s);
            Console.WriteLine("Y="+ (int)e.ScrollY) ;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            IconTintColorEffect.SetTintColor(logo, Color.Red);
            Task.Run(async () =>
            {
                await Task.Delay(500);
                IconTintColorEffect.SetTintColor(logo, Color.White);
            });
        }
    }
}