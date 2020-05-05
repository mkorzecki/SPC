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
    public partial class EditProductPage : ContentPage
    {
        private Product _product;
        public EditProductPage(Product product)
        {
            InitializeComponent();
            //shopsName.Items.Add("Biedronka");
            //shopsName.Items.Add("Stokrotka");
            //shopsName.Items.Add("Selgros");

            _product = product;
            shopsName.SelectedItem = _product.ShopName;
            productName.Text = _product.ProductName;
            manufacturerName.Text = _product.ManufacturerName;
            price.Text = _product.Price.ToString();
            volume.Text = _product.Volume.ToString();
            pricePerVolume.Text = _product.PricePerVolume.ToString();
            quantity.Text = _product.Quantity.ToString();
            pricePerQuantity.Text = _product.PricePerQuantity.ToString();
        }

        private async void OnEditProductButtonClicked(object sender, EventArgs e)
        {
            _product.ShopName = shopsName.SelectedItem.ToString();
            _product.ProductName = productName.Text;
            _product.ManufacturerName = manufacturerName.Text;
            _product.Price = double.Parse(price.Text);
            _product.Volume = double.Parse(volume.Text);
            _product.PricePerVolume = double.Parse(pricePerVolume.Text);
            _product.PricePerQuantity = double.Parse(pricePerQuantity.Text);
            _product.ModifiedDate = DateTime.UtcNow;
            await App.Database.SaveProductAsync(_product);
            await Navigation.PushAsync(new ProductsPage());
        }

        private async void OnDeleteProductButtonClicked(object sender, EventArgs e)
        {
            await App.Database.DeleteProductAsync(_product);
            await Navigation.PushAsync(new ProductsPage());
        }

        private void OnPriceChange(object sender, TextChangedEventArgs e)
        {
            SetPricePerVolume(sender, e);
            SetPricePerQuantity(sender, e);
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
                    var priceValue = double.Parse(price.Text);
                    var volumeValue = double.Parse(volume.Text);
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
                    var priceValue = double.Parse(price.Text);
                    var quantityValue = double.Parse(quantity.Text);
                    pricePerQuantity.Text = Math.Round(priceValue / quantityValue, 2).ToString();
                }
            }
            else
                pricePerQuantity.Text = null;
        }
    }
}