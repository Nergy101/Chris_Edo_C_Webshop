using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
namespace WinkelServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWinkelService" in both code and config file together.
    [ServiceContract]
    public interface IWinkelService
    {
        [OperationContract]
        string GetData(int value);
        [OperationContract]
        void Register(string username, string password);
        [OperationContract]
        User LogIn(string username, string password);
        [OperationContract]
        List<Item> GetItems();

        /// <summary>
        /// hieronder nog in de maak
        /// </summary>
        [OperationContract]
        void BuyItem(Purchase p);
        [OperationContract]
        List<Purchase> getUserInventory(User u);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    [DataContract]
    public class User
    {
        public User(string username, string password, double saldo, List<Purchase> purchaseList)
        {
            Username = username;
            Password = password;
            this.Saldo = saldo;
            this.PurchaseList = purchaseList;
        }

        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public double Saldo { get; set; }

        [DataMember]
        public List<Purchase> PurchaseList { get; set; }

        public override string ToString()
        {
            return ($"{Username}, {Password}, {Saldo}, {PurchaseList}");
        }
    }
    [DataContract]
    public class Item
    {
        public Item(string name, double price, int inStore)
        {
            Name = name;
            Price = price;
            InStore = inStore;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public int InStore { get; set; }

        public override string ToString()
        {
            return ($"{Name}, €{Price}, {InStore} pc.");
        }
    }
    [DataContract]
    public class Purchase
    {
        public Purchase(User buyer, Item item, int amount)
        {
            Buyer = buyer;
            Item = item;
            Amount = amount;
        }

        [DataMember]
        public User Buyer { get; set; }
        [DataMember]
        public Item Item { get; set; }
        [DataMember]
        public int Amount { get; set; }

        public override string ToString()
        {
            return ($"{Buyer}, {Item}, {Amount} pc, total costs: {Item.Price*Amount}");
        }
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WinkelServiceLibrary.ContractType".

    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
