using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Data
{
    public partial class Employee
    {
        public EntitySet<Territory> TerritoriesSet
        {
            get
            {
                var territoriesSet = new EntitySet<Territory>();
                territoriesSet.AddRange(this.Territories);
                return territoriesSet;
            }
        }
    }
}
