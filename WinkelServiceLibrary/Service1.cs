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
                    User user = new User(username, password, 100);
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    Console.WriteLine("Succes!");
                }
            }
            catch (Exception e)
            {
                // Something went wrong.
                Console.WriteLine(e);
            }

        }

        public User BuyItem(Purchase p, User u) //ook buyer meegeven
        {
            using (WebShopContainer ctx = new WebShopContainer())
            {
                Purchase pur = new Purchase();
                pur.Id = p.Id;
                pur.Amount = p.Amount;
                pur.ItemName = p.ItemName;
                pur.UserUsername = u.Username;
                User ctxUser = ctx.Users.Find(u.Username);
                ctxUser.Saldo -= p.Item.Price;
                ctxUser.Purchases.Add(pur);
                Item ctxItem = ctx.Items.Find(p.ItemName);
                ctxItem.Stock -= p.Amount;
                ctx.SaveChanges();

                u.Purchases.Add(p);
                u.Saldo -= p.Item.Price;
                return u;
            }
        }

        public List<Item> GetItems()
        {
            List<Item> itemRegister = new List<Item>();
            Console.WriteLine(itemRegister.ToString());
            Console.WriteLine("items added to list");

            using (WebShopContainer ctx = new WebShopContainer())
            {
                var items = from i in ctx.Items
                            select i;

                foreach (Item i in items)
                {
                    Item item = new Item();
                    item.Name = i.Name;
                    item.Price = i.Price;
                    item.Stock = i.Stock;
                    itemRegister.Add(item);
                }
                return itemRegister;
            }
        }

        public User LogIn(string username, string password)
        {
            User currentUser = new User();

            try
            {
                using (WebShopContainer ctx = new WebShopContainer())
                {
                    var user = from u in ctx.Users.Include("Purchases.Item")
                               where u.Username.Equals(username) && u.Password.Equals(password)
                               select u;

                    foreach (User u in user)
                    {
                        if (u.Username == "")
                        {
                            return currentUser;
                        }
                        else
                        {
                            currentUser = u;
                            Console.WriteLine($"Succes bitch! user = {currentUser.Username}");
                        }

                    }
                }
                return currentUser;

            }
            catch (Exception e)
            {
                Console.WriteLine("Your login credentials are invalid" + e);
                return currentUser;
            }
        }
    }

}
