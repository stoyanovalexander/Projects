using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EntityFramework.Data;
using System.Transactions;

namespace ExcercisesAll
{
    class ExcercisesAll
    {
        static void Main()
        {
            //Ex10
            var SupplierIncome = findSupplierIncome();
            Console.WriteLine("Supplier {0}'s income is {1}", "ss", SupplierIncome);
        }

        static IEnumerable<int> findSupplierIncome()
        {
            NorthwindEntities northwindEntities = new NorthwindEntities();
            string nativeSQLQuery =
                "select sum(od.UnitPrice * Quantity) as SupplierIncome " +
                "from dbo.Suppliers as s " +
                "inner join dbo.Products as p " +
                "on p.SupplierID = s.SupplierID " +
                "inner join dbo.[Order Details] as od " +
                "on od.ProductID = p.ProductID " +
                "inner join dbo.Orders as o " +
                "on o.OrderID = od.OrderID " +
                "where s.CompanyName = 'Tokyo Traders' and year(o.OrderDate) = 1996";
            var income =
                northwindEntities.ExecuteStoreQuery<int>(nativeSQLQuery);
            return income;
        }
    }
}
