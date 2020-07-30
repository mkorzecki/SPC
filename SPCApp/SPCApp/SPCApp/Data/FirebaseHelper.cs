using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using SPCApp.Models;

namespace SPCApp.Data
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://spcapp-e9fd0.firebaseio.com/");


        public async Task<List<Product>> GetAllProducts()
        {
            return (await firebase
              .Child("Products")
              .OnceAsync<Product>()).Select(item => new Product
              {
                  ID = item.Object.ID,
                  ShopName = item.Object.ShopName,
                  ProductName = item.Object.ProductName,
                  ManufacturerName = item.Object.ManufacturerName,
                  Price = item.Object.Price,
                  Volume = item.Object.Volume,
                  PricePerVolume = item.Object.PricePerVolume,
                  Quantity = item.Object.Quantity,
                  PricePerQuantity = item.Object.PricePerQuantity
              }).ToList();
        }

        public async Task<List<Product>> GetNextProductsIds()
        {
            return (await firebase
              .Child("Products")
              .OnceAsync<Product>()).Select(item => new Product
              {
                  ID = item.Object.ID
              }).ToList();
        }

        private int GetNextProductsId(List<Product> productsIds)
        {
            return productsIds.Count() != 0 ? productsIds.Max(p => p.ID) + 1 : 1;
        }

        public async Task AddProduct(Product product)
        {
            var productsIds = await GetNextProductsIds();
            product.ID = GetNextProductsId(productsIds);

            var allProducts = await GetAllProducts();
            var theSameProducts = allProducts.Where(a => a.ShopName == product.ShopName && a.ManufacturerName == product.ManufacturerName && a.Price == product.Price).ToList();
            if (theSameProducts.Count() > 0)
                return;

            await firebase
              .Child("Products")
              .PostAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            var toUpdatePerson = (await firebase
              .Child("Products")
              .OnceAsync<Product>()).Where(a => a.Object.ID == product.ID).FirstOrDefault();

            await firebase
              .Child("Products")
              .Child(toUpdatePerson.Key)
              .PutAsync(product);
        }

        public async Task<Product> GetProduct(int personId)
        {
            var allProducts = await GetAllProducts();
            await firebase
              .Child("Products")
              .OnceAsync<Product>();
            return allProducts.Where(a => a.ID == personId).FirstOrDefault();
        }

        public async Task DeleteProduct(int productId)
        {
            var toDeleteProduct = (await firebase
              .Child("Products")
              .OnceAsync<Product>()).Where(a => a.Object.ID == productId).FirstOrDefault();
            await firebase.Child("Products").Child(toDeleteProduct.Key).DeleteAsync();
        }

        public async Task CreateProductsBackUp()
        {
            var products = await GetAllProducts();
            var JSONProducts = Newtonsoft.Json.JsonConvert.SerializeObject(products);
            ProductBackUp productsBackUp = new ProductBackUp()
            {
                JSONObject = JSONProducts,
                CreatedDate = DateTime.Now
            };

            var productsBackUpIds = await GetNextProductsBackUpsIds();
            productsBackUp.ID = GetNextProductsBackUpId(productsBackUpIds);

            await firebase
              .Child("ProductsBackUp")
              .PostAsync(productsBackUp);
        }

        private int GetNextProductsBackUpId(List<ProductBackUp> productsBackUpIds)
        {
            return productsBackUpIds.Count() != 0 ? productsBackUpIds.Max(p => p.ID) + 1 : 1;
        }

        public async Task<List<ProductBackUp>> GetNextProductsBackUpsIds()
        {
            return (await firebase
              .Child("ProductsBackUp")
              .OnceAsync<ProductBackUp>()).Select(item => new ProductBackUp
              {
                  ID = item.Object.ID
              }).ToList();
        }

        public async Task<List<ProductBackUp>> GetAllProductsBackUp()
        {
            return (await firebase
              .Child("ProductsBackUp")
              .OnceAsync<ProductBackUp>()).Select(item => new ProductBackUp
              {
                  ID = item.Object.ID,
                  JSONObject = item.Object.JSONObject,
                  CreatedDate = item.Object.CreatedDate
              }).ToList();
        }
        public async Task DeleteProductBackUp(int productId)
        {
            var toDeleteProductBackUp = (await firebase
              .Child("ProductsBackUp")
              .OnceAsync<ProductBackUp>()).Where(a => a.Object.ID == productId).FirstOrDefault();
            await firebase.Child("ProductsBackUp").Child(toDeleteProductBackUp.Key).DeleteAsync();
        }

    }
}
