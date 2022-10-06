using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rawaa.ViewModels
{
    public class SearchPageVM : BaseViewModel
    {
        private RequestProvider<Product> requestProvider = new RequestProvider<Product>();
        public ObservableCollection<Product> ListOfProducts { get; set; }

        private string entrySearch = "";
        public string EntrySearch
        {
            get => entrySearch;
            set
            {
                if (entrySearch == value) return;
                entrySearch = value;
                Fetch(EntrySearch);
                OnPropertyChanged(nameof(EntrySearch));
            }
        }

        public ICommand RemainingItemsThresholdReachedCommand => new Command(Incrementally);

        // ctor
        public SearchPageVM()
        {
            ListOfProducts = new ObservableCollection<Product>();
        }
        int page = 0;
        private async void Incrementally()
        {
            IsBusy = true;

            page++;

            IsBusy = false;

        }



        private async Task Fetch(string text)
        {
            IsBusy = true;
            var url = $"{AppSettings.currentLang}/api/cp/Product/search/{text}";
            var list = await requestProvider.GetListAsync(url);
            RefreshList(list);
            IsBusy = false;
        }

        private void RefreshList(List<Product> list)
        {
            try
            {
                //await Task.Delay(3000);
                ListOfProducts.Clear();

                foreach (var item in list)
                {
                    ListOfProducts.Add(item);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message) ;
            }
        }




        private void getReseltFromSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
                return;
            //var ListOfResult = StoredList.Where(s => s.Title.ToLower().Contains(search.ToLower())).OrderBy((x) => x.Title).ToList();

            //AddResultsInTheRecentSearchList(ListOfResult);
        }

    }
}
