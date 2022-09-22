using Rawaa.Helper;
using Rawaa.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rawaa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            init();
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    return true;
        //}
        string lngCelected = null;
        string culture = LocalizationResourceManager.storedLanguageName;

        private void init()
        {
            //if (culture == "ar")
            //{
            //    box_ar.IsEnabled = false;
            //    box_en.IsEnabled = true;
            //    lngCelected = "ar";
            //}
            //else
            //{
            //    box_ar.IsEnabled = true;
            //    box_en.IsEnabled = false;
            //    lngCelected = "en";
            //}
        }
        private void Button_Clicked_Arabic(object sender, EventArgs e)
        {
            lngCelected = "ar";
            frameAr.BackgroundColor = Color.FromHex("#F6B21B");
            frameEn.BackgroundColor = Color.White;
        }

        private void Button_Clicked_English(object sender, EventArgs e)
        {
            lngCelected = "en";
            frameEn.BackgroundColor = Color.FromHex("#F6B21B");
            frameAr.BackgroundColor = Color.White;

        }

        private async void Button_Clicked_Ok(object sender, EventArgs e)
        {
            if (lngCelected == "ar")
                await LocalizationResourceManager.SetLanguageAsync("ar");
            else
                await LocalizationResourceManager.SetLanguageAsync("en");

            if(lngCelected == culture)
            {
               await this.DisplayToastAsync(LanguageResources.LanguagePage_TheSameLange);
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}