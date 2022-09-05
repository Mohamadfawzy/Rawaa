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

        public static string storedLanguageName
        {
            get => Preferences.Get(nameof(storedLanguageName), languageCurrentDevice);
            set => Preferences.Set(nameof(storedLanguageName), value);
        }
        public static string SetLanguage(string langName = null, bool isMainPage = false)
        {
            if (langName == null)
                langName = storedLanguageName;

            CultureInfo language = new CultureInfo(langName);
            Thread.CurrentThread.CurrentUICulture = language;
            LanguageResources.Culture = language;

            storedLanguageName = language.ToString();

            //if (isMainPage)
            //{
            //    var ds = DependencyService.Get<IMyEnvironment>();
            //    if (storedLanguageName == "ar")
            //    {
            //        ds.FlowDirectionRTL();
            //        App.Current.MainPage.FlowDirection = FlowDirection.RightToLeft;
            //    }
            //    else
            //    {
            //        ds.FlowDirectionLTR();
            //        App.Current.MainPage.FlowDirection = FlowDirection.LeftToRight;
            //    }
            //}
                
            return storedLanguageName;
        }
        public static async Task<bool> SetLanguageAsync(string langName = null)
        {
            var currentCulture = SetLanguage(langName);
            var t1 = ChangeDirection(currentCulture);
            return await Task.FromResult(t1);
        }
        public static bool ChangeDirection(string name, bool startApp = false)
        {
            var ds = DependencyService.Get<IMyEnvironment>();
            if (name == "ar")
            {
                ds.FlowDirectionRTL();
                App.Current.MainPage = new AppShell();
                App.Current.MainPage.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                ds.FlowDirectionLTR();
                App.Current.MainPage = new AppShell();
                App.Current.MainPage.FlowDirection = FlowDirection.LeftToRight;
            }
            //CurrentLanguage = name;
            return true;
        }

        public static void checkDirectionWhenStart()
        {
            var ds = DependencyService.Get<IMyEnvironment>();
            if (storedLanguageName == "ar")
            {
                ds.FlowDirectionRTL();
                App.Current.MainPage.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                ds.FlowDirectionLTR();
                App.Current.MainPage.FlowDirection = FlowDirection.LeftToRight;
            }

        }
        static void old()
        {



        }
    }
}
