using Rawaa.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rawaa.ViewModels
{
    public class SearchPageVM : BaseViewModel
    {
        private string entrySearch = "";

        public string EntrySearch
        {
            get => entrySearch;
            set
            {
                if (entrySearch == value) return;
                entrySearch = value;
                getReseltFromSearch(EntrySearch);
                OnPropertyChanged(nameof(EntrySearch));
            }
        }
        
        private List<Product> StoredList = new List<Product>()
        {
            new Product { Image = "m2.jpg", Calories = 1234, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.50 },
            new Product { Image = "m1.jpg", Calories = 1234, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.99 },
            new Product { Image = "p1.jpg", Calories = 1241, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.50 },
            new Product { Image = "p2.jpg", Calories = 1341, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.99 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "بيتزا مشكل محوج طعمة جميل ولا في احلي من كدا ", SmallSizePrice = 21.50 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "met", SmallSizePrice = 21.50 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "met", SmallSizePrice = 21.50 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "bitza", SmallSizePrice = 21.50 },
            new Product { Image = "p3.jpg", Calories = 1241, Title = "bitza", SmallSizePrice = 21.50 },

        };

        public SearchPageVM()
        {
            ListOfProducts = new ObservableCollection<Product>();
            Task.Run(()=> AddResultsInTheRecentSearchList(StoredList));
        }
        public ObservableCollection<Product> ListOfProducts { get; set; }
        private void getReseltFromSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
                return;
            var ListOfResult = StoredList.Where(s => s.Title.ToLower().Contains(search.ToLower())).OrderBy((x) => x.Title).ToList();
            
            AddResultsInTheRecentSearchList(ListOfResult);
        }

        private async void AddResultsInTheRecentSearchList(List<Product> list)
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

    }
}
