using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class GiftDAO
    {
        private MyDBContext db = new MyDBContext();
        // tra ve danh sach cac mau tin
        public List<Gift> getList(string status = "All")
        {
            List<Gift> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Gifts.Where(m => m.Status != 0).ToList();
                        break;

                    }
                case "Trash":
                    {
                        list = db.Gifts.Where(m => m.Status == 0).ToList();
                        break;

                    }
                default:
                    {
                        list = db.Gifts.ToList();
                        break;
                    }

            }

            return list;

        }
        //Tra ve 1 mau tin
        public Gift getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Gifts.Find(id);
            }
        }
        // Them mau tin
        public int Insert(Gift row)
        {

            db.Gifts.Add(row);
            return db.SaveChanges();
        }
        // Cap Nhat mau tin
        public int Update(Gift row)
        {

            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        // Xoa mau tin
        public int Delete(Gift row)
        {
            db.Gifts.Remove(row);
            return db.SaveChanges();
        }
    }
}
