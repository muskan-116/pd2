using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bussinessProject1
{
    class Program
    {

    
        class Credentials
        {
        public string username;
        public string passwords;
        public string roles;
        }
        class Hotel
        {
            public string productName;
            public int productPrice;
        }
        class Customer
        {
            public string selectFood;
            public int foodQuantity;
        }
    
        static void Main(string[] args)
        {
            List<Credentials> person = new List<Credentials> ();

            List<Hotel> food = new List<Hotel>();
            List<Customer> order = new List<Customer>();
            string path = "G:\\PD2\\bussinessProject1\\bussinessProject1\\login.txt";
            string path1 = "G:\\PD2\\bussinessProject1\\bussinessProject1\\hotel.txt";
            string path2  = "G:\\PD2\\bussinessProject1\\bussinessProject1\\customer.txt";
            int option;
            readData(path, person);
            do
            {
                option = menu();

                if (option == 1)
                {
                    Console.Clear();
                    Console.Write("enter name:");
                    string name = Console.ReadLine();
                    Console.Write("enter password:");
                    string lockpass = Console.ReadLine();
                    Console.Write("enter your role:");
                    string character = Console.ReadLine();
                    signup(path, name, lockpass, character);
                    cls();


                }
                else if (option == 2)
                {
                    Console.Clear();
                    Console.Write("enter name:");
                    string name = Console.ReadLine();
                    Console.Write("enter password:");
                    string lockpass = Console.ReadLine();
                    string total = signin(path, name, lockpass, person);
                    cls();
                    readFood(path1, food);
                    Console.Clear();
                    if (total == "admin")
                    {
                        int ad;
                        ad = adminMenu();
                        if (ad == 1)
                        {
                            Console.Clear();
                            viewFood(path1, food);
                            Console.Clear();
                            ad = adminMenu();

                        }
                        else if (ad == 2)
                        {
                            Console.Clear();
                            Console.Write("enter food name:");
                            string foodName = Console.ReadLine();
                            Console.Write("enter food price :");
                            int price = int.Parse(Console.ReadLine());
                            addFood(path1, foodName, price);
                            Console.Clear();
                            ad = adminMenu();

                        }
                        else if (ad == 3)

                        {
                            Console.Clear();
                            updateFood(food);
                            Console.Clear();
                            ad = adminMenu();
                        }
                        else if (ad == 4)
                        {
                            Console.Clear();
                            deleteFood(food);
                            Console.Clear();
                            ad = adminMenu();
                        }
                        else if(ad ==5)
                        {
                            Console.Clear();
                            ad = menu();

                        }
                    }
                    
                    else if (total == "customer")
                    {
                        
                        Console.Clear();
                        
                        int history = customerMenu();
                        
                        if (history == 1)
                        {
                            viewFood(path1, food);
                        }
                        else if (history == 2)
                        {
                            Console.Write("how many food items u want to select :");
                            int foodSelect = int.Parse(Console.ReadLine());
                            for (int x = 0; x < foodSelect; x++)
                            {

                                Console.Write("enter food name :");
                                string selectName = Console.ReadLine();
                                Console.Write("enter food quantity:");
                                int quantity = int.Parse(Console.ReadLine());

                                addOrder(path2, selectName, quantity);
                              
                            }

                            Console.Clear();

                        }
                        else if (history == 3)
                        {
                            viewOrder(path2, order);
                        }
                        else if (history == 4)
                        {

                        }
                        else if(history ==5 )
                        {
                            Console.Clear();
                            history = menu();
                        }

                    }
                }
            }
                 while (option < 3);
                     {
                            Console.Read();
                     }
        }
        static int menu ()
        {
            int choice;
            Console.WriteLine("1.Press 1 for sign up");
            Console.WriteLine("2.Press 2 for sign  in");
            Console.WriteLine("3.Press 3 for exit");
            Console.WriteLine("Enter your choice");
            choice = int.Parse(Console.ReadLine());
            return choice;
            Console.Clear();
        }
        static void  cls()
        {
            Console.WriteLine("press any key to continue.....");
            Console.ReadKey();
        }
       static void signup(string path,string name,string lockpass,string character)
        {
            StreamWriter file = new StreamWriter(path, true);
            file.WriteLine(name + "," + lockpass + "," + character);
            file.Flush();
            file.Close();
          
        }
        static string  signin(string path,string name,string lockpass,List<Credentials> person)
        {
            bool flag = false;
            string r = "";
            string rool = null;
            for(int x=0;x<person.Count;x++)
            {
                if (name == person[x].username && lockpass ==person[x].passwords  )
                {
                    rool= person[x].roles;
                    r = rool;
                    Console.WriteLine("sign in successfully.......");
                    flag = true;
                    break;
                    cls();
                   

                }
                else if(flag ==false)
                {
                    Console.ReadKey();
                }
               
            }
            return r;
            if (flag ==false)
            {
                Console.WriteLine("invalid user!!!!");
            }
            Console.ReadKey();
        }
        static void readData(string path, List<Credentials> person)
        {
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                string record;
                while ((record = file.ReadLine()) != null)
                {
                    Credentials info = new Credentials();
                    info.username = getParse(record, 1);
                    info.passwords = getParse(record, 2);
                    info.roles = getParse(record, 3);
                    person.Add(info);
                }
                file.Close();
            }

            else
            {
                Console.WriteLine("no exists!!!");
            }
        }  
        static string  getParse(string record ,int field)
        {
            int comma = 1;
            string line = "";
            for(int x=0;x<record.Length;x++)
            {
                if(record[x]==',')
                {
                    comma++;
                }
                else if(comma == field)
                {
                    line = line + record[x];
                }
            }
            return line;
        }
        static int adminMenu()
        {
            int choice;
            Console.WriteLine("_______________________");
            Console.WriteLine("______ ADMIN MENU _____");
            Console.WriteLine("_______________________");
            Console.WriteLine("1.view food items");
            Console.WriteLine("2.add food item");
            Console.WriteLine("3.update food");
            Console.WriteLine("4.delete food");
            Console.WriteLine("5.exit");

            Console.WriteLine("enter your choice:");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
        static void viewFood(string path1,List<Hotel> food)
        {
            for(int x = 0;x<food.Count;x++)
            {
                Console.WriteLine("foodname : {0} , foodprice: {1}", food[x].productName, food[x].productPrice);
            }
            cls();
        }
        static void addFood(string path1,string foodName,int price)
        {
            StreamWriter file = new StreamWriter(path1,true);
            file.WriteLine(foodName + "," + price);
            file.Flush();
            file.Close();
        }
        static void readFood(string path1, List<Hotel> food)
        {
            if (File.Exists(path1))
            {
                StreamReader file = new StreamReader(path1);
                string record;
                while ((record = file.ReadLine()) != null)
                {
                    Hotel info = new Hotel();
                    info.productName = getParse(record, 1);
                    info.productPrice = int.Parse(getParse(record, 2));
                    
                    food.Add(info);
                }
                file.Close();
            }

            else
            {
                Console.WriteLine("no exists!!!");
            }
        }
        static void updateFood(List<Hotel> food)
        {
            Console.WriteLine("enter food name u want to update?");
            string updateFood = Console.ReadLine();
            for(int x=0;x<food.Count;x++)
            {
                if(updateFood == food[x].productName)

                {
                    Console.WriteLine("enter price:");
                    int newPrice = int.Parse(Console.ReadLine());
                    food[x].productPrice = newPrice;
                    break;

                }

            }

        }
        static void deleteFood(List<Hotel> food)
        {
            Console.WriteLine("enter the food name you want to delete ");
            string deleteFood = Console.ReadLine();
            for (int x = 0;x<food.Count;x++)
            {
                if(deleteFood == food[x].productName)
                {
                    food[x].productName = food[x + 1].productName;
                    food[x].productPrice = food[x + 1].productPrice;
                }

            }
        }
        static int  customerMenu()
        {
            Console.Clear();
            int choice;
            Console.WriteLine("---> ############### <----");
            Console.WriteLine("---> CUSTOMER MENU <----");
            Console.WriteLine("--->############### <----");
            Console.WriteLine("1.view food menu ");
            Console.WriteLine("2.write your  order");
            Console.WriteLine("3. view your order");
            Console.WriteLine("4.Display bill amount");
            Console.WriteLine("5.EXIT");
            Console.Write("enter option :");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
        static void addOrder(string path2,string selectName,int quantity)
        {
            StreamWriter file = new StreamWriter(path2, true);
            file.WriteLine(selectName + "," + quantity);
            file.Flush();
            file.Close();

        }
        static void readOrder(string path2, List<Customer> order)
        {
            if (File.Exists(path2))
            {
                StreamReader file1 = new StreamReader(path2);
                string line;
                while ((line = file1.ReadLine()) != null)
                {
                    Customer temp = new Customer();
                    temp.selectFood = getParse(line, 1);
                    temp.foodQuantity = int.Parse(getParse(line,2));
                   
                    order.Add(temp);
                }
                file1.Close();
            }

            else
            {
                Console.WriteLine("no exists!!!");
            }
        } 


        static void  viewOrder(string path2, List<Customer> order)
        {
              
                for (int x = 0; x < order.Count; x++)
                {
                    Console.WriteLine("foodname : {0} , quantity: {1} ", order[x].selectFood, order[x].foodQuantity);
                }
                cls();
        }
        
        

    }
}
