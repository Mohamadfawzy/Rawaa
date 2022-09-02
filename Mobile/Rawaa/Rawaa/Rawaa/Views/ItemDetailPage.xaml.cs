using Rawaa.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Rawaa.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}