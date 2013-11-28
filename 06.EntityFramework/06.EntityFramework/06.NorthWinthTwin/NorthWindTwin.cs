using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EntityFramework.Data;
using System.Transactions;
using System.Data.Objects;

namespace _06.NorthWinthTwin
{
    class NorthWindTwin
    {
        static void Main()
        {
            
        }

        private static void CreatingNewDataBase(NorthwindEntities northwindEntities)
        {
            
           var script= northwindEntities.Database.ExecuteSqlCommand("CREATE DATABASE [NorthwindTwin] \n GO \nUSE [NorthwindTwin] \n"); 
            //string script = northwindEntities.CreateDatabaseScript();
          //script = "CREATE DATABASE [NorthwindTwin] \n GO \nUSE [NorthwindTwin] \n" + script;

            StreamWriter northwindTwinFile = new StreamWriter("NorthwindTwin.sql");
            using (northwindTwinFile)
            {
                northwindTwinFile.WriteLine(script);
            }
            Console.WriteLine(script);
        }
    }
}
