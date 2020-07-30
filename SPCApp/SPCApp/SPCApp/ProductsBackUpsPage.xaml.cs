using SPCApp.Data;
using SPCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsBackUpsPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ProductsBackUpsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var productsBackUps = await firebaseHelper.GetAllProductsBackUp();
            productsBackUpsListView.ItemsSource = productsBackUps.OrderBy(p => p.CreatedDate).ToList();
        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ProductsImportPage(e.SelectedItem as ProductBackUp));
            }
        }
    }
}