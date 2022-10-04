using Rawaa.Fonts;
using Rawaa.Models;
using Rawaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace Rawaa.Views
{
    //[QueryProperty(nameof(SelectedProduct), "meal")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsPage : ContentPage
    {
        ProductDetailsPageVM vm;
        public Product SelectedProduct
        {
            set
            {
                Load(value);
                //LoadAnimal(value);
            }
        }
        public ProductDetailsPage()
        {
            InitializeComponent();
            vm = (BindingContext as ProductDetailsPageVM);
            previousFrame = smallFrame;
            previousLable = smallLable;

            previousFrameTeste = frameTesteNormal;
            previousLableTeste = labelTesteNormal;
            previousLableCheckbox = labelTesteCheckbox;
        }

        void Load(Product product)
        {
            (BindingContext as ProductDetailsPageVM).Meal = product;
        }

        // size section
        PancakeView previousFrame = new PancakeView();
        Label previousLable = new Label();
        private void Size_Tapped(object sender, EventArgs e)
        {
            var frame = (PancakeView)sender;
            var gester = (TapGestureRecognizer)frame.GestureRecognizers.FirstOrDefault();
            var sizeName = gester.CommandParameter.ToString();
            var label = frame.Content as Label;

            previousFrame.Style = (Style)Resources["deselectedFrame"];
            previousLable.Style = (Style)Resources["deselectedLabel"];

            frame.Style = (Style)Resources["selectedFrame"];
            label.Style = (Style)Resources["selectedLabel"];

            byte size = 1;
            switch (sizeName)
            {
                case "small":
                    vm.SelectedSizePrice = vm.Meal.SmallSizePrice;
                    break;
                case "medium":
                    size = 2;
                    vm.SelectedSizePrice = vm.Meal.MediumSizePrice;
                    break;
                case "larg":
                    size = 2;
                    vm.SelectedSizePrice = vm.Meal.BigSizePrice;
                    break;
            }
            previousFrame = frame;
            previousLable = frame.Content as Label;
            //AppSettings.Alert(name);
            vm.CalculatePrice();
            vm.CartOption.Size = size;
        }

        // Teste section
        PancakeView previousFrameTeste = new PancakeView();
        Label previousLableTeste = new Label();
        Label previousLableCheckbox = new Label();
        private void Teste_Tapped(object sender, EventArgs e)
        {
            var frame = (PancakeView)sender;
            var gester = (TapGestureRecognizer)frame.GestureRecognizers.FirstOrDefault();
            var name = gester.CommandParameter.ToString();
            var stack = (StackLayout)frame.Content;
            var label1 = (Label)stack.Children[0];
            var label2 = (Label)stack.Children[1];

            previousFrameTeste.Style = (Style)Resources["deselectedFrame"];
            previousLableTeste.Style = (Style)Resources["deselectedLabel"];
            previousLableCheckbox.Text = IconFont.CheckboxBlankCircleOutline;

            frame.Style = (Style)Resources["selectedFrame"];
            label1.Style = (Style)Resources["selectedLabel"];
            label2.Text = IconFont.CheckboxMarkedCircle;

            previousFrameTeste = frame;
            previousLableTeste = label1;
            previousLableCheckbox = label2;

            vm.CartOption.Taste = Convert.ToByte(name);
            //AppSettings.Alert(name);
        }

        // drink section
        PancakeView previousFrameDrink = new PancakeView();
        private void Drink_Tapped(object sender, EventArgs e)
        {
            var frame = (PancakeView)sender;
            var gester = (TapGestureRecognizer)frame.GestureRecognizers.FirstOrDefault();
            var name = gester.CommandParameter;

            previousFrameDrink.Opacity = 0.7;
            previousFrameDrink.Border = new Border() { Color = Color.Gray, Thickness = 1 };
            previousFrameDrink.Scale = 1;


            frame.Opacity = 1;
            frame.Border = new Border() { Color = Color.FromHex("#F6B21B"), Thickness = 4 };
            frame.Scale = 1.08;
            previousFrameDrink = frame;
        }

        // quantity section
        int quantity = 0;
        int _price = 0;
        private void Plus_Tapped(object sender, EventArgs e)
        {
            var puls = sender as Label;
            int.TryParse(labelQuantity.Text, out quantity);
            quantity++;
            labelQuantity.Text = quantity.ToString();
            int.TryParse(price.Text, out _price);
            price.Text = (_price += _price).ToString();
        }

        private void Minus_Tapped(object sender, EventArgs e)
        {
            int.TryParse(labelQuantity.Text, out quantity);
            if (quantity < 1)
                return;
            quantity--;
            labelQuantity.Text = quantity.ToString();
            int.TryParse(price.Text, out _price);
            price.Text = (_price -= _price).ToString();
        }
    }
}