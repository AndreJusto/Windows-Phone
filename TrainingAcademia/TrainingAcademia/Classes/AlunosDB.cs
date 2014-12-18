using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAcademia.Classes
{
    public class AlunosDB
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

        public static Alunos GetAluno(int aId)
        {
            DataBase db = getDataBase();
            Alunos alunos = (from alu in db.Alunos where alu.ID == aId select alu).First();
            return alunos;
        }

        public static List<Alunos> GetAlunos(string aNome)
        {
            DataBase db = getDataBase();

            if (aNome != null)
            {
                var query = from alu in db.Alunos where alu.Nome.Contains(aNome) orderby alu.Nome select alu;
                List<Alunos> alunos = new List<Alunos>(query.AsEnumerable());
                return alunos;
            }
            else
            {
                var query = from alu in db.Alunos orderby alu.Nome select alu;
                List<Alunos> alunos = new List<Alunos>(query.AsEnumerable());
                return alunos;
            }

        }


        public static void Salvar(Alunos aluno)
        {
            DataBase db = getDataBase();
            db.Alunos.InsertOnSubmit(aluno);
            db.SubmitChanges();
        }

        public static void Deletar(Alunos aluno)
        {
            DataBase db = getDataBase();
            var query = from alu in db.Alunos
                        where alu.ID == aluno.ID
                        select alu;
            db.Alunos.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }
        public static void Atualizar(Alunos a)
        {
            DataBase db = getDataBase();
            Alunos aluno = (from alu in db.Alunos
                              where alu.ID == a.ID
                              select alu).First();

            aluno.Nome = a.Nome;
            db.SubmitChanges();
        }
    }
}
