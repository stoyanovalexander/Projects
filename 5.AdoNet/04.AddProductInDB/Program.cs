using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _04.AddProductInDB
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter Product name");
            string productName = Console.ReadLine();
            Console.WriteLine("Enter SupplierID");
            int? supplier = int.Parse(Console.ReadLine());
            SqlParameter sqlParameterSupplier = new SqlParameter("@supplier", supplier);
            if (supplier == null)
            {
                sqlParameterSupplier.Value = DBNull.Value;
            }
            Console.WriteLine("Enter CategoryID");
            int? category = int.Parse(Console.ReadLine());
            SqlParameter sqlParameterCategory = new SqlParameter("@category", category);
            if (category == null)
            {
                sqlParameterCategory.Value = DBNull.Value;
            }
            Console.WriteLine("Enter Quantity per unit");
            string quantity = Console.ReadLine();
            SqlParameter sqlParameterQuantity = new SqlParameter("@quantity", quantity);
            if (quantity == null)
            {
                sqlParameterQuantity.Value = DBNull.Value;
            }
            Console.WriteLine("Enter Unit price");
            decimal? price = decimal.Parse(Console.ReadLine());
            SqlParameter sqlParameterPrice = new SqlParameter("@price", price);
            if (price == null)
            {
                sqlParameterPrice.Value = DBNull.Value;
            }
            Console.WriteLine("Enter Units in stock");
            int? stock = int.Parse(Console.ReadLine());
            SqlParameter sqlParameterStock = new SqlParameter("@stock", stock);
            if (stock == null)
            {
                sqlParameterStock.Value = DBNull.Value;
            }
            Console.WriteLine("Enter Units on order");
            int? ordered = int.Parse(Console.ReadLine());
            SqlParameter sqlParameterOrdered = new SqlParameter("@ordered", ordered);
            if (ordered == null)
            {
                sqlParameterOrdered.Value = DBNull.Value;
            }
            Console.WriteLine("Enter Reorder level");
            int? reorder = int.Parse(Console.ReadLine());
            SqlParameter sqlParameterReorder = new SqlParameter("@reorder", reorder);
            if (reorder == null)
            {
                sqlParameterReorder.Value = DBNull.Value;
            }
            Console.WriteLine("Enter 1 if the product is Discontinued and 0 if it is not");
            int isDiscontinued = int.Parse(Console.ReadLine());
            // ПРЕДИ ПРОВЕРКА ВИЖ КАК Е НАПИСАНА ПРИ ТЕБ БАЗАТА ДАННИ ПРИ МЕН Е NORTHWND (без I), Промени го 
            // в стринга отдолу според както е при теб(пиша това защото на мен ми се случи да се мотая заради това)
            SqlConnection dbCon = new SqlConnection("Server=.; " +
                                "Database=NORTHWND; Integrated Security=true");
            dbCon.Open();
            using (dbCon)
            {
                SqlCommand cmdInsertProject = new SqlCommand(
                    "INSERT INTO Products(ProductName, SupplierID, CategoryID, QuantityPerUnit, " +
                    "UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued) " +
                    "VALUES (@productName, @supplier, @category, @quantity, " +
                    "@price, @stock, @ordered, @reorder, @isDiscontinued)", dbCon);
                cmdInsertProject.Parameters.AddWithValue("@productName", productName);
                cmdInsertProject.Parameters.Add(sqlParameterSupplier);
                cmdInsertProject.Parameters.Add(sqlParameterCategory);
                cmdInsertProject.Parameters.Add(sqlParameterQuantity);
                cmdInsertProject.Parameters.Add(sqlParameterPrice);
                cmdInsertProject.Parameters.Add(sqlParameterStock);
                cmdInsertProject.Parameters.Add(sqlParameterOrdered);
                cmdInsertProject.Parameters.Add(sqlParameterReorder);
                cmdInsertProject.Parameters.AddWithValue("@isDiscontinued", isDiscontinued);
                cmdInsertProject.ExecuteNonQuery();

                SqlCommand cmdSelectIdentity =
                    new SqlCommand("SELECT @@Identity", dbCon);
                int insertedRecordId =
                    (int)(decimal)cmdSelectIdentity.ExecuteScalar();
                Console.WriteLine("Inserted product ID: {0}", insertedRecordId);
            }
        }
    }
}
