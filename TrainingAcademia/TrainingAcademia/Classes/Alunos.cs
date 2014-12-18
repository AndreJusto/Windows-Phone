using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TrainingAcademia.Classes
{
    [Table(Name= "Alunos")]
    public class Alunos
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(CanBeNull = false)]
        public string Nome { get; set; }        
    }
}
