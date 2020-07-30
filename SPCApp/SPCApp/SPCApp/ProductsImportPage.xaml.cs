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
    public partial class ProductsImportPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        private ProductBackUp _productBackUp;
        private List<Product> _products;

        public ProductsImportPage(ProductBackUp productBackUp)
        {
            InitializeComponent();
            _productBackUp = productBackUp;
            _products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(productBackUp.JSONObject);
            productsBackUpListView.ItemsSource = _products.ToList();
        }

        private async void OnImportProductsBackUpButtonClicked(object sender, EventArgs e)
        {
            foreach (var product in _products)
            {
                await firebaseHelper.AddProduct(product);
            }
            await DisplayAlert("Success", "Products Imported Successfully", "OK");
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnDeleteProductBackUpButtonClicked(object sender, EventArgs e)
        {
            await firebaseHelper.DeleteProductBackUp(_productBackUp.ID);
            await DisplayAlert("Success", "Products BackUp Removed Successfully", "OK");
            await Navigation.PushAsync(new ProductsBackUpsPage());
        }
    }
}