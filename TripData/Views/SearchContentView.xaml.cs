using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TripData.Views
{
    public partial class SearchContentView : ListView
    {
        public SearchContentView()
        {
            InitializeComponent();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

           this.SelectedItem = null;
        }
    }
}
