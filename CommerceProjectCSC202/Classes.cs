using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceProjectCSC202
{
    internal class Product
    {
        static int idtobe = 0;
        public int Productid { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public int ProductStockLeft;
        public double ProductPrice;
        public bool instock => ProductStockLeft > 0;
        public Product(string ProductName, string ProductDescrip, int ProductStock, double ProductPrice)
        {
            this.Productid = idtobe++;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            this.ProductStockLeft = ProductStock;
            this.ProductDescription = ProductDescrip;
        }
        public void ReduceStock() 
        {
            int ProductStockLeftTMP = ProductStockLeft - 1;
            if (ProductStockLeftTMP >= 0)
            {
                ProductStockLeft = ProductStockLeftTMP;
            }
            else {
                throw new OutOfStockException();
            }
        }
        public void ReduceStock(int stockreduce)
        {
            int ProductStockLeftTMP = ProductStockLeft - stockreduce;
            if (ProductStockLeftTMP >= 0)
            {
                ProductStockLeft = ProductStockLeftTMP;
            }
            else
            {
                throw new OutOfStockException();
            }
        }

        public override string ToString() 
        {
            return $"Name: {ProductName}\nID is: {Productid}\nDescription: {ProductDescription}\nPrice: {ProductPrice}\n Stock Remaining: {ProductStockLeft}\n";
        }
    }
}
