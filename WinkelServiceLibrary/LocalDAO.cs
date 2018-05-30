using System;
using System.Data.SqlClient;
using System.Text;


namespace WinkelServiceLibrary
{
    public class LocalDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //--CREATE TABLE[PURCHASE] ([NAME] varchar(255) ,[ITEM] varchar(255), [AMOUNT] int, FOREIGN KEY([NAME]) REFERENCES[USER] ([USERNAME]),FOREIGN KEY([ITEM]) REFERENCES[ITEM] ([NAME]));
        //--CREATE TABLE[ITEM] ([NAME] varchar(255),[PRICE] float (53),[InStore] int, PRIMARY KEY([NAME]));
        //--CREATE TABLE[USER]([username] varchar(255), [password] varchar(255), [saldo] float (53), PRIMARY KEY([username]));

        public void test123()
        {
            Console.WriteLine("JE MOEDER");
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "ondora02.hu.nl:8521/cursus02.hu.nl";
                    builder.UserID = "tosad_2017_2d_team2";
                    builder.Password = "tosad_2017_2d_team2";
                    //builder.InitialCatalog = "test";
                    //builder.DataSource = "your_server.database.windows.net";
                    //builder.UserID = "your_user";
                    //builder.Password = "your_password";
                    //builder.InitialCatalog = "your_database";

                    //using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        Console.WriteLine("\nQuery data example:");
                        Console.WriteLine("=========================================\n");

                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("CREATE TABLE [USER]([username] varchar,[password] varchar,[saldo] float);");
                        String sql = sb.ToString();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString() + "doei");
                }
                Console.WriteLine("SQL Successfull");
                Console.ReadLine();
            }
        }
    }

}
