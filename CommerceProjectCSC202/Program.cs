using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading;

namespace CommerceProjectCSC202
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Variable Initialization area.
            List<Product> products = DataHandler.Load();
            List<Manager> managers = new List<Manager>();
            List<Customer> customers = new List<Customer>();
            // Init the boolean that tells the thread to stop running.
            bool isover = false;
            Thread RandThread = new Thread(() => Randomizer.RandomizerWorker(ref isover, ref products));


            while (true)
            {
                // This interface system is based on my previous CSC 200 project. It's too good not to reuse for a console based commerce system!
                Console.ForegroundColor = ConsoleColor.Green;
                // Wound up experimenting with nested interfaces
                Console.WriteLine("Please select the interface you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Customer Interface");
                Console.WriteLine("2. Manager Interface");
                Console.Write("Insert selection here: ");
                Console.ForegroundColor= ConsoleColor.Blue;
                //Figure out what the user wants
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
                        //What does the user want to go to? Manager? Customer?
                        {
                            case 1:
                                CustomerUI(ref products);
                                break;
                            case 2:
                                ManagerUI(ref products);
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
            isover = true;
            RandThread.Interrupt();
            RandThread.Join();
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
        static void Checkout(ref List<Product> cart, ref List<Product> products) 
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
            foreach (Product tproduct in cart) 
            {
                for (int i = 0; i < products.Count; i++) 
                {
                    if (tproduct.Productid == products[i].Productid) 
                    {
                        products[i].ReduceStock();
                        Console.WriteLine(products[i].ProductName + " stock reduced!");
                    }
                }
            }
            cart = new List<Product>();


        }

        public static void ManagerUI(ref List<Product> products)
        {
            List<Manager> managers = DataHandler.LoadManagers();
            Manager logged = null;
            while (true)
            {
                try
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Would you like to register a new account?");
                    Console.Write("Y/N: ");
                    while (true)
                    {
                        string check = Console.ReadLine();
                        if (check == "Y")
                        {
                            Console.Write("Give a Username: ");
                            string inputuname = Console.ReadLine();
                            Console.Write("Give a Password: ");
                            string inputpass = Console.ReadLine();
                            try
                            {
                                managers.Add(new Manager(inputuname, inputpass));
                                DataHandler.SaveManager(managers);
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("That didn't work. Please try again:");
                                Console.Write(ex.ToString());
                            }
                        }
                        else if (check == "N")
                        { break; }
                    }
                    Console.WriteLine("Please Log in: ");
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = sha256_hash(Console.ReadLine());
                    int ucount = 0;
                    foreach (Manager manager in managers)
                    {
                        if (manager.username == username && manager.password == password)
                        {
                            logged = manager;
                        }
                    }
                    if (logged != null) { break; }
                    Console.WriteLine("Login failed!");


                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Login failed, reason below\n" + ex);
                }
            }
                // Add login to managers and customers
                while (true)
            {
                // This interface system is based on my previous CSC 200 project. It's too good not to reuse for a console based commerce system!
                Console.ForegroundColor = ConsoleColor.Green;


                Console.WriteLine("Please select the method you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Manually add a Customer.");
                Console.WriteLine("2. Remove a Customer.");
                Console.WriteLine("3. Search for products.");
                Console.WriteLine("4. Get more details on a product.");
                Console.WriteLine("5. Register a product.");
                Console.WriteLine("6. Delist a product.");
                Console.WriteLine("7. Add to the stock of a product.");

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
                    if (sel >= 1 && sel <= 7) // Update that value whenever you add a method:
                    {
                        switch (sel)
                        {

                            case 1:
                                //This could have been a method, but, eh.
                                List<Customer> customers = DataHandler.LoadCustomers();
                                while (true) {
                                    Console.Write("Give a Username: ");
                                    string inputuname = Console.ReadLine();
                                    Console.Write("Give a Password: ");
                                    string inputpass = Console.ReadLine();
                                    try
                                    {
                                        customers.Add(new Customer(inputuname, inputpass));
                                        DataHandler.SaveCustomer(customers);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("That didn't work. Please try again:");
                                        Console.Write(ex.ToString());
                                    }
                                }
                                DataHandler.SaveCustomer(customers);
                                break;
                            case 2:
                                List<Customer> customers2 = DataHandler.LoadCustomers();
                                //deletion successful bool
                                bool delsuc = false;
                                
                                while (delsuc) 
                                {
                                    //No way anyone makes that their username, right?
                                    Console.Write("Give a Username (EEEEEEEEE to leave): ");
                                    string inputuname2 = Console.ReadLine();
                                    for (int i = 0; i < customers2.Count; i++)
                                    {
                                        if (customers2[i].username == inputuname2) 
                                        {
                                            customers2.Remove(customers2[i]);
                                        }
                                    }
                                    
                                }
                                DataHandler.SaveCustomer(customers2);
                                break;
                            case 3:
                                Console.Write("Insert Selection now: ");
                                Search(Console.ReadLine(), ref products);
                                break;
                            case 4:
                                Console.Write("Insert an ID Number now: ");
                                string read = Console.ReadLine();
                                GetInfo(int.Parse(read), ref products);
                                break;
                            case 5:
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
                            case 6:
                                Console.Write("Insert an ID Number now: ");
                                string read4 = Console.ReadLine();
                                Remove(int.Parse(read4), ref products);
                                break;
                            case 7:
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
        public static void CustomerUI(ref List<Product> products)
        {
            List<Customer> customers = DataHandler.LoadCustomers();
            Customer logged = null;
            while (true) {
                try
                {
                    
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("Would you like to register a new account?");
                    Console.Write("Y/N: ");
                    while (true) 
                    {
                        string check = Console.ReadLine();
                        if (check == "Y") 
                        {
                            Console.Write("Give a Username: ");
                            string inputuname = Console.ReadLine();
                            Console.Write("Give a Password: ");
                            string inputpass = Console.ReadLine();
                            try
                            {
                                customers.Add(new Customer(inputuname, inputpass));
                                DataHandler.SaveCustomer(customers);
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("That didn't work. Please try again:");
                                Console.Write(ex.ToString());
                            }
                        }
                        else if (check == "N")
                        { break; }
                    }
                    Console.WriteLine("Please Log in: ");
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = sha256_hash(Console.ReadLine());
                    int ucount = 0;
                    foreach (Customer customer in customers) 
                    {
                        if (customer.username == username && customer.password == password) 
                        {
                            logged = customer;
                        }
                    }
                    if (logged != null) { break; }
                    Console.WriteLine("Login failed!");
                    
                    
                }
                catch (Exception ex) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Login failed, reason below\n" + ex);
                }
            }
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
                    if (sel >= 1 && sel <= 6) // Update that value whenever you add a method:
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
                                AddCart(int.Parse(read2), ref products, ref logged.cart);
                                break;
                            case 4:
                                Console.Write("Insert an ID Number now: ");
                                string read3 = Console.ReadLine();
                                RemoveCart(int.Parse(read3), ref logged.cart);
                                break;
                            case 5:
                                PrintCart(ref logged.cart);
                                break;
                            case 6:
                                Checkout(ref logged.cart, ref products);
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
                DataHandler.SaveCustomer(customers);
                DataHandler.Save(products);
            }
            DataHandler.SaveCustomer(customers);
            DataHandler.Save(products);
        }
        //Acquired from Stackoverflow
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
