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
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
            
        }
        private void GoToLoginPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("LoginPage",false);
        }

        private void GoToLanguage_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("SettingPage", false);
        }

        private void GoToOrdersPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("OrdersPage", false);
        }

        private void GoToRegisterPage(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("RegisterPage", false);
        }



        private void Button_Clicked(object sender, EventArgs e)
        {

            AppSettings.UserId = "0";
            AppSettings.FullName = "null";
        }

        void Refresh()
        {

        }
    }
}