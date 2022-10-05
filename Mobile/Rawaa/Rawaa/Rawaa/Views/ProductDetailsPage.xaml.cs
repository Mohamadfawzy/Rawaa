using Rawaa.Fonts;
using Rawaa.Models;
using Rawaa.ViewModels;
using System;
using System.Collections;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsPage : ContentPage
    {
        ProductDetailsPageVM vm;

        PancakeView previousFrame = new PancakeView();
        Label previousLable = new Label();

        PancakeView previousFrameTeste = new PancakeView();
        Label previousLableTeste = new Label();
        Label previousLableCheckbox = new Label();

        PancakeView previousFrameDrink = new PancakeView();

        // ctor
        public ProductDetailsPage()
        {
            InitializeComponent();

            vm = (BindingContext as ProductDetailsPageVM);

            previousFrame = smallFrame;
            previousLable = smallLable;

            previousFrameTeste = frameTesteNormal;
            previousLableTeste = labelTesteNormal;
            previousLableCheckbox = labelTesteCheckbox;

            HandleUiFromCart();
        }


        // size section
        private void Size_Tapped(object sender, EventArgs e)
        {
            var frame = (PancakeView)sender;
            HandleSizeMela(frame);
        }

        // teste section
        private void Teste_Tapped(object sender, EventArgs e)
        {
            var frame = (PancakeView)sender;
            HandleTesteFrame(frame);
        }

        // drink section
        private void Drink_Tapped(object sender, EventArgs e)
        {
            var frame = (PancakeView)sender;
            HandleDrinkItems(frame);

        }

        //
        private void HandleSizeMela(PancakeView frame)
        {
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
                    vm.SelectedSizePrice = (double)vm.Meal.MediumSizePrice;
                    break;
                case "larg":
                    size = 3;
                    vm.SelectedSizePrice = (double)vm.Meal.BigSizePrice;
                    break;
            }
            previousFrame = frame;
            previousLable = frame.Content as Label;
            vm.CalculatePrice();
            vm.CartOption.Size = size;
        }

        //
        private void HandleTesteFrame(PancakeView frame)
        {
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
        }

        // 
        private void HandleDrinkItems(PancakeView frame)
        {

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
        //
        private void HandleUiFromCart()
        {
            var vm = ProductDetailsPageVM.StaticCart;
            var frame = new PancakeView();
            if (vm == null) return;
            switch (vm.Size)
            {
                case 1:
                    frame = smallFrame;
                    break;
                case 2:
                    frame = mediumFrame;
                    break;
                case 3:
                    frame = largFrame;
                    break;
            }
            HandleSizeMela(frame);

            switch (vm.Taste)
            {
                case 1:
                    frame = frameTesteNormal;
                    break;
                case 2:
                    frame = frameTesteHot;
                    break;
            }
            HandleTesteFrame(frame);

            test();
        }

        // 
        private async void test()
        {
            var list = flexDrinks.Children.ToList();
            foreach (var item in list)
            {
                var stack = (StackLayout)item;
                var frame = (PancakeView)stack.Children[0];
                var gester = (TapGestureRecognizer)frame.GestureRecognizers[0];
                var name = gester.CommandParameter.ToString();
                if (name == "mohamed5")
                {
                    previousFrameDrink.Opacity = 0.7;
                    previousFrameDrink.Border = new Border() { Color = Color.Gray, Thickness = 1 };
                    previousFrameDrink.Scale = 1;


                    frame.Opacity = 1;
                    frame.Border = new Border() { Color = Color.FromHex("#F6B21B"), Thickness = 4 };
                    frame.Scale = 1.08;
                    previousFrameDrink = frame;

                }
            }



            //await AppSettings.Alert(name.ToString());
        }

    }
}