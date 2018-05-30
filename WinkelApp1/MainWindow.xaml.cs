﻿using System;
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

        private void EmptyUserInventory()
        {
            try
            {
                //delete all entries
                foreach (Purchase p in loggedInUser.PurchaseList)
                {
                    InventoryList.Items.Remove($"Item bought: {p.Item.Name} \nAmount bought: {p.Amount}, Price: {p.Item.Price} \nTotal Price: {p.Item.Price * p.Amount}");
                }
            }
            catch
            {
                Console.WriteLine("No items to remove (yet)");
            }
        }

        private void FillUserInventory()
        {
            //delete all entries
            foreach (Purchase p in loggedInUser.PurchaseList)
            {
                InventoryList.Items.Remove($"Item bought: {p.Item.Name} \nAmount bought: {p.Amount}, Price: {p.Item.Price} \nTotal Price: {p.Item.Price * p.Amount}");
            }

            //add all entries (also new ones)
            foreach (Purchase p in loggedInUser.PurchaseList)
            {
                InventoryList.Items.Add($"Item bought: {p.Item.Name} \nAmount bought: {p.Amount}, Price: {p.Item.Price} \nTotal Price: {p.Item.Price * p.Amount}");
            }
        }

        public void fillShopInventory()
        {
            ItemList.SelectedItem = null;
            selectedItem = null;

            foreach (Item i in winkelService.GetItems())
            {
                ItemList.Items.Remove(i);
            }

            foreach (Item i in winkelService.GetItems())
            {
                if (i.InStore > 0)
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
            string text = RegisterUsername.Text;
            try
            {
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
                EmptyUserInventory();
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
                    Console.WriteLine(selectedItem.Name + ", " + selectedItem.Price + ", " + selectedItem.InStore);
                    ResultBox.Text = (selectedItem.Name + ", prijs: " + selectedItem.Price + ", Instore: " + selectedItem.InStore);
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
            int amount;
            try
            {
                amount = Int32.Parse(AmountBox.Text);
                if ((amount >= 0) && (amount % 1 == 0) && (selectedItem.Price * amount) <= loggedInUser.Saldo && (amount <= selectedItem.InStore) && selectedItem != null)
                {
                    Purchase p = new Purchase(loggedInUser, selectedItem, amount);
                    Console.WriteLine(p.ToString());
                    winkelService.BuyItem(p);

                    ItemList.Items.Refresh();

                    Console.WriteLine("Purchase info: " + p.ToString());
                    ResultBox.Text = ("Purchase info: " + p.ToString());

                    ItemList.Items.Refresh();

                    SaldoBox.Text = loggedInUser.Saldo.ToString();

                    InventoryList.Items.Refresh();

                    FillUserInventory();

                    Console.WriteLine("refreshing values");
                    ResultBox.Text += ("\nrefreshing values");

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
            catch (NullReferenceException)
            {
                Console.WriteLine("You must first select an item to buy");
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
            foreach (Purchase p in loggedInUser.PurchaseList)
            {
                InventoryList.Items.Remove($"Item bought: {p.Item.Name} \nAmount bought: {p.Amount}, Price: {p.Item.Price} \nTotal Price: {p.Item.Price * p.Amount}");
            }
        }

        private void On_Click_Refresh_Store(object sender, RoutedEventArgs e)
        {
            fillShopInventory();
        }
    }
}
