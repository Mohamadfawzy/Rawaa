using Rawaa.Services;
using Rawaa_Api.Models;
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
    public partial class RegisterPage : ContentPage
    {
        RequestProvider<User> requestProvider;
        public RegisterPage()
        {
            InitializeComponent();
            //requestProvider = new RequestProvider<User>();
        }

        private void Close_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..", false);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            requestProvider = new RequestProvider<User>();
            var uri = "api/client/user";
            User user = new User
            {
                FullName = fullName.Text,
                Email = email.Text,
                Phone = phone.Text,
                Password = password.Text
                
            };
            var res = await requestProvider.PostOneAsync<User>(user, uri);

            if (res == null)
            {
                await AppSettings.Alert("please check you email and password");
                return;
            }

            AppSettings.UserId = res.Id.ToString();
            AppSettings.FullName = res.FullName;

            await Shell.Current .GoToAsync("../..", false);
            await AppSettings.Alert(res.FullName);
        }

        private void GoToLoginPage_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("LoginPage", false);
        }
    }
}