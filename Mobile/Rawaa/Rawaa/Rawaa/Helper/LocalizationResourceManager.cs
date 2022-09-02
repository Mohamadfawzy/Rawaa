using Rawaa.Resources.Languages;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Rawaa.Helper
{
    public static class LocalizationResourceManager
    {
        private static string languageCurrentDevice = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        public static string CurrentLanguage
        {
            get => Preferences.Get(nameof(CurrentLanguage), languageCurrentDevice);
            set => Preferences.Set(nameof(CurrentLanguage), value);
        }
        public static string SetLanguage(string langName = null, bool isMainPage = false)
        {
            if (langName == null)
                langName = CurrentLanguage;
            CultureInfo language = new CultureInfo(langName);
            Thread.CurrentThread.CurrentUICulture = language;
            LanguageResources.Culture = language;
            CurrentLanguage = language.ToString();
            //if(!isMainPage)
            //    App.Current.MainPage = new MainPage();
            return language.ToString();
        }
        public static async Task<bool> SetLanguageAsync(string langName = null)
        {
            var currentCulture = SetLanguage(langName);
            var t1 = ChangeDirection(currentCulture);
            return await Task.FromResult(t1);
        }
        public static bool ChangeDirection(string name)
        {
           // var ds = DependencyService.Get<IMultipleDependencies>();
            if (name == "ar")
            {
               // ds.FlowDirectionRTL();
                //App.Current.MainPage = new MainPage();
                App.Current.MainPage.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
               // ds.FlowDirectionLTR();
                //App.Current.MainPage = new MainPage();
                App.Current.MainPage.FlowDirection = FlowDirection.LeftToRight;
            }
            //CurrentLanguage = name;
            return true;
        }


        static void old()
        {
            
        }
    }
}
