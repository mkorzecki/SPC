using MySql.Data.MySqlClient;
using SPCApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SPCApp.Data
{
    public class ProductsDatabase
    {
        readonly MySqlConnection _connection;

        public ProductsDatabase(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                var cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT ID, ShopName, ProductName, ManufacturerName, Price, Volume, PricePerVolume, Quantity, PricePerQuantity FROM Products";

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ID = reader.GetInt32("ID"),
                        ShopName = reader.GetString("ShopName"),
                        ProductName = reader.GetString("ProductName"),
                        ManufacturerName = reader.GetString("ManufacturerName"),
                        Price = reader.GetDouble("Price"),
                        Volume = reader.GetDouble("Volume"),
                        PricePerVolume = reader.GetDouble("PricePerVolume"),
                        Quantity = reader.GetInt32("Quantity"),
                        PricePerQuantity = reader.GetDouble("PricePerQuantity"),
                    });

                }
                Console.WriteLine(products);
            }
            catch (MySqlException ex)
            {

                throw;
            }
            finally
            {
                _connection.Close();
            }
            return products;
        }

        internal void SaveProductAsync(Product product)
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                var cmd = _connection.CreateCommand();
                if (product.ID != 0)
                {
                    UpdateProduct(product, cmd);
                }
                else
                {
                    InsertProduct(product, cmd);
                }
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

                throw;
            }
            finally
            {
                _connection.Close();
            }

        }

        private void InsertProduct(Product product, MySqlCommand cmd)
        {
            cmd.CommandText =
     "INSERT INTO Products (ShopName, ProductName, ManufacturerName, Price, Volume, PricePerVolume, Quantity, PricePerQuantity, CreatedDate, ModifiedDate) " +
     "VALUES(@ShopName, @ProductName, @ManufacturerName, @Price, @Volume, @PricePerVolume, @Quantity, @PricePerQuantity, @CreatedDate, @ModifiedDate)";
            cmd.Parameters.AddWithValue("@ShopName", product.ShopName);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@ManufacturerName", product.ManufacturerName);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Volume", product.Volume);
            cmd.Parameters.AddWithValue("@PricePerVolume", product.PricePerVolume);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@PricePerQuantity", product.PricePerQuantity);
            cmd.Parameters.AddWithValue("@CreatedDate", product.CreatedDate);
            cmd.Parameters.AddWithValue("@ModifiedDate", product.ModifiedDate);
        }

        private void UpdateProduct(Product product, MySqlCommand cmd)
        {
            cmd.CommandText =
    "UPDATE Products SET ShopName = @ShopName, ProductName = @ProductName, ManufacturerName = @ManufacturerName, Price = @Price, Volume = @Volume," +
    "PricePerVolume = @PricePerVolume, Quantity = @Quantity, PricePerQuantity=@PricePerQuantity, ModifiedDate = @ModifiedDate WHERE ID = @ID";
            cmd.Parameters.AddWithValue("@ID", product.ID);
            cmd.Parameters.AddWithValue("@ShopName", product.ShopName);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@ManufacturerName", product.ManufacturerName);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Volume", product.Volume);
            cmd.Parameters.AddWithValue("@PricePerVolume", product.PricePerVolume);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@PricePerQuantity", product.PricePerQuantity);
            cmd.Parameters.AddWithValue("@ModifiedDate", product.ModifiedDate);
        }

        internal void DeleteProductAsync(int id)
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                var cmd = _connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Products WHERE Products.ID = @ID";
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
