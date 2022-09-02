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

namespace Rawaa.ViewModels
{
    public class HomePageVM : BaseViewModel
    {
        public IDataStore<AdsM> requestProvider =  new AdData();

        //private readonly IDataStore<AdsM> requestProvider;

        RequestProvider<AdsM> aa22;


        public List<AdsM> Sliders { get; set; }
        public int SliderPosition { get; set; }
        public byte[] MyImage { get; set; }

        public ICommand langCommand => new Command<string>((string s) => { s = "sssss"; });

        public HomePageVM()
        {
            //requestProvider = DependencyService.Get<IDataStore<AdsM>>();
            
            //GetListAsync();
            ss2();


        }

        HttpClient client = new HttpClient
        {
            //BaseAddress = new Uri(BaseUrl),
            Timeout = new TimeSpan(0, 0, 10)
        };

        public async void as3()
        {
            var result = aa22.GetListAsync().Result.ToList();

            var cl1 = new AdsM() { Image = result.FirstOrDefault().Image };
            var cl2 = new AdsM() { Image = result.FirstOrDefault().Image };
            var cl3 = result.FirstOrDefault();
            Sliders = new List<AdsM>() { cl1, cl2, cl3 };
            SetSliderPosition(2);
            OnPropertyChanged("Sliders");

        }
        public async void ss()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            HttpClient myClint = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri("https://192.168.1.101:7128"),
                Timeout = new TimeSpan(0, 0, 10)
            };

            var json = await myClint.GetStringAsync("api/FileUploads/bytImage?imageName=1");
            var result = JsonConvert.DeserializeObject<byte[]>(json);

            var cl1 = new AdsM() { Image = result };
            var cl2 = new AdsM() { Image = result };
            Sliders = new List<AdsM>() { cl1, cl2 };
            SetSliderPosition(2);
            OnPropertyChanged("Sliders");

        }

        private async void ss2()
        {
            var result = await requestProvider.GetItemsAsync(true);
            var cl1 = new AdsM() { Image = result.FirstOrDefault().Image };
            var cl2 = new AdsM() { Image = result.FirstOrDefault().Image };
            var cl3 = result.FirstOrDefault();
            Sliders = new List<AdsM>() { cl1, cl2, cl3 };
            SetSliderPosition(2);
            OnPropertyChanged("Sliders");
        }

        public async Task<byte[]> GetListAsync()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            byte[] result;
            try
            {
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://192.168.1.101:7128/api/FileUploads/bytImage?imageName=1"))
                    {
                        var response = await httpClient.SendAsync(request);
                        var ima = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<byte[]>(ima) ;
                    }
                }
                
                var cl1 = new AdsM() {Image = result };
                var cl2 = new AdsM() {Image = result };
                Sliders = new List<AdsM>() { cl1, cl2 };
                SetSliderPosition(2);
                OnPropertyChanged("Sliders");
                return result;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return null;// await Task.FromResult(default(TResult));
        }

        public async Task<byte[]> GetListAsync1()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            byte[] result;
            try
            {
                var json = await client.GetAsync("https://192.168.1.101:7128/api/FileUploads/bytImage?imageName=1");
                var content = await json.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<byte[]>(content);

                var cl1 = new AdsM() { Image = result };
                var cl2 = new AdsM() { Image = result };
                Sliders = new List<AdsM>() { cl1, cl2 };
                SetSliderPosition(2);
                OnPropertyChanged("Sliders");
                return result;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return null;// await Task.FromResult(default(TResult));
        }

        private void SetSliderPosition(int count)
        {
            try
            {
                if (count > 1)
                {
                    Device.StartTimer(TimeSpan.FromSeconds(5),() =>
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
