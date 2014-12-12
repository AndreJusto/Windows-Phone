using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Windows.Media.Imaging;

namespace Photo_tattoo.Classes
{
    [Table(Name = "Photos")]
    public class Photos
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }
        [Column(CanBeNull = false)]
        public string category { get; set; }
        [Column(DbType="IMAGE", CanBeNull= false, UpdateCheck = UpdateCheck.Never)]
        public byte[] img { get; set; }
        public BitmapImage imagem { get; set; }
    }
}
