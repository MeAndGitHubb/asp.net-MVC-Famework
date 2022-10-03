using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class NewYearDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<NewYear> getList(string status = "All")
        {
            List<NewYear> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.NewYears.Where(m => m.Id != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.NewYears.Where(m => m.Id == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.NewYears.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public NewYear getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.NewYears.Find(id);
            }
        }
        // Them mau tin
        public int Insert(NewYear row)
        {

            db.NewYears.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(NewYear row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(NewYear row)
        {
            db.NewYears.Remove(row);
            return db.SaveChanges();
        }
    }
}
