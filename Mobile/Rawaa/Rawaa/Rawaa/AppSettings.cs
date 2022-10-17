using Rawaa.Helper;
using Rawaa_Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using System.Threading.Tasks;

namespace Rawaa
{
    public static class AppSettings
    {
        //https://192.168.1.101:7279
        //http://192.168.1.101:5279
        //https://www.rawaa.somee.com
        public const string ApiUrl = "http://192.168.1.101:9002";
        public const string ImageUrl = ApiUrl + "/api/file/";
        public static int countOfCart = 0;
        public static string currentLang
        {
            get => LocalizationResourceManager.storedLanguageName;
        }

        // vm
        public static double staticTotalPrice = 0.0;



        public static string UserId
        {
            get => Preferences.Get("userId", "0");
            set => Preferences.Set("userId", value);
        }
        public static string AddressId
        {
            get => Preferences.Get("addressId", "0");
            set => Preferences.Set("addressId", value);
        }

        public static string FullName
        {
            get => Preferences.Get("fullName", "null");
            set => Preferences.Set("fullName", value);
        }

        public static async Task Alert(string massege,int seconds=5)
        {
            var op = new ToastOptions()
            {
                BackgroundColor = Color.Black,
                MessageOptions = new MessageOptions
                {
                    Message = massege,
                },
                CornerRadius = 10,
                Duration = TimeSpan.FromSeconds(seconds),
            };

            await Shell.Current.DisplayToastAsync(op);
            Console.WriteLine("\n\n\n\n massege is: "+massege);
        }

        public static void RefreshCountBasket()
        {

        }

    }
}
