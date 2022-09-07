using Rawaa.Resources.Languages;
using Rawaa.Services;
using Rawaa.Views;
using System;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rawaa
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            Rawaa.Helper.LocalizationResourceManager.SetLanguage(null, true);
            MainPage = new AppShell();
            Rawaa.Helper.LocalizationResourceManager.checkDirectionWhenStart();
        }

        protected async override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
