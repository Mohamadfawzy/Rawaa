using Rawaa.Services;
using Rawaa_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Exceptions;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rawaa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        RequestProvider<User> requestProvider;
        public LoginPage()
        {
            InitializeComponent();
            requestProvider = new RequestProvider<User>();
        }

        private void Close_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..", false);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var uri = "api/client/user/login";
            User user = new User
            {
                Password = password.Text,
                Email = email.Text
            };
            var res = await requestProvider.PostOneAsync<User>(user, uri);
            
            if(res == null)
            {
                await this.DisplayToastAsync("please check you email and password");
                return;
            }
            var op = new ToastOptions()
            {
                BackgroundColor = Color.Black,
                MessageOptions = new MessageOptions
                {
                    Message = res.FullName,
                },
                CornerRadius = 10,
                Duration = TimeSpan.FromSeconds(3),
            };

            AppSettings.UserId = res.Id.ToString();
            AppSettings.FullName = res.FullName;

            await this.DisplayToastAsync(op);
            var u = new User();
            Console.WriteLine("mooooooooooooooooooooooooooooooooooo"+ u.Id);
        }


        private void GoToRegisterPage_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("RegisterPage", false);
        }
    }
}