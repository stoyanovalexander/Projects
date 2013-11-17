using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _01.NumOfRowsInCategories
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
                SqlCommand cmdCount = new SqlCommand(
                    "SELECT COUNT(*) FROM Categories", dbCon);
                int categories = (int)cmdCount.ExecuteScalar();
                Console.WriteLine("Categories count: {0} ", categories);
            }
        }
    }
}
