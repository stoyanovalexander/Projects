using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Data;

namespace _05.FindAllSalesByDate
{
    class FindAllSalesByDate
    {
        static void Main()
        {
            FindAllSalesByDateRange("WA", new DateTime(1997, 01, 01), new DateTime(1997, 06, 06));
        }

        static void FindAllSalesByDateRange(string region, DateTime from, DateTime to)
        {
            using (var db = new NorthwindEntities())
            {
                var askedSales = db.Orders.Where(o => o.ShipRegion == region && o.OrderDate > from && o.OrderDate < to)
                                          .Select(o => new { ShipName = o.ShipName, OrderDate = o.OrderDate });

                foreach (var item in askedSales)
                {
                    Console.WriteLine(item.ShipName + " - " + item.OrderDate);
                }
            }
        }
    }
}
