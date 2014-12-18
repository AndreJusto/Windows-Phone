using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TrainingAcademia.Classes
{
    class DataBase : DataContext
    {
        public static string DBConnectionString = "Data Source ='isostore:alunos.sdf'";

        public DataBase()
            : base(DBConnectionString)
        { }

        public Table<Alunos> Alunos
        {
            get
            {
                return this.GetTable<Alunos>();
            }
        }

        public Table<Treinos> Treinos
        {
            get
            {
                return this.GetTable<Treinos>();
            }
        }

        public Table<DiaSemana> DiaSemana
        {
            get
            {
                return this.GetTable<DiaSemana>();
            }
        }
    }
}
