using System;
using WinkelServiceLibrary; //hierdoor?
using System.Data.SqlClient;

namespace dbconnectionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LocalDAO derp = new WinkelServiceLibrary.LocalDAO();
            derp.DikkeDoei();
            
        }
    }
}
