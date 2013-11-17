using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _02.NameAndDescOfCategories
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
                    "SELECT CategoryName, Description FROM Categories", dbCon);

                Console.WriteLine("Category:\tDescription");
                SqlDataReader reader = cmdSelect.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["CategoryName"];
                        string description = (string)reader["Description"];
                        Console.WriteLine("{0}:\t{1}", name.Trim(), description.Trim());
                    }
                }
            }
        }
    }
}
