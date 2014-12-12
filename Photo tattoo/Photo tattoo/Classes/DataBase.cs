using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Photo_tattoo.Classes
{

    class DataBase : DataContext
    {
        public static string DBConnectionString = "Data Source ='isostore:photos.sdf'";

        public DataBase()
            : base(DBConnectionString)
        { }

        public Table<Photos> Photos
        {
            get
            {
                return this.GetTable<Photos>();
            }
        }
    }
}
