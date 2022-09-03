using Newtonsoft.Json;
using Rawaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text.Json.Serialization;
using RestSharp;
using Rawaa.Services;
using Rawaa.Helper;

namespace Rawaa.ViewModels
{
    public class HomePageVM : BaseViewModel
    {
        public IDataStore<AdsM> requestProvider = new AdData();

        public List<AdsM> Sliders { get; set; }
        public int SliderPosition { get; set; }

        public ICommand langCommand => new Command<string>(changLang);

        public HomePageVM()
        {
            ss2();
        }

        async void changLang(string lng)
        {
            await LocalizationResourceManager.SetLanguageAsync(lng);
        }
        HttpClient client = new HttpClient
        {
            //BaseAddress = new Uri(BaseUrl),
            Timeout = new TimeSpan(0, 0, 10)
        };


        // tip return task for exeptions
        private async Task ss2()
        {
            var result = await requestProvider.GetItemsAsync(true);
            var cl1 = new AdsM() { Image = result.FirstOrDefault().Image };
            var cl2 = new AdsM() { Image = result.FirstOrDefault().Image };
            var cl3 = result.FirstOrDefault();
            Sliders = new List<AdsM>() { cl1, cl2, cl3 };
            //SetSliderPosition(2);
            OnPropertyChanged("Sliders");
        }





        private void SetSliderPosition(int count)
        {
            try
            {
                if (count > 1)
                {
                    Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                    {
                        SliderPosition = (SliderPosition + 1) % count;
                        OnPropertyChanged(nameof(SliderPosition));
                        return true;
                    });
                }
            }
            catch (Exception) { }
        }

        // handel slider
        public void HandelSlides()
        {
            Task.Run(() =>
            {
                Sliders.Clear();
                var list = new List<AdsM>();
                //list = AllServices.GetSlidersOffers();
                if (list == null)
                {
                    // DO somthing
                }
                else
                {
                    foreach (var item in list)
                    {
                        if (Sliders.Count > 9)
                        {
                            break;
                        }
                        Sliders.Add(item);
                    }

                    Sliders = list;
                    if (Sliders.Count > 1)
                    {
                        //IndicatorViewVisible = true;
                        OnPropertyChanged("IndicatorViewVisible");
                    }
                    OnPropertyChanged(nameof(Sliders));
                    //AIVisible = false;
                    //SliderItemCount = Sliders.Count;
                }

            });

        }


    } // end class
}


/*
private readonly IDataStore<AdsM> requestProvider;
requestProvider = DependencyService.Get<IDataStore<AdsM>>();
 */
