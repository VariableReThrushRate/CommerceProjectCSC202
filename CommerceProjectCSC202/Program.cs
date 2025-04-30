using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace CommerceProjectCSC202
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Variable Initialization area.
            List<Product> products = DataHandler.Load();
            List<Product> cart = new List<Product>();


            while (true)
            {
                // This interface system is based on my previous CSC 200 project. It's too good not to reuse for a console based commerce system!
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Please select the interface you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Customer Interface");
                Console.WriteLine("2. Manager Interface");

                Console.Write("Insert selection here: ");

                Console.ForegroundColor= ConsoleColor.Blue;

                string brug = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                try
                {
                    //exit line
                    if (brug == "EEE")
                    {
                        break;
                    }
                    int sel = Convert.ToInt32(brug);
                    if (sel >= 1 && sel <= 2) // Update that value whenever you add a method:
                    {
                        switch (sel)
                        {
                            case 1:
                                CustomerUI(ref products, ref cart);
                                break;
                            case 2:
                                ManagerUI(ref products, ref cart);
                                break;
                            default:
                                Console.WriteLine("How did you get here???");
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidSelectionException();
                    }
                }
                catch (NotImplementedException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That function is not implimented:" + exception.ToString());
                    
                }
                catch (FormatException exception)
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("That was not converted right! Please try again.");
                    Console.WriteLine(exception.ToString());
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThat did not work. Please try again.\n");
                    Console.Write(exception.ToString());
                }
                DataHandler.Save(products);
            }
            DataHandler.Save(products);
        }
        static void Search(string search, ref List<Product> products)
        {
            int found = 0;
            foreach (Product product in products) 
            {
                if (product.ProductName.ToLower().Contains(search.ToLower())) 
                {
                    
                    Console.WriteLine("Name is: " + product.ProductName);
                    Console.WriteLine("ID is: " + product.Productid.ToString());
                    found++;
                }
                
            }
            if (found == 0) 
            {
                Console.WriteLine("No Products found!");
            }
        }
        static void GetInfo(int searchID, ref List<Product> products)
        {
            foreach (Product product in products)
            {
                if (product.Productid == searchID)
                {

                    Console.WriteLine(product);
                }

            }
            Console.WriteLine("No Products found!");
            
        }
        static void AddCart(int searchID, ref List<Product> products, ref List<Product> cart) 
        {

            foreach (Product product in products)
            {
                if (product.Productid == searchID)
                {
                    cart.Add(product);
                    Console.WriteLine("Added to cart!");
                    return;
                }

            }
            Console.WriteLine("Item not found!");
        }
        static void RemoveCart(int searchID, ref List<Product> cart)
        {
            Product product = null;
            foreach (Product tproduct in cart) 
            {
                if (tproduct.Productid == searchID) 
                {
                    product = tproduct;
                    break;
                }
            }
            if (product == null)
            {
                Console.WriteLine("Item not found!");
            }
            else 
            {
                cart.Remove(product);
            }
            
        }
        static void PrintCart(ref List<Product> cart) 
        {
           Console.WriteLine("Printing cart!");
            foreach (Product product in cart) 
            {
                Console.WriteLine(product);
            }
        }
        static void Remove(int searchID, ref List<Product> products)
        {
            Product product = null;
            foreach (Product tproduct in products)
            {
                if (tproduct.Productid == searchID)
                {
                    product = tproduct;
                    break;
                }
            }
            if (product == null)
            {
                Console.WriteLine("Item not found!");
            }
            else
            {
                products.Remove(product);
            }

        }
        static void UpdateStock(int searchID, ref List<Product> products, int stockamount)
        {
            foreach (Product tproduct in products)
            {
                if (tproduct.Productid == searchID)
                {
                    tproduct.AddStock(stockamount);
                    Console.WriteLine("Stock updated! It is now:" + tproduct.ProductStockLeft.ToString());
                    return;
                }
            }
            
            Console.WriteLine("Item not found!");
            
        }
        static void Checkout(ref List<Product> cart) 
        {
            double price = 0;
            foreach (Product tproduct in cart) 
            {
                price += tproduct.ProductPrice;
            }
            Console.WriteLine("Price is: $" + price.ToString());
            Console.WriteLine("DO NOT USE REAL DETAILS FOR THE FOLLOWING! IT IS MERELY AN EXERCISE. IT WILL NOT WRITE THIS INFO TO DISK, ONLY MEMORY.");
            Console.Write("Insert your credit card number: ");
            string number = Console.ReadLine();
            Console.Write("Insert your credit card number expiration date: ");
            string expiration = Console.ReadLine();
            Console.Write("Insert your credit card Security code: ");
            string cvc = Console.ReadLine();
            Console.Write("Insert your Street Address: ");
            string address = Console.ReadLine();
            // Thanks to https://gist.github.com/arundvp/188d92fefda9bb7546ee52a9ecf7aad6 for this regex. I would have no idea how to do this otherwise 💀
            if (!Regex.IsMatch(number, @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|6(?:011|5[0-9]{2})[0-9]{12}|(?:2131|1800|35\d{3})\d{11})$")) 
            {
                Console.WriteLine("Credit card did not validate!");
                return;
            }
            Console.WriteLine("Payment successful!");
            cart = new List<Product>();


        }

        public static void ManagerUI(ref List<Product> products, ref List<Product> cart)
        {
            // Add login to managers and customers
            while (true)
            {
                // This interface system is based on my previous CSC 200 project. It's too good not to reuse for a console based commerce system!
                Console.ForegroundColor = ConsoleColor.Green;


                Console.WriteLine("Please select the method you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Manually add a Customer.");
                Console.WriteLine("2. Add a manager.");
                Console.WriteLine("3. Remove a manager.");
                Console.WriteLine("4. Search for products.");
                Console.WriteLine("5. Get more details on a product.");
                Console.WriteLine("6. Register a product.");
                Console.WriteLine("7. Delist a product.");
                Console.WriteLine("8. Add to the stock of a product.");

                Console.Write("Insert selection here: ");

                Console.ForegroundColor = ConsoleColor.Blue;

                string brug = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                try
                {
                    //exit line
                    if (brug == "EEE")
                    {
                        break;
                    }
                    int sel = Convert.ToInt32(brug);
                    if (sel >= 1 && sel <= 8) // Update that value whenever you add a method:
                    {
                        switch (sel)
                        {
                            
                            case 1:
                                throw new NotImplementedException();
                                break;
                            case 2:
                                Console.Write("Insert an ID Number now: ");
                                string read2 = Console.ReadLine();
                                AddCart(int.Parse(read2), ref products, ref cart);
                                break;
                            case 3:
                                throw new NotImplementedException();

                                break;
                            case 4:
                                Console.Write("Insert Selection now: ");
                                Search(Console.ReadLine(), ref products);
                                break;
                            case 5:
                                Console.Write("Insert an ID Number now: ");
                                string read = Console.ReadLine();
                                GetInfo(int.Parse(read), ref products);
                                break;
                            case 6:
                                Console.WriteLine("Adding new product!");
                                Console.Write("Insert a name for the product: ");
                                string name = Console.ReadLine();
                                Console.Write("Insert a Description for the product: ");
                                string description = Console.ReadLine();
                                Console.Write("Insert a price for the product: $");
                                double price = double.Parse(Console.ReadLine());
                                Console.Write("Insert an initial stock for the product: ");
                                int stock = int.Parse(Console.ReadLine());
                                Product tproduct = new Product(name, description, stock, price);
                                Console.WriteLine("Adding new product!");
                                products.Add(tproduct);
                                break;
                            case 7:
                                Console.Write("Insert an ID Number now: ");
                                string read4 = Console.ReadLine();
                                Remove(int.Parse(read4), ref products);
                                break;
                            case 8:
                                Console.Write("Insert an ID Number now: ");
                                string read5 = Console.ReadLine();
                                Console.Write("Insert how much stock to add: ");
                                string read6 = Console.ReadLine();
                                UpdateStock(int.Parse(read5), ref products, int.Parse(read6));
                                break;

                            default:
                                Console.WriteLine("How did you get here???");
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidSelectionException();
                    }
                }
                catch (NotImplementedException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That function is not implimented:" + exception.ToString());

                }
                catch (FormatException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That was not converted right! Please try again.");
                    Console.WriteLine(exception.ToString());
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThat did not work. Please try again.\n");
                    Console.Write(exception.ToString());
                }
                DataHandler.Save(products);
            }
            DataHandler.Save(products);
        }
        public static void CustomerUI(ref List<Product> products, ref List<Product> cart)
        {
            while (true)
            {
                // This interface system is based on my previous CSC 200 project. It's too good not to reuse for a console based commerce system!
                Console.ForegroundColor = ConsoleColor.Green;


                Console.WriteLine("Please select the method you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Search for a product by name.");
                Console.WriteLine("2. Get more details on a product by ID.");
                Console.WriteLine("3. Add a Product to your cart.");
                Console.WriteLine("4. Remove a Product from Cart.");
                Console.WriteLine("5. Display your Cart.");
                Console.WriteLine("6. Checkout your cart.");
                Console.WriteLine("7. Make an account.");
                Console.WriteLine("8. Login.");

                Console.Write("Insert selection here: ");

                Console.ForegroundColor = ConsoleColor.Blue;

                string brug = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                try
                {
                    //exit line
                    if (brug == "EEE")
                    {
                        break;
                    }
                    int sel = Convert.ToInt32(brug);
                    if (sel >= 1 && sel <= 8) // Update that value whenever you add a method:
                    {
                        switch (sel)
                        {
                            case 1:
                                Console.Write("Insert Selection now: ");
                                Search(Console.ReadLine(), ref products);
                                break;
                            case 2:
                                Console.Write("Insert an ID Number now: ");
                                string read = Console.ReadLine();
                                GetInfo(int.Parse(read), ref products);
                                break;
                            case 3:
                                Console.Write("Insert an ID Number now: ");
                                string read2 = Console.ReadLine();
                                AddCart(int.Parse(read2), ref products, ref cart);
                                break;
                            case 4:
                                Console.Write("Insert an ID Number now: ");
                                string read3 = Console.ReadLine();
                                RemoveCart(int.Parse(read3), ref cart);
                                break;
                            case 5:
                                PrintCart(ref cart);
                                break;
                            case 6:
                                Checkout(ref cart);
                                break;
                            case 7:
                                throw new NotImplementedException();
                                break;
                            case 8:
                                throw new NotImplementedException();
                                break;
                            default:
                                Console.WriteLine("How did you get here???");
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidSelectionException();
                    }
                }
                catch (NotImplementedException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That function is not implimented:" + exception.ToString());

                }
                catch (FormatException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That was not converted right! Please try again.");
                    Console.WriteLine(exception.ToString());
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThat did not work. Please try again.\n");
                    Console.Write(exception.ToString());
                }
                DataHandler.Save(products);
            }
            DataHandler.Save(products);
        }
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

    }

}
