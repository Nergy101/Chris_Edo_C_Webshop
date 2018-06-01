using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Data.SqlClient;
namespace WinkelServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WinkelService" in both code and config file together.
    public class WinkelService : IWinkelService
    {

        //List<User> userRegister = new List<User>() { new User("admin", "admin", 100.0, new List<Purchase>()) };

        //List<Item> itemRegister = new List<Item>() { new Item("appel", 1.50, 10), new Item("peer", 2.00, 5), new Item("doeken", 8.00, 20)};

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void Register(string username, string password)
        {
            try
            {
                //check if already exists
                using (WebShopContainer ctx = new WebShopContainer())
                {
                    User user = new User(username, password);
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    Console.WriteLine("did something something dark side");
                }


                //foreach (User u in userRegister)
                //{
                //    if (username.Equals(u.Username) || password.Equals(u.Password))
                //    {
                //        Console.WriteLine("This username or password (combination) is already in use");
                //        throw new Exception("This username or password (combination) is already in use");
                //    }
                //}
                ////create new user
                //List<Purchase> newPurchaseList = new List<Purchase>();

                //User newUser = new User(username, password, 10.00, newPurchaseList);
                //userRegister.Add(newUser); // list maken werkt niet? pakt alleen eerste user
                //Console.WriteLine($"user registered: {username}, {password}");
                //var result = string.Join(",", userRegister);

                //Console.WriteLine(result);
            }
            catch (Exception e)
            {
                // Something went wrong.
                Console.WriteLine(e);
            }

        }

        public void BuyItem(Purchase p) //ook buyer meegeven
        {
            WebShopContainer ctx = new WebShopContainer();
            
                Console.WriteLine(p.User.Username);
                Console.WriteLine(p.UserUsername);
                p.User.Purchases.Add(p);
                ctx.SaveChanges();
                //User user = new User();



            
            //Console.WriteLine("user: " + p.User.ToString());
            //p.User.Purchases.Add(p);
            //Console.WriteLine(p.User.Purchases.ToString());

            //Console.WriteLine($"voor-- saldo: {p.User.Saldo} en item InStore: {p.Item.InStore}");
            //p.Buyer.Saldo = p.User.Saldo - (pItem.Price * p.Amount);// naar BuyItem verplaatsen
            //p.Item.InStore = p.Item.InStore - p.Amount;
            //Console.WriteLine($"na-- saldo: {p.Buyer.Saldo} en item InStore: {p.Item.InStore}");

            //Console.WriteLine($"{p.Item}, amount bought: {p.Amount}, Saldo of {p.Buyer.Username} is now: {p.Buyer.Saldo}");
        }

        public ICollection<Purchase> getUserInventory(User u)
        {
            return u.Purchases;
        }

        public List<Item> GetItems()
        {
            List<Item> itemRegister = new List<Item>();
            Console.WriteLine(itemRegister.ToString());
            Console.WriteLine("items added to list");

            using (WebShopContainer ctx = new WebShopContainer())
            {
                //User user = new User();
                var items = from i in ctx.Items
                           select i;

                foreach (Item i in items)
                {
                    Item item = new Item();
                    item.Name = i.Name;
                    item.Price = i.Price;
                    item.Stock = i.Stock;
                    itemRegister.Add(item);
                    //Console.WriteLine(i.Name);
                }
                return itemRegister;
            }

            //try
            //{
            //    return itemRegister;
            //}
            //catch (Exception e)
            //{
            //    // Something unexpected went wrong.
            //    Console.WriteLine(e);
            //    return null;
            //}
        }

        public User LogIn(string username, string password)
        {
            User currentUser = new User();

            try
            {
                using (WebShopContainer ctx = new WebShopContainer())
                {
                    //User user = new User();
                    var user = from u in ctx.Users
                                   where u.Username.Equals(username) && u.Password.Equals(password) 
                                   select u;

                    foreach (User u in user)
                    {
                        if(u.Username == "")
                        {
                            return currentUser;
                        } else
                        {
                            currentUser = u;
                            Console.WriteLine($"Succes bitch! user = {currentUser.Username}");
                        }
                        
                    }
                }
                return currentUser;

            } catch(Exception e)
            {
                Console.WriteLine("Your login credentials are invalid" + e);
                return currentUser;
            }
            //foreach (User u in userRegister)
            //{
            //    Console.WriteLine($"check: {username.Equals(u.Username) && password.Equals(u.Password)}");
            //    if (username.Equals(u.Username) && password.Equals(u.Password)) // fout, werkt alleen voor eerste index (?)
            //    {
            //        Console.WriteLine("User logged in: " + username);
            //        return (u);
            //    }
            //}
            //Console.WriteLine("Your login credentials are invalid");
            //return (new User("", "", 0.00, new List<Purchase>()));
        }

        List<Purchase> IWinkelService.getUserInventory(User u)
        {
            throw new NotImplementedException();
        }


        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}
