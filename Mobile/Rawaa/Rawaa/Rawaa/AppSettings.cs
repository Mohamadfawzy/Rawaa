using Rawaa_Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Rawaa
{
    public static class AppSettings
    {
        //http://www.rawaa.somee.com/
        public const string ApiUrl = "http://192.168.1.101:5117";
        public const string ImageUrl = ApiUrl + "/api/file/";
        public static int countOfCart = 0;
        public static string UserId
        {
            get => Preferences.Get("userId", "0");
            set => Preferences.Set("userId", value);
        }

        public static string FullName
        {
            get => Preferences.Get("fullName", "null");
            set => Preferences.Set("fullName", value);
        }


    }
}
