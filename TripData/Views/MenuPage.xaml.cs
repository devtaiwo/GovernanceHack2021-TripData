using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TripData.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        private async void OnBackPage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Chimoney());
        }
    }
}
