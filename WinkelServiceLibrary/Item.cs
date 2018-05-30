using System.Runtime.Serialization;
namespace WinkelServiceLibrary
{
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
