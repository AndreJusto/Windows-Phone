using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Photo_tattoo.Classes
{
    class PhotosDB
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

        public static List<Photos> GetPhotos(string pCategory)
        {
            DataBase db = getDataBase();

            if (pCategory != null)
            {
                var query = from pho in db.Photos where pho.category.Contains(pCategory) orderby pho.category select pho;
                List<Photos> photos = new List<Photos>(query.AsEnumerable());
                return photos;
            }
            else
            {
                var query = from pho in db.Photos orderby pho.category select pho;
                List<Photos> photos = new List<Photos>(query.AsEnumerable());
                return photos;
            }

        }

        public static void Salvar(Photos photo)
        {
            DataBase db = getDataBase();
            db.Photos.InsertOnSubmit(photo);
            db.SubmitChanges();
        }

        public static void Deletar(Photos photo)
        {
            DataBase db = getDataBase();
            var query = from pho in db.Photos
                        where pho.id == photo.id
                        select pho;
            db.Photos.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }
        public static void Atualizar(Photos p)
        {
            DataBase db = getDataBase();
            Photos photo = (from pho in db.Photos
                            where pho.id == p.id
                            select pho).First();

            photo.category = p.category;
            db.SubmitChanges();
        }
    }
}
