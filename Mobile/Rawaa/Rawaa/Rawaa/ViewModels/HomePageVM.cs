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
using Rawaa_Api.Models;

namespace Rawaa.ViewModels
{
    public class HomePageVM : BaseViewModel
    {
        public int SliderPosition { get; set; }


        public IDataStore<AdsM> requestProvider = new AdData();
        public List<AdsM> Sliders { get; set; }
        public List<Category> FoodMenu { get; set; }
        public List<Product> MostPopularMeals { get; set; }



        public ICommand langCommand => new Command<string>(changLang);
        public ICommand CurrentItemChangedCommand => new Command<AdsM>(CurrentItemChangedExcute);

        public HomePageVM()
        {
            Task.Run(()=>FetchAds());
            Task.Run(() => FetchCategory());
            Task.Run(() => FetchMostPopularMeals());
            Task.Run(() => FetchOffers());
        }

        // excuted
        void CurrentItemChangedExcute(AdsM item)
        {
            Console.WriteLine("====================="+ item.ImageUrl);
        }
        async void changLang(string lng)
        {
            await LocalizationResourceManager.SetLanguageAsync(lng);
        }

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



        // fetch
        private async Task FetchAds()
        {
            var lis = new List<AdsM>() { new AdsM { Image = "p3.jpg" }, new AdsM { Image = "m4.jpg" }, new AdsM { Image = "p2.jpg" } };
            Sliders = lis;
        }


        private async Task FetchCategory()
        {
            var lis = new List<Category>() 
            {
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 
                new Category { Image = "m3.jpg", Title= "الصنف" }, new Category { Image = "m2.jpg" , Title= "الصنف"}, new Category { Image = "m1.jpg" , Title= "الصنف"}, 

            };
            FoodMenu = lis;
        }  
        private async Task FetchMostPopularMeals()
        {
            var lis = new List<Product>()
            {
                new Product { Image = "m2.jpg", Calories= 1234, Title= "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا " , SmallSizePrice = 21.50 },
                new Product { Image = "m1.jpg", Calories= 1234,Title= "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا " , SmallSizePrice = 21.99  },
                new Product { Image = "p1.jpg", Calories= 1241,Title= "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا " , SmallSizePrice = 21.50  },
                new Product { Image = "p2.jpg", Calories= 1341,Title= "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا " , SmallSizePrice = 21.99  },
                new Product { Image = "p3.jpg", Calories= 1241,Title= "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا " , SmallSizePrice = 21.50  },

            };
            MostPopularMeals = lis;
        } 

        private async Task FetchOffers()
        {

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
