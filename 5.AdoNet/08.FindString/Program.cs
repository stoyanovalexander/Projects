using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _08.FindString
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter Product name");
            string name = Console.ReadLine();
            name = EscapeSQLString(name);
            SqlConnection dbCon = new SqlConnection(Settings8.Default.dbConString); // стринга ми е в setings8.setings фаила.
            dbCon.Open();
            using (dbCon)
            {
                SqlCommand cmdSelect = new SqlCommand(
                    "SELECT ProductName FROM Products WHERE ProductName LIKE @name ESCAPE '|'", dbCon);
                cmdSelect.Parameters.AddWithValue("@name", "%" + name + "%");

                Console.WriteLine("Products:");
                SqlDataReader reader = cmdSelect.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string productName = (string)reader["ProductName"];
                        Console.WriteLine(productName.Trim());
                    }
                }
            }
        }

        static string EscapeSQLString(string name)
        {
            name = name.Replace("%", "|%");
            name = name.Replace("_", "|_");
            name = name.Replace("\\", "|\\");
            name = name.Replace("\"", "|\"");
            return name.Replace("'", "''");
        }
    }
}
