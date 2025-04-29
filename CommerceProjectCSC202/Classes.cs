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
        [JsonIgnore]static int idtobe = 0;
        public int Productid { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public int ProductStockLeft { get; private set; } = 1000;
        public double ProductPrice;
        [JsonIgnore]  public bool  instock => ProductStockLeft > 0 ;

        [JsonConstructorAttribute]
        public Product(int Productid, string ProductName, string ProductDescription, int ProductStockLeft, double ProductPrice) 
        {
            this.Productid = Productid;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            this.ProductDescription = ProductDescription;
            this.ProductStockLeft = ProductStockLeft;
        }
        public Product(string ProductName, string ProductDescrip, int stock, double ProductPrice)
        {
            idtobe++;
            this.Productid = idtobe;
            this.ProductName = ProductName;
            this.ProductPrice = Math.Abs(ProductPrice);
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
        public static int GetID() 
        {
            return idtobe;
        }

        public override string ToString() 
        {
            return $"Name: {ProductName}\nID is: {Productid}\nDescription: {ProductDescription}\nPrice: ${ProductPrice}\n Stock Remaining: {ProductStockLeft}\n";
        }
        
    }
}
