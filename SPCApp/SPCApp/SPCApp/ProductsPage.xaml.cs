using SPCApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data.MySqlClient;
using System.Data;

namespace SPCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public string _typingWord;

        public ProductsPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var products =  App.Database.GetProducts();       
            foreach (var product in products)
            {
                _products.Add(product);
            }
            productsListView.ItemsSource = _products.OrderByDescending(p => p.ProductName).ToList();
        }

        private async void OnCreateNewProductButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewProductPage
            {
                BindingContext = new Product()
            });
        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EditProductPage(e.SelectedItem as Product));
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _typingWord = MainSearchBar.Text.ToLower();
            productsListView.ItemsSource = _products.Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(_typingWord)
                                                            || p.ShopName != null && p.ShopName.ToLower().Contains(_typingWord));
        }
        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                switch (selectedIndex)
                {
                    case 0:
                        productsListView.ItemsSource = _products.Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(_typingWord) || p.ShopName != null && p.ShopName.ToLower().Contains(_typingWord))
                            .OrderByDescending(p => p.PricePerVolume).ToList();
                        break;
                    case 1:
                        productsListView.ItemsSource = _products.Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(_typingWord) || p.ShopName != null && p.ShopName.ToLower().Contains(_typingWord))
                            .OrderBy(p => p.PricePerVolume).ToList();
                        break;
                    case 2:
                        productsListView.ItemsSource = _products.Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(_typingWord) || p.ShopName != null && p.ShopName.ToLower().Contains(_typingWord))
                            .OrderBy(p => p.ProductName).ToList();
                        break;
                    case 3:
                        productsListView.ItemsSource = _products.Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(_typingWord) || p.ShopName != null && p.ShopName.ToLower().Contains(_typingWord))
                            .OrderByDescending(p => p.ProductName).ToList();
                        break;
                    case 4:
                        productsListView.ItemsSource = _products.Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(_typingWord) || p.ShopName != null && p.ShopName.ToLower().Contains(_typingWord))
                            .OrderBy(p => p.ShopName).ToList();
                        break;
                    case 5:
                        productsListView.ItemsSource = _products.Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(_typingWord) || p.ShopName != null && p.ShopName.ToLower().Contains(_typingWord))
                            .OrderByDescending(p => p.ShopName).ToList();
                        break;
                    default:
                        productsListView.ItemsSource = _products.OrderByDescending(p => p.CreatedDate).ToList();
                    break;
                }
            }
        }
    }
}