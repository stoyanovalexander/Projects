using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _06.ReadExcelDoc
{
    class Program
    {
        static void Main()
        {
            OleDbConnection connection = new OleDbConnection(Settings6.Default.conString); // стринга ми е в setings6.setings фаила.
            connection.Open();
            using (connection)
            {
                Console.WriteLine("Name\tScore");
                OleDbCommand command = new OleDbCommand("SELECT Name, Score FROM [Sheet1$]", connection);
                OleDbDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        var firstItem = reader[0];
                        var secondItem = reader[1];
                        Console.WriteLine("{0}\t{1}", firstItem, secondItem);
                    }
                }
            }
        }
    }
}
