using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceProjectCSC202
{
    internal class Product
    {
        public string ProductName { get; private set; }
        public int ProductStockLeft;
        public double ProductPrice;
        public bool instock => ProductStockLeft > 0;
        public Product(string ProductName, int ProductStock, double ProductPrice) 
        {

        }
    }
}
