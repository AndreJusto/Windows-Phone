using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TrainingAcademia.Classes
{
    [Table(Name = "Treinos")]
    public class Treinos
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(CanBeNull = false)]
        public string Nome { get; set; }
        [Column(CanBeNull = false)]
        public string Descricao { get; set; }
        [Column(CanBeNull = false)]
        public int IdSemana { get; set; }
        [Column(CanBeNull = false)]
        public int IdAlunos { get; set; }
    }
}
