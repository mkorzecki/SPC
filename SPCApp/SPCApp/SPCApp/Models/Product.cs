using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCApp.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ShopName { get; set; }
        public string ProductName { get; set; }
        public string ManufacturerName { get; set; }
        public double Price { get; set; }
        public double Volume { get; set; }
        public double PricePerVolume { get; set; }
        public int Quantity { get; set; }
        public double PricePerQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
