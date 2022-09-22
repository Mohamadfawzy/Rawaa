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

        private void Button_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("LoginPage",false);
        }

        private void Language_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("SettingPage", false);

        }
    }
}