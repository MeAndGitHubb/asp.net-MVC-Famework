using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class HotDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<Hot> getList(string status = "All")
        {
            List<Hot> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Hots.Where(m => m.Status != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Hots.Where(m => m.Status == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Hots.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Hot getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Hots.Find(id);
            }
        }
        // Them mau tin
        public int Insert(Hot row)
        {

            db.Hots.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Hot row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Hot row)
        {
            db.Hots.Remove(row);
            return db.SaveChanges();
        }
    }
}
