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
        List<Product> products = new List<Product>();

        public ProductsPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Product> _products = await App.Database.GetProductsAsync();
            foreach (var product in _products)
            {
                products.Add(product);
            }
            productsListView.ItemsSource = products.OrderByDescending(p => p.CreatedDate).ToList();
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
            var typingWord = MainSearchBar.Text.ToLower();
            productsListView.ItemsSource = products.Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(typingWord)
                                                            || p.ShopName != null && p.ShopName.ToLower().Contains(typingWord));
        }

        private void sortByNameDesc(object sender, EventArgs e)
        {
            productsListView.ItemsSource = products.OrderByDescending(p => p.ProductName);
        }

        private void sortByNameAsc(object sender, EventArgs e)
        {
            productsListView.ItemsSource = products.OrderBy(p => p.ProductName);
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
                        productsListView.ItemsSource = products.OrderBy(p => p.PricePerVolume).ToList();
                        break;
                    case 1:
                        productsListView.ItemsSource = products.OrderByDescending(p => p.PricePerVolume).ToList();
                        break;
                    case 2:
                        productsListView.ItemsSource = products.OrderBy(p => p.ProductName).ToList();
                        break;
                    case 3:
                        productsListView.ItemsSource = products.OrderByDescending(p => p.ProductName).ToList();
                        break;
                    case 4:
                        productsListView.ItemsSource = products.OrderBy(p => p.ShopName).ToList();
                        break;
                    case 5:
                        productsListView.ItemsSource = products.OrderByDescending(p => p.ShopName).ToList();
                        break;
                    default:
                        productsListView.ItemsSource = products.OrderByDescending(p => p.CreatedDate).ToList();
                    break;
                }
            }
        }
    }
}