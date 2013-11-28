using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Data;

namespace _04.AsPreviousWithNativeSql
{
    class AsPreviousWithNativeSql
    {
        static void Main()
        {
            int year = 1997;
            FindAllCustomersWithNativeSQL(year, "Canada");
        }
        static void FindAllCustomersWithNativeSQL(int orderDate, string country)
        {
            using (var db = new NorthwindEntities())
            {
                string sqlQuery = @"SELECT c.ContactName from Customers" +
                                  " c INNER JOIN Orders o ON o.CustomerID = c.CustomerID " +
                                  "WHERE (YEAR(o.OrderDate) = {0} AND o.ShipCountry = {1});";

                object[] parameters = { orderDate, country };
                var sqlQueryResult = db.Database.SqlQuery<string>(sqlQuery, parameters);

                foreach (var order in sqlQueryResult)
                {
                    Console.WriteLine(order);
                }
            }
        }
    }
}
