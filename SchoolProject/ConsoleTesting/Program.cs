using DataAccess.Emloyees;
using GlobalLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Cryptography;
namespace ConsoleTesting
{
    internal class Program

    {
        static void Main(string[] args)
        {
            TestUpdateUser();
        }
        static void TestUpdateUser()
        {
            bool updated = clsUserData.UpdateUser(1, 1, "User1", ComputeHash("a1!A"), true);
            Console.WriteLine($"UpdateUser -> Updated? {updated}");
        }
        public static string ComputeHash(string data)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hasbyte = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hasbyte).Replace("-", "").ToLower();
            }
        }

        //static void Main(string[] args)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json",
        //        optional: false, reloadOnChange: true);
        //    IConfiguration config = builder.Build();


        //    string g = DataAccessSettings.ConnectionString;

        //    Console.WriteLine("Hello, World!");
        //    using (SqlConnection conn = new SqlConnection(g))
        //    {
        //        conn.Open();

        //        string query = "SELECT * FROM Persons";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    // لنفترض أن الجدول فيه عمود اسمه "Name"
        //                    string name = reader["PersonID"].ToString();
        //                    Console.WriteLine("Name: " + name);
        //                }
        //            }
        //        }
        //    }

        //}


    }
}
