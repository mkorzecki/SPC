using System.Collections.Generic;
using System.Threading.Tasks;
using SPCApp.Models;
using SQLite;

namespace SPCApp.Data
{
    public class ProductsLocalDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ProductsLocalDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>().Wait();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<Product> GetProductAsync(int id)
        {
            return _database.Table<Product>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveProductAsync(Product Product)
        {
            if (Product.ID != 0)
            {
                return _database.UpdateAsync(Product);
            }
            else
            {
                return _database.InsertAsync(Product);
            }
        }

        public Task<int> DeleteProductAsync(Product Product)
        {
            return _database.DeleteAsync(Product);
        }
    }
}
