using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Data;

namespace _01.CreateDbContext
{
    class CreateDbContext
    {
        static void Main()
        {
            //NorthwindEntities dbContext = new NorthwindEntities();
            using (var dbContext = new NorthwindEntities())
            {
            }
        }
    }
}
