namespace CommerceProjectCSC202
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            //Variable Initialization area.
            List<Product> products = new List<Product>();
            List<Product> cart = new List<Product>();


            while (true)
            {
                // This interface system is based on my previous CSC 200 project. It's too good not to reuse for a console based commerce system!
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Please select the method you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Search for a product by name.");
                Console.WriteLine("2. Get more details on a product by ID.");
                Console.WriteLine("3. Add a Product to your cart.");
                Console.WriteLine("4. Remove a Product from Cart.");
                Console.WriteLine("5. Check out your cart.");
                Console.WriteLine("6. Register a product.");
                Console.WriteLine("7. Delist a product.");

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
                    if (sel >= 1 && sel <= 7) // Update that value whenever you add a method:
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
                                Console.WriteLine("Insert an ID Number now: ");
                                string read2 = Console.ReadLine();
                                AddCart(int.Parse(read2), ref products, ref cart);
                                break;
                            case 4:
                                Console.WriteLine("Insert an ID Number now: ");
                                string read3 = Console.ReadLine();
                                RemoveCart(int.Parse(read3), ref cart);
                                break;
                            case 5:
                                PrintCart(ref cart);
                                break;
                            case 6:
                                throw new NotImplementedException();
                                break;
                            case 7:
                                Console.WriteLine("Insert an ID Number now: ");
                                string read4 = Console.ReadLine();
                                Remove(int.Parse(read4), ref products);
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
            }
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
    }
    
}
