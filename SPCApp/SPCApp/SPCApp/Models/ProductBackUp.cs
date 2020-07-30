using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCApp.Models
{
    public class ProductBackUp
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string JSONObject { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
