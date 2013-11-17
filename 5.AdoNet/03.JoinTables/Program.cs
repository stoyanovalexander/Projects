using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _03.JoinTables
{
    class Program
    {
        static void Main()
        {
            // ПРЕДИ ПРОВЕРКА ВИЖ КАК Е НАПИСАНА ПРИ ТЕБ БАЗАТА ДАННИ ПРИ МЕН Е NORTHWND (без I), Промени го 
            // в стринга отдолу според както е при теб(пиша това защото на мен ми се случи да се мотая заради това)
            SqlConnection dbCon = new SqlConnection("Server=.; " +
                                "Database=NORTHWND; Integrated Security=true");
            dbCon.Open();
            using (dbCon)
            {
                SqlCommand cmdSelect = new SqlCommand(
                    "SELECT p.ProductName, c.CategoryName FROM Products p " +
                    "JOIN Categories c ON p.CategoryID = c.CategoryID " +
                    "GROUP BY CategoryName, ProductName", dbCon);

                SqlDataReader reader = cmdSelect.ExecuteReader();
                StringBuilder result = new StringBuilder();
                using (reader)
                {
                    reader.Read();
                    string categoryName = (string)reader["CategoryName"];
                    string productName = (string)reader["ProductName"];
                    Console.WriteLine(categoryName.Trim() + ":");
                    result.Append(productName.Trim());
                    while (reader.Read())
                    {
                        string currentCategory = (string)reader["CategoryName"];
                        productName = (string)reader["ProductName"];
                        if (categoryName == currentCategory)
                        {
                            result.Append(", " + productName.Trim());
                        }
                        else
                        {
                            Console.WriteLine(result);
                            Console.WriteLine();
                            result = new StringBuilder();
                            categoryName = currentCategory;
                            Console.WriteLine(categoryName.Trim() + ":");
                            result.Append(productName.Trim());
                        }
                    }
                }
            }
        }
    }
}
