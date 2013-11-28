using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Data;

namespace _02.DAOClass
{
    public class DAO
    {
        static void Main()
        {
            
        }

        static void Add(string name, string id)
        {
            Customer newCustomer = new Customer()
            {
                CompanyName = name,
                CustomerID = id
            };

            using (var db = new NorthwindEntities())
            {
                bool isInDB = IsInDataBase(db, id);

                if (!isInDB)
                {
                    db.Customers.Add(newCustomer);
                    db.SaveChanges();
                    Console.WriteLine("Added Successful.");
                }
                else
                {
                    throw new ArgumentException("Such customer already exists");
                }
            }
        }

        static void Edit(string id, string newContactName)
        {
            using (var db = new NorthwindEntities())
            {
                var customer = db.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
                customer.ContactName = newContactName;
                db.SaveChanges();
            }
        }

        static void Delete(string id)
        {
            using (var db = new NorthwindEntities())
            {
                var customer = db.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
        }

        static bool IsInDataBase(NorthwindEntities db, string id)
        {
            bool alreadyInDB = db.Customers.Where(a => a.CustomerID == id).Any();
            return alreadyInDB;
        }
    }
}
