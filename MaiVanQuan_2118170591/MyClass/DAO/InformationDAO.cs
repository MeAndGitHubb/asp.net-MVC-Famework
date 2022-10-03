using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class InformationDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<Information> getList(string status = "All")
        {
            List<Information> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Informations.Where(m => m.Status != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Informations.Where(m => m.Status == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Informations.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Information getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Informations.Find(id);
            }
        }
        // Them mau tin
        public int Insert(Information row)
        {

            db.Informations.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Information row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Information row)
        {
            db.Informations.Remove(row);
            return db.SaveChanges();
        }
    }
}
