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
        public static int ProductStockLeft { get; private set; } = 1000;
        public double ProductPrice;
        public bool instock => ProductStockLeft > 0;
        public Product(string ProductName, string ProductDescrip, double ProductPrice)
        {
            this.Productid = idtobe++;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
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
        public static void SetStock(int stockcount) 
        {
            ProductStockLeft = stockcount;
        }

        public override string ToString() 
        {
            return $"Name: {ProductName}\nID is: {Productid}\nDescription: {ProductDescription}\nPrice: {ProductPrice}\n Stock Remaining: {ProductStockLeft}\n";
        }
    }
}
