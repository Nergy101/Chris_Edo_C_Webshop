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
            // Set the ListBox to display items in multiple columns.
            // Set the selection mode to multiple and extended.
            ListBox ItemList = new ListBox();
            ItemList.SelectionMode = SelectionMode.Multiple;
            ItemList.SelectionMode = SelectionMode.Extended;
            InitializeComponent();
        }

        IWinkelService winkelService = new WinkelService();
        List<Item> itemList = new List<Item>();
        Item selectedItem;
        User loggedInUser;

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
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            winkelService.Register(RegisterUsername.Text, RegisterPasswordInput.Text);
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(LoginUsername.Text == "" || LoginPassword.Text == ""))
            {
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
                        foreach (Item i in winkelService.GetItems())
                        {
                            ItemList.Items.Add(i);
                        }
                    }
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

        private void OnPurchaseClick(object sender, RoutedEventArgs e)
        {
            int amount;
            try
            {
                amount = Int32.Parse(AmountBox.Text);
                if ((selectedItem.Price * amount) <= loggedInUser.Saldo && (amount <= selectedItem.InStore) && selectedItem != null)
                {

                    Console.WriteLine(new Purchase(loggedInUser, selectedItem, amount).ToString());
                    winkelService.BuyItem(new Purchase(loggedInUser, selectedItem, amount));
                    Console.WriteLine("refreshing items");
                    ResultBox.Text = ("refreshing items");

                    ///
                    /// Hieronder faalt nog, delete random de gekozen entry maar niet alle, en stopt er geen nieuwe in
                    ///
                    for (int n = ItemList.Items.Count - 1; n >= 0; --n)
                    {
                            ItemList.Items.RemoveAt(n);
                    }

                    foreach (Item i in winkelService.GetItems())
                    {
                        ItemList.Items.Add(i);
                    }
                    
                    // update Saldo, mss BuyItem een User laten returnen weer?

                }
                else
                {
                    Console.WriteLine($"Either you dont have enough Saldo and/or there are not enough {selectedItem.Name}(s) in store");
                    ResultBox.Text = ($"Either you dont have enough Saldo and/or there are not enough {selectedItem.Name}(s) in store");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("the amount must be a numerical value");
                ResultBox.Text = ("the amount must be a numerical value");

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
    }
}
