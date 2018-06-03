using System;
using System.Collections.Generic;
using System.Linq;
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
        User BuyItem(Purchase p, User u);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
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
