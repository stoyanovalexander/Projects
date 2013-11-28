using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Data;

namespace _03.YearAndCountry
{
    public class YearAndCountry
    {
        static void Main()
        {
            int year = 1997;
            FindAllCustomers(year, "Canada");
        }

        static void FindAllCustomers(int orderDate, string shipDestination)
        {
            using (var db = new NorthwindEntities())
            {
                var orders = from order in db.Orders
                             where order.OrderDate.Value.Year == orderDate && order.ShipCountry == shipDestination
                             select order;

                foreach (var order in orders)
                {
                    Console.WriteLine("Customer name: {0} , with CustomerId: {1}", order.Customer.ContactName, order.Customer.CustomerID);
                }
            }
        }
    }
}
