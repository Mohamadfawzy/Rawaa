using Rawaa.Models;
using Rawaa.Services;
using Rawaa.ViewModels;
using Rawaa_Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rawaa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateDeliveryAddressPage : ContentPage
    {
        RequestProvider<User> requestProvider;
        public CreateDeliveryAddressPage()
        {
            InitializeComponent();
            requestProvider = new RequestProvider<User>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            Shell.SetTabBarIsVisible(this, false);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var uri = "api/client/deliveryaddress";

            var address = new DeliveryAddress()
            {
                Governorate = governorate.Text,
                City = city.Text,
                Street = street.Text,
                BuildingNumber = Convert.ToInt32(buildingNumber.Text),
                FloorrUmber = Convert.ToByte(turnNumber.Text),
                ApartmentNumber = Convert.ToByte(apartmentNumber.Text),
                Notes = notes.Text,
                ShortName = shortName.Text,
                CustomerId = Convert.ToInt32(AppSettings.UserId)
            };
            
            var res = await requestProvider.PostOneAsync<DeliveryAddress>(address, uri);
            if (res == null)
            {
                await AppSettings.Alert("please check you email and password");
                return;
            }
        }
    }
}