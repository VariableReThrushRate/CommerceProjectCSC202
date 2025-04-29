using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommerceProjectCSC202
{
    internal class Product
    {
        static int idtobe = 0;
        [JsonIgnore] public int Productid { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public int ProductStockLeft { get; private set; } = 1000;
        public double ProductPrice;
        [JsonIgnore]  public bool  instock => ProductStockLeft > 0 ;
        public Product()
        {
            
        }

        public Product(string ProductName, string ProductDescrip, int stock, double ProductPrice)
        {
            idtobe++;
            this.Productid = idtobe;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            this.ProductDescription = ProductDescrip;
            this.ProductStockLeft = stock;
        }
        public void AddStock(int stock)
        {
            ProductStockLeft = ProductStockLeft + Math.Abs(stock);
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
        public  void SetStock(int stockcount) 
        {
            this.ProductStockLeft = stockcount;
        }
        public static void Setid(int id)
        {
            idtobe = id;
        }

        public override string ToString() 
        {
            return $"Name: {ProductName}\nID is: {Productid}\nDescription: {ProductDescription}\nPrice: {ProductPrice}\n Stock Remaining: {ProductStockLeft}\n";
        }
        
    }
}
