using SPCApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SPCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProductPage : ContentPage
    {
        public NewProductPage()
        {
            InitializeComponent();
        }

        async private void OnAddNewProductButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            product.CreatedDate = DateTime.UtcNow;
            await App.Database.SaveProductAsync(product);
            await Navigation.PushAsync(new ProductsPage());
        }

        private void OnPriceChange(object sender, TextChangedEventArgs e)
        {
            SetPricePerVolume(sender, e);
            SetPricePerQuantity(sender, e);
            SetButtonAddNewProductVisibility(sender, e);
        }

        private void OnVolumeChange(object sender, TextChangedEventArgs e)
        {
            SetPricePerVolume(sender, e);
        }

        private void OnQuantityChange(object sender, TextChangedEventArgs e)
        {
            SetPricePerQuantity(sender, e);
        }

        private void SetPricePerVolume(object sender, TextChangedEventArgs e)
        {
            if (price != null && price.Text != null && price.Text.Length != 0
                && volume != null && volume.Text != null && volume.Text.Length != 0)
            {
                if (volume.Text == null || volume.Text == "0")
                    pricePerVolume.Text = null;
                else 
                {
                    var priceValue = double.Parse((price.Text).Replace(',', '.'));
                    var volumeValue = double.Parse((volume.Text).Replace(',', '.'));
                    pricePerVolume.Text = Math.Round(priceValue * 100 / volumeValue, 2).ToString();
                }
            }
            else
                pricePerVolume.Text = null;
        }

        private void SetPricePerQuantity(object sender, TextChangedEventArgs e)
        {
            if (price != null && price.Text != null && price.Text.Length != 0
                && quantity != null && quantity.Text != null && quantity.Text.Length != 0)
            {
                if (quantity.Text == null || quantity.Text == "0" || quantity.Text == "1")
                    pricePerQuantity.Text = price.Text;
                else
                {
                    var priceValue = double.Parse((price.Text).Replace(',', '.'));
                    var quantityValue = double.Parse((quantity.Text).Replace(',', '.'));
                    pricePerQuantity.Text = Math.Round(priceValue / quantityValue, 2).ToString();
                }
            }
            else
                pricePerQuantity.Text = null;
        }

        private void SetButtonAddNewProductVisibility(object sender, TextChangedEventArgs e)
        {
            addNewProductButton.IsVisible = price != null && price.Text != null && price.Text.Length != 0;
        }
    }
}