﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rawaa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("..", false);
            return true;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(() => SetFocus());
        }

        async Task SetFocus()
        {
            await Task.Delay(2070);
            entrySearch.Focus();
        }
    }
}