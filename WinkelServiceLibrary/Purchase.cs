using System.Runtime.Serialization;
namespace WinkelServiceLibrary
{
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
