using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rawaa.Droid.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdnroidSetting))]
namespace Rawaa.Droid.Helper
{
    internal class AdnroidSetting
    {

        Android.App.Activity activity = Platform.CurrentActivity;
        Android.Views.Window window;
        public AdnroidSetting()
        {
            window = activity.Window;
        }

        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(color.ToPlatformColor());

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
            }
        }

        public void SetNavigationBarColor(System.Drawing.Color color)
        {
            window.SetNavigationBarColor(color.ToPlatformColor());
        }

        public void ShowStatusBar(System.Drawing.Color color)
        {
            throw new NotImplementedException();
        }

        public void HideStatusBar()
        {
            throw new NotImplementedException();
        }
    }
}