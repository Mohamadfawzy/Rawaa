using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Android.Views;

namespace Rawaa.Droid
{
    [Activity(Label = "Rawaa", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // addd this
            Window.DecorView.LayoutDirection = LayoutDirection.Ltr;
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer:true);
            Window.SetNavigationBarColor(Xamarin.Forms.Color.FromHex("#2B388F").ToAndroid());//2B388F
            this.SetStatusBarColor(Xamarin.Forms.Color.FromHex("#2B388F").ToAndroid()); // red cb1901

            // end add
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}