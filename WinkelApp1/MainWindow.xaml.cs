using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinkelServiceLibrary; //hierdoor?
using System.Data.SqlClient;

namespace WinkelApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        IWinkelService winkelService = new WinkelService();
        List<Item> itemList = new List<Item>();
        Item selectedItem;
        User loggedInUser;

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        //private void EmptyUserInventory()
        //{
        //    try
        //    {
        //        //using (WebShopContainer ctx = new WebShopContainer())
        //        //{
        //        //    ctx.Database.ExecuteSqlCommand("");
        //        //        dbContext.Database.ExecuteSqlCommand("delete from MyTable");
        //        //}
        //        //delete all entries
        //        //foreach (Purchase p in loggedInUser.PurchaseList)
        //        //{
        //        //    InventoryList.Items.Remove($"Item bought: {p.Item.Name} \nAmount bought: {p.Amount}, Price: {p.Item.Price} \nTotal Price: {p.Item.Price * p.Amount}");
        //        //}
        //    }
        //    catch
        //    {
        //        Console.WriteLine("No items to remove (yet)");
        //    }
        //}

        private void FillUserInventory()
        {
            //List<Purchase> purchases = winkelService.GetUserInventory(loggedInUser);
            //foreach (Purchase p in purchases)
            //{
            //    Console.WriteLine(p.ItemName);
            //    InventoryList.Items.Remove($"Item bought: {p.ItemName} \nAmount bought: {p.Amount}, Price: {p.Item.Price} \nTotal Price: {p.Item.Price * p.Amount}");
            //}

            //delete all entries
            InventoryList.Items.Clear();

            //add all entries (also new ones)
            foreach (Purchase p in loggedInUser.Purchases)
                {
                //Console.WriteLine(p.ItemName);
                InventoryList.Items.Add($"Item bought: {p.ItemName} \nAmount bought: {p.Amount}, Price: {p.Item.Price} \nTotal Price: {p.Item.Price * p.Amount}");
            }
        }

        public void fillShopInventory()
        {
            ItemList.SelectedItem = null;
            selectedItem = null;

            //foreach (Item i in winkelService.GetItems())
            //{
            //    ItemList.Items.Remove(i);
            //}
            ItemList.Items.Clear();

            foreach (Item i in winkelService.GetItems())
            {
                if (i.Stock > 0)
                {
                    ItemList.Items.Add(i);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //kannie weg
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void RegisterUsernameHandler(object sender, TextChangedEventArgs e)
        {

            try
            {
                string text = RegisterUsername.Text;
                RegisterPasswordInput.Text = Reverse(text);
            }
            catch (NullReferenceException a)
            {
                //ErrorBox.Text = a.ToString();
            }

        }


        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            winkelService.Register(RegisterUsername.Text, RegisterPasswordInput.Text);

        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(LoginUsername.Text == "" || LoginPassword.Text == ""))
            {
                //EmptyUserInventory();
                string loginName = LoginUsername.Text;
                string loginPass = LoginPassword.Text;
                loggedInUser = winkelService.LogIn(loginName, loginPass);
                //if not valid returns ""

                if (!(loggedInUser.Username == ""))
                {
                    SaldoBox.Text = loggedInUser.Saldo.ToString();
                    if (ItemList.Items.IsEmpty)
                    {
                        Console.WriteLine("Adding items....");
                        ResultBox.Text = ("Adding items....");
                        fillShopInventory();
                    }
                    FillUserInventory();
                }
            }
        }

        //private void OnItemSelected(object sender, RoutedEventArgs e)
        //{
        //    ItemList = e.Source as ListBox;

        //    if (ItemList != null)
        //    {
        //        Console.WriteLine( ItemList.SelectedItem.ToString() + " is selected.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("value is null");
        //    }
        //}

        private void OnItemSelection(object sender, SelectionChangedEventArgs e)
        {
            ItemList = e.Source as ListBox;

            try
            {
                if (ItemList != null)
                {
                    //Console.WriteLine(ItemList.SelectedItem.ToString() + " is selected.");
                    selectedItem = (Item)ItemList.SelectedItem;
                    Console.WriteLine(selectedItem.Name + ", " + selectedItem.Price + ", " + selectedItem.Stock);
                    ResultBox.Text = (selectedItem.Name + ", prijs: " + selectedItem.Price + ", Instore: " + selectedItem.Stock);
                }
                else
                {
                    Console.WriteLine("value is null");
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("There is/was no selected Item");
                ResultBox.Text = ("There is/was no selected Item");
            }
        }

        private void OnPurchaseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = Int32.Parse(AmountBox.Text);
                Console.WriteLine(amount);
                Console.WriteLine(selectedItem.Price);
                Console.WriteLine(loggedInUser.Saldo);
                if ((amount >= 0) && (amount % 1 == 0) && (selectedItem.Price * amount) <= loggedInUser.Saldo && (amount <= selectedItem.Stock) && selectedItem != null)
                {
                    Purchase p = new Purchase();
                    p.User = loggedInUser;
                    p.Item = selectedItem;
                    p.Amount = (Int16)amount;
                    p.UserUsername = loggedInUser.Username;
                    p.ItemName = selectedItem.Name;
                    Console.WriteLine(p.ToString());
                    loggedInUser = winkelService.BuyItem(p, loggedInUser);

                    ItemList.Items.Refresh();

                    Console.WriteLine("Purchase info: " + p.ToString());
                    ResultBox.Text = ("Purchase info: " + p.ToString());

                    ItemList.Items.Refresh();

                    SaldoBox.Text = loggedInUser.Saldo.ToString();

                    InventoryList.Items.Refresh();

                    FillUserInventory();

                    Console.WriteLine("refreshing values");
                    ResultBox.Text += ("\nrefreshing values");
                    fillShopInventory();

                }
                else
                {
                    Console.WriteLine($"Either you dont have enough Saldo and/or there are not enough {selectedItem.Name}(s) in store\n" +
                        $"Your amount must be 1 or higher and/or You can only buy complete products. Buying {amount} is not allowed!");

                    ResultBox.Text = ($"Either you dont have enough Saldo and/or there are not enough {selectedItem.Name}(s) in store\n" +
                        $"Your amount must be 1 or higher and / or You can only buy complete products. Buying { amount} is not allowed!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("the amount must be a whole number");
                ResultBox.Text = ("the amount must be a whole number");

            }
            catch (NullReferenceException f)
            {
                Console.WriteLine("You must first select an item to buy" + f);
                ResultBox.Text = ("You must first select an item to buy");
            }

        }

        private void TextBox_TextChanged_1(object sender, RoutedEventArgs e)
        {

        }

        private void ResultBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void On_Inventory_Refresh_Click(object sender, RoutedEventArgs e)
        {
            FillUserInventory();
        }



        private void On_Click_Empty_History(object sender, RoutedEventArgs e)
        {
            //foreach (Purchase p in loggedInUser.PurchaseList)
            //{
            //    InventoryList.Items.Remove($"Item bought: {p.Item.Name} \nAmount bought: {p.Amount}, Price: {p.Item.Price} \nTotal Price: {p.Item.Price * p.Amount}");
            //}
        }

        private void On_Click_Refresh_Store(object sender, RoutedEventArgs e)
        {
            fillShopInventory();
        }
    }
}
