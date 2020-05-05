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
        private Product _product;
        public NewProductPage()
        {
            InitializeComponent();
            //shopsName.Items.Add("Biedronka");
            //shopsName.Items.Add("Stokrotka");
            //shopsName.Items.Add("Selgros");
        }

        async private void OnAddNewProductButtonClicked(object sender, EventArgs e)
        {
            _product = new Product()
            {
                ShopName = shopsName.SelectedItem.ToString(),
                ProductName = productName.Text,
                ManufacturerName = manufacturerName.Text,
                Price = double.Parse(price.Text),
                Volume = double.Parse(volume.Text),
                PricePerVolume = double.Parse(pricePerVolume.Text),
                PricePerQuantity = double.Parse(pricePerQuantity.Text),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            await App.Database.SaveProductAsync(_product);
            await Navigation.PushAsync(new MainPage());
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

        private void SetButtonAddNewProductVisibility(object sender, TextChangedEventArgs e)
        {
            addNewProductButton.IsVisible = price != null && price.Text != null && price.Text.Length != 0;
        }
    }
}