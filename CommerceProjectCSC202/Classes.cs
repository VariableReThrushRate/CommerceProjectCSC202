using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Serialization;


namespace CommerceProjectCSC202
{
    public class Product
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
    public class Manager 
    {
       [JsonIgnore] static int idtobe = 0;
        public int id;
        public string username;
        public string password { get; private set; }
        public static void Setid(int id)
        {
            idtobe = id;
        }
        public static int GetID()
        {
            return idtobe;
        }
        public Manager(string username, string password)
        {
            idtobe++;
            this.id = idtobe;
            this.username = username;
            this.password = Program.sha256_hash(password);
        }
        [JsonConstructorAttribute]
        public Manager(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }
    }
    public class Customer
    {
        [JsonIgnore] static int idtobe = 0;
        public int id;
        public string username;
        public string password { get; private set; }
        public List<Product> cart = new List<Product>();
        public static void Setid(int id)
        {
            idtobe = id;
        }
        public static int GetID()
        {
            return idtobe;
        }
        public Customer(string username, string password)
        {
            idtobe++;
            this.id = idtobe;
            this.username = username;
            this.password = Program.sha256_hash(password);
        }
        [JsonConstructorAttribute]
        public Customer(int id, string username, string password, List<Product> cart)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.cart = cart;
        }
        
    }
    public class PaymentInfo 
    {
        private string numbers;
        public string expr { get; private set; }
        public string cvc { get; private set; }
        public string address { get; private set; }
        [JsonConstructorAttribute]
        public PaymentInfo(string numbers, string expr, string cvc, string address)
        {
            this.numbers = numbers;
            this.expr = expr;
            this.cvc = cvc;
            this.address = address;
        }        

    }
}
