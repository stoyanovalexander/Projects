using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _07.InsertRowsInExcel
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Score");
            int score = int.Parse(Console.ReadLine());
            OleDbConnection connection = new OleDbConnection(Settings7.Default.conString); // стринга ми е в setings7.setings фаила.
            connection.Open();
            using (connection)
            {
                OleDbCommand cmdInsert = new OleDbCommand(
                    "INSERT INTO [Sheet1$](Name, Score) " +
                    "VALUES (@name, @score)", connection);
                cmdInsert.Parameters.AddWithValue("@name", name);
                cmdInsert.Parameters.AddWithValue("@score", score);
                cmdInsert.ExecuteNonQuery();

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
