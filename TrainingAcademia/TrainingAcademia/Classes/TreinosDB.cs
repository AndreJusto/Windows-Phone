using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAcademia.Classes
{
    public class TreinosDB
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

        public static List<Treinos> GetTreinos(int pTreino1, int pTreino2)
        {
            DataBase db = getDataBase();
            var query = from trei in db.Treinos
                        where trei.IdSemana == pTreino1 && trei.IdAlunos == pTreino2
                        orderby trei.Nome
                        select trei;

            return new List<Treinos>(query.AsEnumerable());
        }

        public static void Salvar(Treinos treino)
        {
            DataBase db = getDataBase();
            db.Treinos.InsertOnSubmit(treino);
            db.SubmitChanges();
        }

        public static void Deletar(Treinos treino)
        {
            DataBase db = getDataBase();
            var query = from trei in db.Treinos
                        where trei.Id == treino.Id
                        select trei;
            db.Treinos.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }

        public static void Atualizar(Treinos t)
        {
            DataBase db = getDataBase();
            Treinos treino = (from trei in db.Treinos
                                    where trei.Id == t.Id
                                    select trei).First();

            treino.Nome = t.Nome;
            treino.Descricao = t.Descricao;
            db.SubmitChanges();
        }
    }
}
