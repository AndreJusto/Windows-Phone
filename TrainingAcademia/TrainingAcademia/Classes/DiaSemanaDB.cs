using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAcademia.Classes
{
    public class DiaSemanaDB
    {
        private static DataBase getDataBase()
        {
            DataBase db = new DataBase();
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
            }
            return db;
        }

        public static List<DiaSemana> GetDiaSemana(int dSemana)
        {
            DataBase db = getDataBase();
            var query = from dia in db.DiaSemana
                        where dia.IdAluno == dSemana
                        orderby dia.Nome
                        select dia;

            return new List<DiaSemana>(query.AsEnumerable());
        }        

        public static void Salvar(DiaSemana diasemana)
        {
            DataBase db = getDataBase();
            db.DiaSemana.InsertOnSubmit(diasemana);
            db.SubmitChanges();
        }

        public static void Deletar(DiaSemana diasemana)
        {
            DataBase db = getDataBase();
            var query = from dia in db.DiaSemana
                        where dia.Id == diasemana.Id
                        select dia;
            db.DiaSemana.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }

        public static void Atualizar(DiaSemana d)
        {
            DataBase db = getDataBase();
            DiaSemana diasemana = (from dia in db.DiaSemana
                                    where dia.Id == d.Id
                                    select dia).First();

            diasemana.Nome = d.Nome;
            db.SubmitChanges();
        }    
    }
}
