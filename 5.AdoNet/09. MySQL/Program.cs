using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace _09.MySQL
{
    class Program
    {
        static void Main()
        {
            MySqlConnection dbCon = new MySqlConnection(Settings9.Default.dbConString); // стринга ми е в setings6.setings фаила.
            dbCon.Open();
            ListBooks(dbCon);
            dbCon.Open();
            FindBook(dbCon);
            dbCon.Open();
            InsertBook(dbCon);
        }

        static string EscapeSQLString(string name)
        {
            name = name.Replace("%", "|%");
            name = name.Replace("_", "|_");
            name = name.Replace("\\", "|\\");
            name = name.Replace("\"", "|\"");
            return name.Replace("'", "''");
        }

        private static void ListBooks(MySqlConnection dbCon)
        {
            using (dbCon)
            {
                MySqlCommand cmdSelect = new MySqlCommand(
                    "SELECT * FROM Books", dbCon);

                Console.WriteLine("Books:");
                Console.WriteLine("ID\tTitle\t\tAuthor\t\tPublish Date\tISBN");
                MySqlDataReader reader = cmdSelect.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["BookId"];
                        string title = (string)reader["Title"];
                        string author = (string)reader["Author"];
                        DateTime publishDate = (DateTime)reader["PublishDate"];
                        string isbn = (string)reader["ISBN"];
                        Console.WriteLine("{0}\t{1}\t{2}\t{3:dd.MM.yyyy}\t{4}",
                            id, title.Trim(), author.Trim(), publishDate, isbn.Trim());
                    }
                }
            }
        }

        private static void FindBook(MySqlConnection dbCon)
        {
            Console.WriteLine("Enter Book title");
            string name = Console.ReadLine();
            name = EscapeSQLString(name);
            using (dbCon)
            {
                MySqlCommand cmdSelect = new MySqlCommand(
                    "SELECT * FROM Books WHERE Title=@name", dbCon);
                cmdSelect.Parameters.AddWithValue("@name", name);

                Console.WriteLine("Books:");
                Console.WriteLine("ID\tTitle\t\tAuthor\t\tPublish Date\tISBN");
                MySqlDataReader reader = cmdSelect.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["BookId"];
                        string title = (string)reader["Title"];
                        string author = (string)reader["Author"];
                        DateTime publishDate = (DateTime)reader["PublishDate"];
                        string isbn = (string)reader["ISBN"];
                        Console.WriteLine("{0}\t{1}\t{2}\t{3:dd.MM.yyyy}\t{4}",
                            id, title.Trim(), author.Trim(), publishDate, isbn.Trim());
                    }
                }
            }
        }

        private static void InsertBook(MySqlConnection dbCon)
        {
            Console.WriteLine("Enter Book title");
            string title = Console.ReadLine();
            title = EscapeSQLString(title);
            Console.WriteLine("Enter Book author");
            string author = Console.ReadLine();
            author = EscapeSQLString(author);
            Console.WriteLine("Enter Book publish date in format dd.MM.yyyy");
            DateTime publishDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", new CultureInfo("en-US"));
            Console.WriteLine("Enter Book ISBN");
            string isbn = Console.ReadLine();
            isbn = EscapeSQLString(isbn);
            using (dbCon)
            {
                MySqlCommand cmdInsert = new MySqlCommand(
                    "INSERT INTO Books(Title, Author, PublishDate, ISBN) " +
                    "VALUES (@title, @author, @publishDate, @isbn)", dbCon);
                cmdInsert.Parameters.AddWithValue("@title", title);
                cmdInsert.Parameters.AddWithValue("@author", author);
                cmdInsert.Parameters.AddWithValue("@publishDate", publishDate);
                cmdInsert.Parameters.AddWithValue("@isbn", isbn);
                cmdInsert.ExecuteNonQuery();
            }
        }
    }
}
